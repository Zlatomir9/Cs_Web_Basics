namespace CarShop.Data
{
    public class DataConstants
    {
        public const int UserNameMinLength = 4;
        public const int UserNameMaxLength = 20;
        public const string EmailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                            + "@"
                                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
        public const int PasswordMinLength = 4;
        public const int PasswordMaxLength = 20;
        public const string UserTypeClient = "Client";
        public const string UserTypeMechanic = "Mechanic";

        public const int CarModelMinLength = 5;
        public const int CarModelMaxLength = 20;
        public const int CarModelMinYear = 1950;
        public const int CarModelMaxYear = 2021;
        public const string CarPlateNumberRegex = @"[A-Z]{2}[0-9]{4}[A-Z]{2}";
        public const int IssueDescriptionMinLength = 4;
    }
}
