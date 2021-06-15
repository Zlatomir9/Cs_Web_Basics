namespace MyWebServer.App.Controllers
{
    using MyWebServer.App.Models.Animals;
    using MyWebServer.MyHttpServer.Controllers;
    using MyWebServer.MyHttpServer.Http;

    public class AnimalsController : Controller
    {
        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";

            var querry = this.Request.Query;

            var catName = querry.ContainsKey(nameKey)
                ? querry[nameKey]
                : "the cats";

            var catAge = querry.ContainsKey(ageKey)
                ? int.Parse(querry[ageKey])
                : 0;

            var viewModel = new CatViewModel
            {
                Name = catName,
                Age = catAge
            };

            return View(viewModel);
        }

        public HttpResponse Dogs() => View(new DogViewModel
        {
            Name = "Rex",
            Age = 3,
            Breed = "Street Perfect"
        });

        public HttpResponse Bunnies() => View("Rabbits");

        public HttpResponse Turtles() => View("Animals/Wild/Turtles");
    }
}
