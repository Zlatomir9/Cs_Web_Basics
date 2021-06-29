namespace SharedTrip.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class User
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        public ICollection<UserTrip> UserTrips { get; init; } = new List<UserTrip>();
    }
}
