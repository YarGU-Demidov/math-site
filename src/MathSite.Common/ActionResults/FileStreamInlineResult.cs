using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace MathSite.Common.ActionResults
{
    public class FileStreamInlineResult : FileStreamResult
    {
        public FileStreamInlineResult(Stream fileStream, string contentType) : base(fileStream, contentType)
        {
        }

        public FileStreamInlineResult(Stream fileStream, MediaTypeHeaderValue contentType) : base(fileStream,
            contentType)
        {
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return context.HttpContext.RequestServices.GetRequiredService<FileStreamInlineResultExecutor>()
                .ExecuteAsync(context, this);
        }
    }
}