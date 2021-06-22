namespace Git.Services
{
    using Git.Models;
    using Git.Models.Repository;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterFormModel model);

        ICollection<string> ValidateRepository(CreateRepositoryFormModel model);
    }
}
