using Microsoft.Extensions.DependencyInjection;

namespace Nfh
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var img = TgaImage.FromFile(@"c:\TMP\NeighborsFromHell_Assets\tutorial_1.tga");
            img.ToBitmap().Save(@"c:\TMP\NeighborsFromHell_Assets\tutorial_1.png");*/
            /*var gameLocator = new GameLocator();
            foreach (var location in gameLocator.GetGameLocations())
                System.Console.WriteLine(location);*/

            //Playground.BackupGameData();
            var startup = new Startup();

            using var scope = startup.ServiceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<Playground>().Run();            
		}
    }
}
