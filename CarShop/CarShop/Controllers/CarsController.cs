namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Model.Cars;
    using CarShop.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class CarsController : Controller
    {
        private readonly IValidator _validator;
        private readonly IUserService _userService;
        private readonly CarShopDbContext _data;

        public CarsController(IValidator validator, CarShopDbContext data, IUserService userService)
        {
            this._validator = validator;
            this._userService = userService;
            this._data = data;
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this._userService.IsMechanic(this.User.Id))
            {
                return Error($"Only clients can add cars!");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddCarFormModel model)
        {
            var modelErrors = this._validator.ValidateCar(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            this._data.Cars.Add(car);

            this._data.SaveChanges();

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var carsQuery = this._data.Cars.AsQueryable();

            if (this._userService.IsMechanic(this.User.Id))
            {
                carsQuery = carsQuery.Where(c => c.Issues.Any(i => !i.IsFixed));
            }
            else
            {
                carsQuery = carsQuery.Where(c => c.OwnerId == this.User.Id);
            }

            var cars = carsQuery
                .Select(c => new CarListingViewModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    PlateNumber = c.PlateNumber,
                    Image = c.PictureUrl,
                    Year = c.Year,
                    FixedIssues = c.Issues.Where(i => i.IsFixed).Count(),
                    RemainingIssues = c.Issues.Where(i => !i.IsFixed).Count()
                })
                .ToList();

            return View(cars);
        }
    }
}
