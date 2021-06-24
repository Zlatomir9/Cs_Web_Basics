namespace BattleCards.Services
{
    public interface IPasswordHasher
    {
        string HashPasword(string password);
    }
}
