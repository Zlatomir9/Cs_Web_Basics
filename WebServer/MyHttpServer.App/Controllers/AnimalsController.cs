namespace MyWebServer.App.Controllers
{
    using MyWebServer.MyHttpServer.Controllers;
    using MyWebServer.MyHttpServer.Http;

    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";

            var querry = this.Request.Query;

            var catName = querry.ContainsKey(nameKey)
                ? querry[nameKey]
                : "the cats";

            var result = "<h2>Hello from the cats!</h2>";

            return Html(result);
        }

        public HttpResponse Dogs() => View();

        public HttpResponse Bunnies() => View("Rabbits");

        public HttpResponse Turtles() => View("Animals/Wild/Turtles");
    }
}
