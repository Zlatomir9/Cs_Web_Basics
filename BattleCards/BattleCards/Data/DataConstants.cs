namespace BattleCards.Data
{
    public class DataConstants
    {
        public const int UsernamePasswordMaxLength = 20;
        public const int CardNameMaxLength = 15;
        public const int NamesMinLength = 5;
        public const int PasswordMinLength = 5;
        public const int CardDescriptionMaxLength = 200;
        public const string EmailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                            + "@"
                                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
    }
}
