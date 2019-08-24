namespace Eventures.App.Models.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EventCreateInputModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Invalid place! Must be at least {1} symbols!")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Invalid place! Cannot be empty!")]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "TotalTickets")]
        public int TotalTickets { get; set; }

        [Required]
        [Range(typeof(decimal), "0.00", "100000")]
        [Display(Name = "PricePerTicket")]
        public decimal PricePerTicket { get; set; }
    }
}
