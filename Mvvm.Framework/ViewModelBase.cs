using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Framework
{
    /// <summary>
    /// A base class for all the viewmodels. Helps registering change notifications.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The <see cref="IModelChangeNotifier"/> that helps dispatching change notifications
        /// for relevant model changes.
        /// </summary>
        protected IModelChangeNotifier ModelChangeNotifier { get; }

        private PropertyInfo[] cachedProperties;

        /// <summary>
        /// Initializes a new <see cref="ViewModelBase"/>.
        /// </summary>
        /// <param name="modelChangeNotifier">The <see cref="IModelChangeNotifier"/> that helps dispatching the
        /// change notifications for relevant model changes.</param>
        /// <param name="watchedModels">The models relevant to this viewmodel.</param>
        public ViewModelBase(IModelChangeNotifier modelChangeNotifier, params object[] watchedModels)
        {
            cachedProperties = GetRelevantProperties();
            ModelChangeNotifier = modelChangeNotifier;
            foreach (var model in watchedModels) ModelChangeNotifier.Subscribe(model, NotifyAllPropertyChanges);
        }

        /// <summary>
        /// Retrieves the relevant properties that should be notified for change.
        /// Called once upon construction.
        /// </summary>
        /// <returns>The <see cref="PropertyInfo"/>s of the relevant properties.</returns>
        protected virtual PropertyInfo[] GetRelevantProperties() => GetType().GetProperties();

        /// <summary>
        /// Triggers a property changed event for each relevant property.
        /// </summary>
        protected virtual void NotifyAllPropertyChanges()
        {
            foreach (var prop in cachedProperties) NotifyPropertyChanged(prop.Name);
        }

        /// <summary>
        /// Triggers a property changed event for the property with the given name.
        /// </summary>
        /// <param name="name">The name of the changed property to send the notification about.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
