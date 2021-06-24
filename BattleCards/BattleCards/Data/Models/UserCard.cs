namespace BattleCards.Data.Models
{
    public class UserCard
    {
        public string UserId { get; init; }

        public User User { get; init; }

        public int CardId { get; init; }

        public Card Card { get; init; }
    }
}
