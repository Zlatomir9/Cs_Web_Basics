namespace Git.Services
{
    using Git.Models;
    using Git.Models.Repository;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateRepository(CreateRepositoryFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < RepositoryNameMinLength || model.Name.Length > RepositoryNameMaxLength)
            {
                errors.Add($"Repository name should be between {RepositoryNameMinLength} and {RepositoryNameMaxLength} characters.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(UserRegisterFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UsernameMinLength || model.Username.Length > UsernameAndPasswordMaxLength)
            {
                errors.Add($"Username must be between {UsernameMinLength} and {UsernameAndPasswordMaxLength} characters long.");
            }
            if (!Regex.IsMatch(model.Email, EmailRegex))
            {
                errors.Add($"Email {model.Email} is invalid e-mail address!");
            }
            if (model.Password.Length < PasswordMinLength || model.Password.Length > UsernameAndPasswordMaxLength)
            {
                errors.Add($"Password should be between {PasswordMinLength} and {UsernameAndPasswordMaxLength} characters");
            }
            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Passwords do not match!");
            }

            return errors;
        }
    }
}
