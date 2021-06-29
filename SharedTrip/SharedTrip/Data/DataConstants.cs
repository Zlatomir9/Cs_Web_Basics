namespace SharedTrip.Data
{
    public class DataConstants
    {
        public const int DefaultMaxLength = 20;
        public const int UsernameMinLength = 5;
        public const int PasswordMinLength = 6;
        public const int SeatsMinLength = 2;
        public const int SeatsMaxLength = 6;
        public const int DescriptionMaxLength = 80;
        public const string EmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public const string DepartureTimeFormat = "dd.MM.yyyy HH:mm";
    }
}
