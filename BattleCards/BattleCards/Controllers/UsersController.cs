namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.Data.Models;
    using BattleCards.Models;
    using BattleCards.Models.Users;
    using BattleCards.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly BattleCardsDbContext data;
        private readonly IPasswordHasher passwordHasher;
        private readonly IValidator validator;

        public UsersController(BattleCardsDbContext data, IPasswordHasher passwordHasher, IValidator validator)
        {
            this.data = data;
            this.passwordHasher = passwordHasher;
            this.validator = validator;
        }

        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginFromModel model)
        {
            var hashedPassword = this.passwordHasher.HashPasword(model.Password);

            var userId = this.data.Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error($"Username and password combination is invalid");
            }

            this.SignIn(userId);

            return Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterFormModel model)
        {
            var modelErrors = this.validator.ValidateUser(model);

            if (this.data.Users.Any(x => x.Username == model.Username))
            {
                modelErrors.Add($"Username {model.Username} already exists.");
            }
            if (this.data.Users.Any(x => x.Email == model.Email))
            {
                modelErrors.Add($"User with e-mail {model.Email} is already registred.");
            }
            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPasword(model.Password),
                Email = model.Email
            };

            this.data.Users.Add(user);

            this.data.SaveChanges();

            return Redirect("/Users/Login");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
