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
    public class PropertyChangeCommand : ModelChangeCommand
    {
        public object Model { get; }
        public object Target { get; }
        public PropertyInfo PropertyInfo { get; }

        private object? newValue;

        public PropertyChangeCommand(object model, object target, PropertyInfo propertyInfo, object? newValue)
            : base(Services.ModelChangeNotifier, model)
        {
            Model = model;
            Target = target;
            PropertyInfo = propertyInfo;
            this.newValue = newValue;
        }

        public PropertyChangeCommand(object model, PropertyInfo propertyInfo, object? newValue)
            : this(model, model, propertyInfo, newValue)
        {
        }

        protected override void ExecuteWithoutNotify() => PropertyInfo.SetValue(Target, newValue);

        public override IModelChangeCommand GetUndoCommand() => 
            new PropertyChangeCommand(Model, Target, PropertyInfo, PropertyInfo.GetValue(Target));
    }
}
