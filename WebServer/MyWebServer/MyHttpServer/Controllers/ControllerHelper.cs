namespace MyWebServer.MyHttpServer.Controllers
{
    using System;

    public static class ControllerHelper
    {
        public static string GetControllerName(this Type type)
            => type.Name.Replace(nameof(Controller), string.Empty);
    }
}
