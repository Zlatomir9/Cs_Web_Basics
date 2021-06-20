namespace CarShop.Services
{
    using CarShop.Model.Cars;
    using CarShop.Model.Issues;
    using CarShop.Model.Users;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateCar(AddCarFormModel model);

        ICollection<string> ValidateIssue(AddIssueFormModel model);
    }
}
