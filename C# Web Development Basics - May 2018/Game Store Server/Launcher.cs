namespace WebServer
{
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using WebServer.GameStoreApplication;

    public class Launcher : IRunnable
    {
        private const int Port = 1337;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var gameStoreApplication = new GameStoreApp();
            gameStoreApplication.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            gameStoreApplication.Configure(appRouteConfig);

            var webServer = new WebServer(Port, appRouteConfig);
            webServer.Run();
        }
    }
}