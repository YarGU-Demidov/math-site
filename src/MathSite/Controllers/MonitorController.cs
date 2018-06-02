using System.Collections.Generic;
using System.Linq;
using MathSite.Db.DataSeeding.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MathSite.Controllers
{
    [Authorize(RightAliases.AdminAccess)]
    [Route("monitor")]
    public class MonitorController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _provider;

        public MonitorController(IActionDescriptorCollectionProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("routes")]
        public IActionResult GetRoutes()
        {
            IEnumerable<(string Action, string Controller, string Name, string Template, string DisplayName)> routes = _provider.ActionDescriptors.Items.Select(x => 
            (
                x.RouteValues["Action"],
                x.RouteValues["Controller"],
                x.AttributeRouteInfo?.Name,
                x.AttributeRouteInfo?.Template,
                x.DisplayName
            ));

            var text = routes.Select(route =>
                $"DisplayName: {route.DisplayName}, \nAction: {route.Action}, \nController: {route.Controller}, \nTemplate: {route.Template}, \nName: {route.Name}")
                .Aggregate((f, s) => $"{f}\r\n\r\n{s}");

            return Ok(text);
        }
    }
}