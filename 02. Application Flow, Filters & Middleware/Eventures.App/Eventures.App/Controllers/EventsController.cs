namespace Eventures.App.Controllers
{
    using AutoMapper;
    using Eventures.App.Infrastructure.Filters;
    using Eventures.App.Models.Events;
    using Eventures.Services;
    using Eventures.Services.DTOs.Events;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEventsService eventsService;

        public EventsController(IMapper mapper, IEventsService eventsService)
        {
            this.mapper = mapper;
            this.eventsService = eventsService;
        }

        [Authorize(Roles = "Admin, User")]
        public IActionResult All()
        {
            var dto = this.eventsService.GetAll();
            var mappedModel = this.mapper.Map<EventViewModel[]>(dto);

            return this.View(mappedModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(AdminLogsActionFilter))]
        public IActionResult Create(EventCreateInputModel model)
        {
            if (!this.ModelState.IsValid || model.End < model.Start)
            {
                return this.View(model);
            }

            var mappedModel = this.mapper.Map<EventCreateDTO>(model);
            this.eventsService.Create(mappedModel);

            return this.Redirect("/");
        }
    }
}
