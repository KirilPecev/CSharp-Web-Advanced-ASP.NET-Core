namespace Eventures.Services
{
    using Eventures.Data;
    using Eventures.Domain;
    using Eventures.Services.DTOs.Events;
    using System.Collections.Generic;
    using System.Linq;

    public class EventsService : IEventsService
    {
        private readonly EventuresDbContext context;

        public EventsService(EventuresDbContext context)
        {
            this.context = context;
        }

        public void Create(EventCreateDTO currentEvent)
        {
            this.context.Events.Add(new Event()
            {
                Name = currentEvent.Name,
                Place = currentEvent.Place,
                Start = currentEvent.Start,
                End = currentEvent.End,
                PricePerTicket = currentEvent.PricePerTicket,
                TotalTickets = currentEvent.TotalTickets
            });

            this.context.SaveChanges();
        }

        public IEnumerable<EventDTO> GetAll()
        {
            return this.context.Events.Select(x => new EventDTO()
            {
                Name = x.Name,
                Place = x.Place,
                Start = x.Start,
                End = x.End
            });
        }
    }
}
