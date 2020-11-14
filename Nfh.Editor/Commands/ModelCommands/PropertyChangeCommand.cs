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
        private PropertyInfo propertyInfo;
        private object? newValue;

        public PropertyChangeCommand(object model, PropertyInfo propertyInfo, object? newValue)
        {
            this.model = model;
            this.propertyInfo = propertyInfo;
            this.newValue = newValue;
        }

        public void Execute()
        {
            propertyInfo.SetValue(model, newValue);
            Services.ModelChangeNotifier.Notify(model);
        }

        public IModelChangeCommand GetUndoCommand() => 
            new PropertyChangeCommand(model, propertyInfo, propertyInfo.GetValue(model));
    }
}
