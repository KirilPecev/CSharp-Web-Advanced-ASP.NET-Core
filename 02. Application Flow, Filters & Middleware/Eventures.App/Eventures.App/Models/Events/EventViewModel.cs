namespace Eventures.App.Models.Events
{
    using System;

    public class EventViewModel
    {
        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Place { get; set; }
    }
}
