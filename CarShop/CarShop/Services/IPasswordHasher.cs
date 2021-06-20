namespace CarShop.Services
{
    public interface IPasswordHasher
    {
        string HashPasword(string password);
    }
}
