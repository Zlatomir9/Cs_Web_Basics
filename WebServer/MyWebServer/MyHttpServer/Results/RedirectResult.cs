namespace MyWebServer.MyHttpServer.Results
{
    using MyWebServer.MyHttpServer.Http;

    public class RedirectResult : ActionResult
    {
        public RedirectResult(HttpResponse response, string location)
            : base(response)
        {
            this.StatusCode = HttpStatusCode.Found;

            this.AddHeader(HttpHeader.Location, location);
        }
    }
}
