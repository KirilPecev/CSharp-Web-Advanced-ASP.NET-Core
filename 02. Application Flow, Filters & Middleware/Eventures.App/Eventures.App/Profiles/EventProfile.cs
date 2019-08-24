namespace Eventures.App.Profiles
{
    using AutoMapper;
    using Eventures.App.Models.Events;
    using Eventures.Services.DTOs.Events;

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventDTO, EventViewModel>();
            CreateMap<EventCreateInputModel, EventCreateDTO>();
        }
    }
}
