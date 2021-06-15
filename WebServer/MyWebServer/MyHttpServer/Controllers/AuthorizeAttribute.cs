namespace MyWebServer.MyHttpServer.Controllers
{
    using System;
    
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeAttribute : Attribute
    {
    }
}
