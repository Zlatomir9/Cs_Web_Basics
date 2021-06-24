namespace BattleCards.Models.Cards
{
    public class CreateCardFormModel
    {
        public string Name { get; init; }

        public string Image { get; init; }

        public string Keyword { get; init; }

        public int Attack { get; init; }

        public int Health { get; init; }

        public string Description { get; init; }
    }
}
