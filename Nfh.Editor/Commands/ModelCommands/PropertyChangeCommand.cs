using Mvvm.Framework.ViewModel;
using Nfh.Editor.ViewModels;
using System.Reflection;

namespace Nfh.Editor.Commands.ModelCommands
{
    public class PropertyChangeCommand : ModelChangeCommand
    {
        public object Model { get; }
        public object Target { get; }
        public PropertyInfo PropertyInfo { get; }

        private object? newValue;

        public PropertyChangeCommand(object model, object target, PropertyInfo propertyInfo, object? newValue)
            : base(MetaViewModel.Current.ModelChangeNotifier, model)
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
