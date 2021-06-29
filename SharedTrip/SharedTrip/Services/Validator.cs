namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using SharedTrip.Models.Trips;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateTrip(AddTripFormModel trip)
        {
            var errors = new List<string>();

            if (trip.Description == null || trip.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"Trip description must be max {DescriptionMaxLength} characters long.");
            }

            if (trip.Seats < SeatsMinLength || trip.Seats > SeatsMaxLength)
            {
                errors.Add($"Trip seats must be between {SeatsMinLength} and {SeatsMaxLength}.");
            }

            if (trip.DepartureTime == null || !DateTime.TryParseExact(trip.DepartureTime, $"{DepartureTimeFormat}",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                errors.Add($"Departure time should be in format '{DepartureTimeFormat}'");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel user)
        {
            var errors = new List<string>();

            if (user.Username == null || user.Username.Length < UsernameMinLength || user.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username must be between {UsernameMinLength} and {DefaultMaxLength} characters long.");
            }

            if (user.Email == null || !Regex.IsMatch(user.Email, EmailRegularExpression))
            {
                errors.Add($"Email '{user.Email}' is not a valid.");
            }

            if (user.Password == null || user.Password.Length < PasswordMinLength || user.Password.Length > DefaultMaxLength)
            {
                errors.Add($"Password must be between {PasswordMinLength} and {DefaultMaxLength} characters long.");
            }

            if (user.Password != null && user.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                errors.Add("Password and it's confirmation are not equal.");
            }

            return errors;
        }
    }
}
