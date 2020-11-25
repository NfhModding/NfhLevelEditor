using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Editor.Commands.ModelCommands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Nfh.Editor.ViewModels
{
    public abstract class EditorViewModelBase : ViewModelBase
    {
        private IUndoRedoStack undoRedoStack;

        protected EditorViewModelBase(IUndoRedoStack undoRedo, params object[] watchedModels)
            : base(MetaViewModel.Current.ModelChangeNotifier, watchedModels)
        {
            undoRedoStack = undoRedo;
        }

        protected void ChangeProperty(object model, object? newValue, [CallerMemberName] string propertyName = "")
        {
            object target = model;
            string[] parts = propertyName.Split('.');
            for (int i = 0; i < parts.Length - 1; i++)
            {
                var propInfo = target.GetType().GetProperty(parts[i]);
                if (propInfo == null)
                {
                    throw new ArgumentException("Property name is not in the object!", nameof(propertyName));
                }
                var subTarget = propInfo.GetValue(target);
                if (subTarget == null)
                {
                    throw new ArgumentException("Property target is null!", nameof(model));
                }
                target = subTarget;
            }
            var prop = target.GetType().GetProperty(parts[parts.Length - 1]);
            if (prop == null)
            {
                throw new ArgumentException("Property name is not in the object!", nameof(propertyName));
            }
            ChangeProperty(model, target, prop, newValue);
        }

        protected void ChangeProperty(object model, PropertyInfo propertyInfo, object? newValue) =>
            ChangeProperty(model, model, propertyInfo, newValue);

        protected void ChangeProperty(object model, object target, PropertyInfo propertyInfo, object? newValue)
        {
            var command = new PropertyChangeCommand(model, target, propertyInfo, newValue);
            undoRedoStack.Execute(command);
        }

        // Source: https://stackoverflow.com/questions/22499407/how-to-display-a-bitmap-in-a-wpf-image
        protected static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }
    }
}
