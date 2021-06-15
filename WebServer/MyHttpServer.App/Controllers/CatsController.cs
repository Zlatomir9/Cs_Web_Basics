namespace MyWebServer.App.Controllers
{
    using MyWebServer.MyHttpServer.Controllers;
    using MyWebServer.MyHttpServer.Http;

    public class CatsController : Controller
    {
        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Save(string name, int age)
            => Text($"{name} - {age}");
    }
}
