using Image.Tga;
using Microsoft.Extensions.DependencyInjection;
using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Interfaces;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using Nfh.Editor.Commands.ModelCommands;
using Nfh.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Nfh.Editor
{
    // TODO: This is a hack, we basically make them globals
    internal static class Services
    {
        public static IBackupService Backup { get; private set; }
        public static IGameLocator GameLocator { get; private set; }
        public static IImageService Image { get; private set; }
        public static IProjectService Project { get; private set; }

        static Services()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDomainServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            Backup = serviceProvider.GetRequiredService<IBackupService>();
            GameLocator = serviceProvider.GetRequiredService<IGameLocator>();
            Image = serviceProvider.GetRequiredService<IImageService>();
            Project = serviceProvider.GetRequiredService<IProjectService>();
        }
    }
}
