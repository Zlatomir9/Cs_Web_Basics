namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Model;
    using CarShop.Model.Users;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;
    using static CarShop.Data.DataConstants;

    public class UsersController : Controller
    {
        private readonly IValidator _validator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly CarShopDbContext _data;

        public UsersController(IValidator validator, CarShopDbContext data, IPasswordHasher passwordHasher)
        {
            this._validator = validator;
            this._data = data;
            this._passwordHasher = passwordHasher;
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Cars/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this._validator.ValidateUser(model);

            if (this._data.Users.Any(x => x.Username == model.Username))
            {
                modelErrors.Add($"Username {model.Username} already exists.");
            }
            if (this._data.Users.Any(x => x.Email == model.Email))
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
                Password = this._passwordHasher.HashPasword(model.Password),
                Email = model.Email,
                IsMechanic = model.UserType == UserTypeMechanic
            };

            _data.Users.Add(user);

            _data.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Cars/All");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPassword = this._passwordHasher.HashPasword(model.Password);

            var userId = this._data.Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error($"Username and password combination is invalid");
            }

            this.SignIn(userId);

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse LogOut() 
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
