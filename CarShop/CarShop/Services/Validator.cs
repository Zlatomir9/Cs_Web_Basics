namespace CarShop.Services
{
    using CarShop.Model.Cars;
    using CarShop.Model.Issues;
    using CarShop.Model.Users;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using static CarShop.Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateCar(AddCarFormModel model)
        {
            var errors = new List<string>();

            if (model.Model.Length < CarModelMinLength || model.Model.Length > CarModelMaxLength)
            {
                errors.Add($"Car model should be between {CarModelMinLength} and {CarModelMaxLength} characters.");
            }
            if (model.Year < CarModelMinYear || model.Year > CarModelMaxYear)
            {
                errors.Add($"Year {model.Year} is not valid. It must be between {CarModelMinYear} and {CarModelMaxYear}.");
            }
            if (!Regex.IsMatch(model.PlateNumber, CarPlateNumberRegex))
            {
                errors.Add($"Car plate number should be in format AA0000AA");
            }

            return errors;
        }

        public ICollection<string> ValidateIssue(AddIssueFormModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < IssueDescriptionMinLength)
            {
                errors.Add($"Issue description must be atleast {IssueDescriptionMinLength} characters long.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UserNameMinLength || model.Username.Length > UserNameMaxLength)
            {
                errors.Add($"Username {model.Username} is invalid! Username should be between" +
                    $" {UserNameMinLength} and {UserNameMaxLength} characters.");
            }
            if (!Regex.IsMatch(model.Email, EmailRegex))
            {
                errors.Add($"Email {model.Email} is invalid e-mail address!");
            }
            if (model.Password.Length < PasswordMinLength || model.Password.Length > PasswordMaxLength)
            {
                errors.Add($"Password should be between {PasswordMinLength} and {PasswordMaxLength} characters");
            }
            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Passwords do not match!");
            }
            if (model.UserType != UserTypeClient && model.UserType != UserTypeMechanic)
            {
                errors.Add($"User should be a {UserTypeClient} or {UserTypeMechanic}.");
            }

            return errors;
        }
    }
}
