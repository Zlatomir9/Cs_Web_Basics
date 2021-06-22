namespace Git
{
    using Git.Data;
    using Git.Services;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static async Task Main(string[] args)
            => await HttpServer
                .WithRoutes(routes => routes
                .MapStaticFiles()
                .MapControllers())
            .WithServices(services => services
                .Add<IViewEngine, CompilationViewEngine>()
                .Add<IPasswordHasher, PasswordHasher>()
                .Add<IUsersService, UsersService>()
                .Add<IValidator, Validator>()
                .Add<GitDbContext>())
            .WithConfiguration<GitDbContext>(c => c.Database.Migrate())
        .Start();        
    }
}
