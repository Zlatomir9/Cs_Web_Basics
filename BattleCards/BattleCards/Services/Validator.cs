namespace BattleCards.Services
{
    using BattleCards.Models.Cards;
    using BattleCards.Models.Users;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateCard(CreateCardFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < NamesMinLength || model.Name.Length > CardNameMaxLength)
            {
                errors.Add($"Card name {model.Name} is invalid! Name should be between" +
                    $"{NamesMinLength} and {CardNameMaxLength} characters long.");
            }
            if (model.Description.Length > CardDescriptionMaxLength)
            {
                errors.Add($"Card description should be max {CardDescriptionMaxLength} characters long.");
            }
            if (model.Attack < 0)
            {
                errors.Add($"Card attack points can't be a negative number.");
            }
            if (model.Health < 0)
            {
                errors.Add($"Card health points can't be a negative number.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(UserRegisterFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < NamesMinLength || model.Username.Length > UsernamePasswordMaxLength)
            {
                errors.Add($"Username {model.Username} is invalid! Username should be between" +
                    $" {NamesMinLength} and {UsernamePasswordMaxLength} characters.");
            }
            if (!Regex.IsMatch(model.Email, EmailRegex))
            {
                errors.Add($"Email {model.Email} is invalid e-mail address!");
            }
            if (model.Password.Length < PasswordMinLength || model.Password.Length > UsernamePasswordMaxLength)
            {
                errors.Add($"Password should be between {PasswordMinLength} and {UsernamePasswordMaxLength} characters");
            }
            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Passwords do not match!");
            }

            return errors;
        }
    }
}
