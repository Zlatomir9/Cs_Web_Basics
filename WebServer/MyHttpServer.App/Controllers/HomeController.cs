namespace MyWebServer.App.Controllers
{
    using MyWebServer.MyHttpServer.Controllers;
    using MyWebServer.MyHttpServer.Http;
    using System;

    public class HomeController : Controller
    {
        public HttpResponse Index() => Text("Hello from the server!");

        public HttpResponse LocalRedirect() => Redirect("/Animals/Cats");

        public HttpResponse ToSoftUni() => Redirect("https://softuni.bg");

        public HttpResponse StaticFiles() => View();

        public HttpResponse Error() => throw new InvalidOperationException("Invalid action!");
    }
}
