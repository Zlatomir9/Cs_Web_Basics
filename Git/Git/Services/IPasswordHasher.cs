namespace Git.Services
{
    public interface IPasswordHasher
    {
        string HashPasword(string password);
    }
}
