namespace Git.Data
{
    public class DataConstants
    {
        public const int UsernameAndPasswordMaxLength = 20;
        public const int UsernameMinLength = 5;
        public const int PasswordMinLength = 6;
        public const int RepositoryNameMaxLength = 10;
        public const int RepositoryNameMinLength = 3;
        public const int CommitDescriptionMinLenth = 5;

        public const string EmailRegex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                            + "@"
                                            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
    }
}
