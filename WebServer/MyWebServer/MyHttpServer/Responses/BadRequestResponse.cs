namespace MyWebServer.MyHttpServer.Responses
{
    using MyWebServer.MyHttpServer.Http;

    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse() 
            : base(HttpStatusCode.BadRequest)
        {

        }
    }
}
