using Mvvm.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Editor.Commands.ModelCommands
{
    public class PropertyChangeCommand : IModelChangeCommand
    {
        private object model;
        private object target;
        private PropertyInfo propertyInfo;
        private object? newValue;

        public PropertyChangeCommand(object model, object target, PropertyInfo propertyInfo, object? newValue)
        {
            this.model = model;
            this.target = target;
            this.propertyInfo = propertyInfo;
            this.newValue = newValue;
        }

        public PropertyChangeCommand(object model, PropertyInfo propertyInfo, object? newValue)
            : this(model, model, propertyInfo, newValue)
        {
        }

        public void Execute()
        {
            propertyInfo.SetValue(target, newValue);
            Services.ModelChangeNotifier.Notify(model);
        }

        public IModelChangeCommand GetUndoCommand() => 
            new PropertyChangeCommand(model, target, propertyInfo, propertyInfo.GetValue(target));
    }
}
