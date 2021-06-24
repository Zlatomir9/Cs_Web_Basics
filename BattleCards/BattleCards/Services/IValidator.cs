namespace BattleCards.Services
{
    using BattleCards.Models.Cards;
    using BattleCards.Models.Users;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterFormModel model);

        ICollection<string> ValidateCard(CreateCardFormModel model);
    }
}
