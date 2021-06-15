namespace MyWebServer.MyHttpServer.Controllers
{
    using MyWebServer.MyHttpServer.Http;

    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute() 
            : base(HttpMethod.Post)
        {
        }
    }
}
