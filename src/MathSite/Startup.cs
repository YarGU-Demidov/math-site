using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MathSite
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    }
}