namespace Git.Services
{
    using Git.Data;
    using Git.Data.Models;
    using System.Linq;

    public class UsersService : IUsersService
    {
        private readonly GitDbContext _data;
        private readonly IPasswordHasher _passwordHasher;

        public UsersService(GitDbContext data, IPasswordHasher passwordHasher)
        {
            this._data = data;
            this._passwordHasher = passwordHasher;
        }

        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            this._data.Users.Add(user);
            this._data.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = this._passwordHasher.HashPasword(password);

            var userId = this._data.Users
                .Where(u => u.Username == username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            return userId;
        }

        public bool IsEmailAvailable(string email)
            => this._data.Users.Any(e => e.Email == email);
        
        public bool IsUsernameAvailable(string username)
            => this._data.Users.Any(u => u.Username == username);
    }
}
