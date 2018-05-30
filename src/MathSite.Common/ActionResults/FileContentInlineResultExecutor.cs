﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace MathSite.Common.ActionResults
{
    public class FileContentInlineResultExecutor : FileContentResultExecutor
    {
        public FileContentInlineResultExecutor(ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
        }

        protected override (RangeItemHeaderValue range, long rangeLength, bool serveBody) SetHeadersAndLog(
            ActionContext context,
            FileResult result,
            long? fileLength,
            DateTimeOffset? lastModified = null,
            EntityTagHeaderValue etag = null,
            bool enableRangeProcessing = true
        )
        {
            var data = base.SetHeadersAndLog(context, result, fileLength, lastModified, etag, enableRangeProcessing);

            if (context.HttpContext.Response.Headers.TryGetValue(HeaderNames.ContentDisposition, out var headerValue))
            {
                context.HttpContext.Response.Headers[HeaderNames.ContentDisposition] = headerValue.ToString().Replace("attachment;", "inline;");
            }

            context.HttpContext.Response.Headers["Cache-Control"] = "public, max-age=31536000";

            return data;
        }
    }
}