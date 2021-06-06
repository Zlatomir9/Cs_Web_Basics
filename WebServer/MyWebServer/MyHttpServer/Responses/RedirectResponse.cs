namespace MyWebServer.MyHttpServer.Responses
{
    using MyWebServer.MyHttpServer.Http;

    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string location)
            : base(HttpStatusCode.Found)
            => this.Headers.Add("Location", location);
    }
}
