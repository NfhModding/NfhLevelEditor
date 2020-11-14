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
            Services.UndoRedo.Execute(command);
        }
    }
}
