namespace Eventures.App.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using System;

    public class AdminLogsActionFilter : IActionFilter
    {
        private readonly ILogger<AdminLogsActionFilter> logger;

        public AdminLogsActionFilter(ILogger<AdminLogsActionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var currentDateAndTime = DateTime.UtcNow;
            var username = context.HttpContext.User.Identity.Name;
            var eventName = context.HttpContext.Request.Form["Name"];
            var eventStart = context.HttpContext.Request.Form["Start"];
            var eventEnd = context.HttpContext.Request.Form["End"];

            this.logger.LogInformation($"[{currentDateAndTime}] Administrator {username} create event {eventName} ({eventStart} / {eventEnd}).");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
