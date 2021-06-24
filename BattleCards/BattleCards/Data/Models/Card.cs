namespace BattleCards.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class Card
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CardNameMaxLength)]
        public string Name { get; init; }

        [Required]
        public string ImageUrl { get; init; }

        [Required]
        public string Keyword { get; init; }

        public int Attack { get; init; }

        public int Health { get; init; }

        [Required]
        [MaxLength(CardDescriptionMaxLength)]
        public string Description { get; init; }

        public IEnumerable<UserCard> UserCards { get; init; } = new List<UserCard>();
    }
}
