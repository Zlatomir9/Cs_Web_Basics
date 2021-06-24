namespace BattleCards
{
    using BattleCards.Data;
    using BattleCards.Services;
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
                .Add<IValidator, Validator>()
                .Add<BattleCardsDbContext>())
            .WithConfiguration<BattleCardsDbContext>(c => c.Database.Migrate())
        .Start();
    }
}
