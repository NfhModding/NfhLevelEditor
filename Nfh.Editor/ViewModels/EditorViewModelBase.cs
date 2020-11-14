using Mvvm.Framework.ViewModel;
using Nfh.Editor.Commands.ModelCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.ViewModels
{
    public abstract class EditorViewModelBase : ViewModelBase
    {
        protected EditorViewModelBase(params object[] watchedModels)
            : base(Services.ModelChangeNotifier, watchedModels)
        {
        }

        protected void ChangeProperty(object model, object? newValue, [CallerMemberName] string propertyName = "")
        {
            var prop = model.GetType().GetProperty(propertyName);
            if (prop == null)
            {
                throw new ArgumentException("Property name is not in the object!", nameof(propertyName));
            }
            ChangeProperty(model, prop, newValue);
        }

        protected void ChangeProperty(object model, PropertyInfo propertyInfo, object? newValue)
        {
            var command = new PropertyChangeCommand(model, propertyInfo, newValue);
            Services.UndoRedo.Execute(command);
        }
    }
}
