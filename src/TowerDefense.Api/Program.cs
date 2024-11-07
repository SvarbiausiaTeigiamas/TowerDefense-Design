using TowerDefense.Api.Builder;

class Program {
    public static void Main(string[] args)
    {
        var appDirector = new AppDirector(new TestWebAppBuilder(args));
        var app = appDirector.ConstructApp();
        app.Run();
    }
}