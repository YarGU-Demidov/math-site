using System;
using System.IO;
using MathSite.BasicAdmin.ViewModels;
using MathSite.Common;
using MathSite.Common.ActionResults;
using MathSite.Common.Crypto;
using MathSite.Common.FileFormats;
using MathSite.Common.FileStorage;
using MathSite.Core.Auth.Handlers;
using MathSite.Core.Auth.Requirements;
using MathSite.Db;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Facades;
using MathSite.Repository.Core;
using MathSite.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleMvcSitemap;

namespace MathSite
{
    // ReSharper disable once ClassNeverInstantiated.Global
    /// <summary>
    ///     Класс-загрузчик.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        ///     Конфигурация сервисов для разработки.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services, true);
        }

        /// <summary>
        ///     Конфигурация сервисов для боевого сайта.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services, false);
        }

        /// <summary>
        ///     Конфигурация сервисов для тестирования.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services, false);
        }

        /// <summary>
        ///     Конфигурирование DI и настройка сервисов.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="isDevelopment"></param>
        private void ConfigureServices(IServiceCollection services, bool isDevelopment)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddRouting(options => { options.LowercaseUrls = true; });
            services.AddMemoryCache();

            ConfigureEntityFramework(services, isDevelopment);
            ConfigureDependencyInjection(services);
            ConfigureAuthPolices(services);

            services.AddSignalR();

            ConfigureRazorViews(services);
        }

        private static void ConfigureRazorViews(IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/{2}/Views/Shared/{0}.cshtml");

                options.AreaViewLocationFormats.Add("/Areas/Manager/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Areas/Manager/Views/Shared/{0}.cshtml");
            });
        }

        private static void ConfigureAuthPolices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    var expiration = TimeSpan.FromDays(365);

                    options.Cookie.Name = "YSU.Math.Auth";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Path = "/";
                    options.Cookie.Expiration = expiration;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                    options.Cookie.SameSite = SameSiteMode.None;

                    options.ExpireTimeSpan = expiration;

                    options.AccessDeniedPath = new PathString("/error/forbidden/");
                    options.LoginPath = new PathString("/login/");
                    options.LogoutPath = new PathString("/logout/");
                    options.ReturnUrlParameter = "returnUrl";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin",
                    builder => builder.Requirements.Add(new SiteSectionAccess(RightAliases.AdminAccess)));
                options.AddPolicy("peronal-page",
                    builder => builder.Requirements.Add(new SiteSectionAccess(RightAliases.PanelAccess)));

                options.AddPolicy("logout",
                    builder => builder.Requirements.Add(new SiteSectionAccess(RightAliases.LogoutAccess)));
            });

            services.AddScoped<IAuthorizationHandler, SiteSectionAccessHandler>();
        }

        private void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddLogging();

            services.AddOptions();

            services.AddLazyProvider();

            services.AddSingleton(Configuration);
            services.AddSingleton<IConfiguration>(Configuration);
            services.Configure<Settings>(Configuration);

            services.AddScoped<IPasswordsManager, DoubleSha512HashPasswordsManager>();
            services.AddSingleton<IActionDescriptorCollectionProvider, ActionDescriptorCollectionProvider>();

            services.AddSingleton<FileFormatBuilder>();
            
            services.AddRepositories()
                .AddFacades()
                .AddViewModelBuilders()
                .AddBasicAdminViewModelBuilders();

            services.AddStorage<LocalFileSystemStorage>();

            services.AddSingleton<ISitemapProvider, SitemapProvider>();

            services.AddActionResultExecutors();

            // for uploading really large files.
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

            ConfigureDataProtection(services);
        }

        private void ConfigureDataProtection(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            
            var environment = serviceProvider.GetRequiredService<IHostingEnvironment>();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            
            var logger = loggerFactory.CreateLogger("DataProtectionKey");


            var path = Path.Combine(new DirectoryInfo($"{environment.ContentRootPath}").Parent?.FullName, "keys");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            logger.LogInformation($"Using keys from: {path}");

            services.AddDataProtection()
                .SetApplicationName("MathSite")
                .SetDefaultKeyLifetime(TimeSpan.FromDays(28))
                .PersistKeysToFileSystem(new DirectoryInfo(path))
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA512
                });
        }

        private void ConfigureEntityFramework(IServiceCollection services, bool isDevelopment)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContextPool<MathSiteDbContext>(options =>
                {
                    options.UseNpgsql(
                        Configuration.GetConnectionString("Math"),
                        builder => builder.MigrationsAssembly("MathSite")
                    );

                    if (isDevelopment)
                        options.EnableSensitiveDataLogging().ConfigureWarnings(builder => builder.Log());
                }, 500);
        }
    }
}