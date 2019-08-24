namespace Eventures.Services.DTOs.Events
{
    using System;

    public class EventDTO
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Place { get; set; }
    }
}
