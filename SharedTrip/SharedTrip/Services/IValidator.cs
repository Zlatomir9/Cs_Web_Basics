namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using SharedTrip.Models.Trips;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateTrip(AddTripFormModel model);
    }
}
