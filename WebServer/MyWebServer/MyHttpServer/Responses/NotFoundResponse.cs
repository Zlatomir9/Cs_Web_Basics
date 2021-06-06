namespace MyWebServer.MyHttpServer.Responses
{
    using MyWebServer.MyHttpServer.Http;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse() 
            : base(HttpStatusCode.NotFound)
        {
        }
    }
}
