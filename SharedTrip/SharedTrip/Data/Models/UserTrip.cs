namespace SharedTrip.Data.Models
{
    public class UserTrip
    {
        public string UserId { get; init; }

        public User User { get; init; }

        public string TripId { get; init; }

        public Trip Trip { get; init; }
    }
}
