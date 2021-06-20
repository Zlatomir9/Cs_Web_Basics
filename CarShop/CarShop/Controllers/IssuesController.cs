namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Model.Issues;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class IssuesController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator _validator;
        private readonly CarShopDbContext _data;

        public IssuesController(IUserService userService, CarShopDbContext data, IValidator validator)
        {
            this._userService = userService;
            this._validator = validator;
            this._data = data;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var carWithIssues = this._data.Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarIssuesViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    Issues = c.Issues.Select(i => new IssueListingViewModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        IsFixed = i.IsFixed
                    })
                })
                .FirstOrDefault();

            if (carWithIssues == null)
            {
                return Error($"Car with ID '{carId}' does not exist.");
            }

            return View(carWithIssues);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(string carId, AddIssueFormModel model)
        {
            var car = this._data.Cars.FirstOrDefault(x => x.Id == carId);

            if (car == null)
            {
                return Error("Car does not exist");
            }

            var modelErrors = this._validator.ValidateIssue(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var issue = new Issue
            {
                Description = model.Description,
                IsFixed = false,
                CarId = carId
            };

            _data.Issues.Add(issue);

            _data.SaveChanges();

            return this.Redirect($"CarIssues?CarId={carId}");
        }

        [Authorize]
        public HttpResponse Fix(string carId, string issueId)
        {         
            if (!this._userService.IsMechanic(this.User.Id))
            {
                return Error("Only mechanics can fix issues!");
            }

            var issue = this._data.Issues.FirstOrDefault(i => i.Id == issueId);

            if (issue == null)
            {
                return this.BadRequest();
            }

            issue.IsFixed = true;

            _data.SaveChanges();

            return this.Redirect($"CarIssues?CarId={carId}");
        }

        [Authorize]
        public HttpResponse Delete(string carId, string issueId)
        {
            var issue = this._data.Issues.FirstOrDefault(i => i.Id == issueId);

            if (issue == null)
            {
                return this.BadRequest();
            }

            _data.Issues.Remove(issue);

            _data.SaveChanges();

            return this.Redirect($"CarIssues?CarId={carId}");
        }
    }
}
