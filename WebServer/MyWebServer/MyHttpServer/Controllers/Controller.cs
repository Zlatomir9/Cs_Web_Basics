namespace MyWebServer.MyHttpServer.Controllers
{
    using MyWebServer.MyHttpServer.Http;
    using MyWebServer.MyHttpServer.Responses;
    using System.Runtime.CompilerServices;

    public abstract class Controller
    {
        protected Controller(HttpRequest request)
            => this.Request = request;

        protected HttpRequest Request { get; private init; }

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string text)
            => new HtmlResponse(text);

        protected HttpResponse Redirect(string text)
            => new RedirectResponse(text);

        protected HttpResponse View([CallerMemberName] string viewName = "")
            => new ViewResponse(viewName, this.GetControllerName());

        private string GetControllerName()
                => this.GetType().Name
                    .Replace(nameof(Controller), string.Empty);
    }
}
