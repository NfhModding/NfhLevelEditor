using Steam.Acf;
using System;

namespace Nfh
{
	class Program
    {
        static string sourceText = @"
""AppState""
{
	""appid""		""1283190""
	""Universe""		""1""
	""name""		""Neighbours back From Hell""
	""StateFlags""		""4""
	""installdir""		""Neighbours back From Hell""
	""LastUpdated""		""1603622624""
	""UpdateResult""		""0""
	""SizeOnDisk""		""3892752796""
	""buildid""		""5687148""
	""LastOwner""		""76561198016017138""
	""BytesToDownload""		""2977920""
	""BytesDownloaded""		""2977920""
	""AutoUpdateBehavior""		""0""
	""AllowOtherDownloadsWhileRunning""		""0""
	""ScheduledAutoUpdate""		""0""
	""InstalledDepots""
	{
		""1283191""
		{
			""manifest""		""1738313319605739419""
			""size""		""3892752796""
		}
	}
	""UserConfig""
	{
		""language""		""english""
	}
} 
";

        static void Main(string[] args)
        {
			var obj = AcfFile.Parse(sourceText);
            Console.WriteLine(obj["installdir"]);
		}
    }
}
