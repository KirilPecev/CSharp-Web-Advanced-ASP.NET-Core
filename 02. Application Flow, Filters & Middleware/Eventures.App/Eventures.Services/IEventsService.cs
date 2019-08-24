namespace Eventures.Services
{
    using Eventures.Services.DTOs.Events;
    using System.Collections.Generic;

    public interface IEventsService
    {
        IEnumerable<EventDTO> GetAll();

        void Create(EventCreateDTO currentEvent);
    }
}
