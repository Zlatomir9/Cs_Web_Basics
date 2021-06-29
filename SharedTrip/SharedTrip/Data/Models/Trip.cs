namespace SharedTrip.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Trip
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string StartPoint { get; init; }

        [Required]
        public string EndPoint { get; init; }

        public DateTime DepartureTime { get; init; }

        public int Seats { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; init; }

        public string ImagePath { get; init; }

        public ICollection<UserTrip> UserTrips { get; init; } = new List<UserTrip>();
    }
}
