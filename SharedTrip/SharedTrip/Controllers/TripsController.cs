namespace SharedTrip.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Models.Trips;
    using SharedTrip.Services;
    using System;
    using System.Globalization;
    using System.Linq;
    using static Data.DataConstants;

    public class TripsController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public TripsController(
            IValidator validator,
            ApplicationDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse All()
        {
            var trips = this.data.Trips
                .Select(c => new TripsListingViewModel
                {
                    Id = c.Id,
                    StartPoint = c.StartPoint,
                    EndPoint = c.EndPoint,
                    DepartureTime = c.DepartureTime.ToString($"dd.MM.yyyy г. HH:mm"),
                    Seats = c.Seats.ToString()
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddTripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Redirect("/Trips/Add");
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.ParseExact(model.DepartureTime, $"{DepartureTimeFormat}", CultureInfo.InvariantCulture),
                Description = model.Description,
                Seats = model.Seats,
                ImagePath = model.ImagePath
            };

            data.Trips.Add(trip);

            data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            if (!this.data.Trips.Any(x => x.Id == tripId))
            {
                return BadRequest();
            }

            var trip = this.data.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripDetailsViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString($"{DepartureTimeFormat}"),
                    Seats = t.Seats.ToString(),
                    Image = t.ImagePath,
                    Description = t.Description
                })
                .FirstOrDefault();

            return View(trip);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var trip = this.data.Trips
                .Where(t => t.Id == tripId)
                .FirstOrDefault();

            var user = this.data.Users
                .Where(u => u.Id == this.User.Id)
                .FirstOrDefault();

            if (trip == null || user == null)
            {
                return BadRequest();
            }

            if ((this.data.UserTrips.Any(x => x.TripId == tripId && x.UserId == user.Id))
                || (trip.Seats == 0))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            var userTrip = new UserTrip
            {
                Trip = trip,
                User = user
            };

            this.data.UserTrips.Add(userTrip);
            trip.Seats -= 1;

            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}
