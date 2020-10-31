using Microsoft.Win32;
using Steam.Acf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Nfh.Services
{
    internal class SteamGameLocationProvider : IGameLocationProvider
    {
        public DirectoryInfo Locate(string gameName)
        {
            var installationFolder = GetSteamInstallationFolder()
                ?? throw new Exception("Could not find the Steam installation folder"); // ToDo custom exception

            var appFolders = GetSteamAppFolders(installationFolder);
            var gameFolder = GetGameFolder(gameName, appFolders)
                ?? throw new Exception($"Could not find the {gameName} installation folder");

            return gameFolder;
        }

        private static DirectoryInfo? GetSteamInstallationFolder()
        {
            // Verify the code is running on Windows.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // The time of the development the Steam client is 32-bit, 
                // so if the code is running on a 64-bit machine it should be under the Wow6432Node folder in the registry
                var steamRegistryPath = RuntimeInformation.OSArchitecture switch
                {
                    Architecture.X86 => @"SOFTWARE\Valve\Steam",
                    Architecture.X64 => @"SOFTWARE\Wow6432Node\Valve\Steam",
                    _ => throw new NotImplementedException(),
                };

                using (var key = Registry.LocalMachine.OpenSubKey(steamRegistryPath))
                {
                    if (key?.GetValue("InstallPath") is string folderPath && Directory.Exists(folderPath))
                    {
                        return new DirectoryInfo(folderPath);
                    }
                }
            }
            return null;
        }

        private static IReadOnlyCollection<DirectoryInfo> GetSteamAppFolders(DirectoryInfo steamDirectory)
        {
            const string steamApps = "steamapps";

            // Only 1 Steam directory per drive
            var defaultAppsFolder = new DirectoryInfo(Path.Combine(steamDirectory.FullName, steamApps));

            if (!defaultAppsFolder.Exists)
            {
                throw new Exception("The default steam library folder could not be found");
            }

            var libraryFolderMeta = defaultAppsFolder.GetFiles(
                    searchPattern: "libraryfolders.vdf", searchOption: SearchOption.TopDirectoryOnly)
                .FirstOrDefault();

            if (libraryFolderMeta == null)
                return new List<DirectoryInfo> { defaultAppsFolder };

            var metaContent = File.ReadAllText(libraryFolderMeta.FullName);
            var libraryFolders = AcfFile.Parse(metaContent);

            return libraryFolders.Keys
                .Where(k => int.TryParse(k, out _)) // The additional library folders is identified by a number as a string e.g "1"
                .Select(k => libraryFolders[k])
                .Aggregate(new List<DirectoryInfo>(), (folders, acfEntry) =>
                {
                    // Check if the defined folder is exist
                    var path = Path.Combine(acfEntry.Value, steamApps);
                    if (Directory.Exists(path))
                        folders.Add(new DirectoryInfo(path));

                    return folders;
                })
                .Append(defaultAppsFolder)
                .ToList();
        }

        private static DirectoryInfo? GetGameFolder(string gameName, IReadOnlyCollection<DirectoryInfo> appFolders)
        {
            foreach (var (appFolder, acfFile) in appFolders
                .SelectMany(appFolder => 
                    appFolder
                        .GetFiles(searchPattern: "*.acf", SearchOption.TopDirectoryOnly)
                        .Select(acfFile => (appFolder, acfFile))))
            {
                var fileContent = File.ReadAllText(acfFile.FullName);
                var acfEntry = AcfFile.Parse(fileContent);
                
                if (acfEntry.TryGetValue("name", out var name) && name.Value == gameName)
                {
                    if (acfEntry.TryGetValue("installdir", out var installFolder))
                    {
                        var fullPath = Path.Combine(appFolder.FullName, "common", installFolder.Value);
                        var gameFolder = new DirectoryInfo(fullPath);
                        
                        if (gameFolder.Exists)
                            return gameFolder;
                        break;
                    }

                    break;
                }
            }           

            return null;
        }
    }
}
