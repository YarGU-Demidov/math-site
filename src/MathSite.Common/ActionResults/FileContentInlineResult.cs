using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace MathSite.Common.ActionResults
{
    public class FileContentInlineResult : FileContentResult
    {
        public FileContentInlineResult(byte[] fileContents, string contentType) 
            : base(fileContents, contentType)
        {
        }

        public FileContentInlineResult(byte[] fileContents, MediaTypeHeaderValue contentType) 
            : base(fileContents, contentType)
        {
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            return context.HttpContext.RequestServices.GetRequiredService<FileContentInlineResultExecutor>().ExecuteAsync(context, this);
        }
    }
}