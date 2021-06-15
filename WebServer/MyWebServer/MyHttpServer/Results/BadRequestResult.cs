namespace MyWebServer.MyHttpServer.Results
{
    using MyWebServer.MyHttpServer.Http;

    public class BadRequestResult : HttpResponse
    {
        public BadRequestResult() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
