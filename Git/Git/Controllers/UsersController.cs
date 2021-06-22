namespace Git.Controllers
{
    using Git.Data;
    using Git.Models;
    using Git.Models.User;
    using Git.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class UsersController : Controller
    {
        private readonly GitDbContext _data;
        private readonly IValidator _validator;
        private readonly IUsersService _usersService;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(GitDbContext data, IValidator validator, IUsersService usersService, IPasswordHasher passwordHasher)
        {
            this._data = data;
            this._validator = validator;
            this._usersService = usersService;
            this._passwordHasher = passwordHasher;
        }

        public HttpResponse Register() 
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Repositories/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterFormModel model)
        {
            var modelErrors = this._validator.ValidateUser(model);

            if (this._usersService.IsUsernameAvailable(model.Username))
            {
                modelErrors.Add($"Username {model.Username} already exists.");
            }
            if (this._usersService.IsEmailAvailable(model.Email))
            {
                modelErrors.Add($"User with e-mail {model.Email} is already registred.");
            }
            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var hashedPassword = this._passwordHasher.HashPasword(model.Password);

            this._usersService.CreateUser(model.Username, model.Email, hashedPassword);

            return Redirect("/Users/Login");
        }

        public HttpResponse Login() 
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/Repositories/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserViewModel model)
        {
            var hashedPassword = this._passwordHasher.HashPasword(model.Password);

            var userId = this._data.Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is invalid!");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
