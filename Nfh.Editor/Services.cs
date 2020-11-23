using Image.Tga;
using Microsoft.Extensions.DependencyInjection;
using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Interfaces;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using Nfh.Editor.Commands.ModelCommands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Nfh.Editor
{
    internal static class Services
    {
        public static string? GamePath { get; set; }
        public static string? ProjectPath { get; set; }

        public static IBackupService Backup { get; private set; }
        public static IGameLocator GameLocator { get; private set; }
        public static IImageService Image { get; private set; }
        public static IProjectService Project { get; private set; }

        // Frontend
        public static IModelChangeNotifier ModelChangeNotifier { get; } = new ModelChangeNotifier();
        public static IUndoRedoStack UndoRedo { get; } = new UndoRedoStack
        {
            MergeStrategy = new CommandMergeStrategy(),
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            Backup = serviceProvider.GetRequiredService<IBackupService>();
            GameLocator = serviceProvider.GetRequiredService<IGameLocator>();
            Image = serviceProvider.GetRequiredService<IImageService>();
            Project = serviceProvider.GetRequiredService<IProjectService>();
        }
    }
}
