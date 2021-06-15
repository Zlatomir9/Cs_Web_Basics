namespace MyWebServer.App.Controllers
{
    using MyWebServer.App.Models.Animals;
    using MyWebServer.MyHttpServer.Controllers;
    using MyWebServer.MyHttpServer.Http;

    public class DogsController : Controller
    {
        [HttpGet]
        public HttpResponse Create() => View();

        public HttpResponse Create(DogFormModel model)
            => Text($"Dog: {model.Name} - {model.Age} - {model.Breed}");
    }
}
