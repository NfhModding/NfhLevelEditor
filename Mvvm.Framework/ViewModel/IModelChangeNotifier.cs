using System;
using System.Collections.Generic;
using System.Text;

namespace Mvvm.Framework.ViewModel
{
    /// <summary>
    /// A dispatcher that runs some action on certain model changes.
    /// </summary>
    public interface IModelChangeNotifier
    {
        /// <summary>
        /// Associates the given model with a given action to run. Models can be associated with 
        /// multiple actions.
        /// </summary>
        /// <param name="watchedModel">The model to associate.</param>
        /// <param name="onChange">The <see cref="Action"/> to run when the relevant model changes.</param>
        public void Subscribe(object watchedModel, Action onChange);
        /// <summary>
        /// Notifies that the given model was changed and the associated actions will run accordingly.
        /// </summary>
        /// <param name="changedModel">The model that was changes.</param>
        public void Notify(object changedModel);
        /// <summary>
        /// Utility to notify changes on multiple models.
        /// </summary>
        /// <param name="changedModels">The models that were changes.</param>
        public void Notify(IEnumerable<object> changedModels)
        {
            foreach (var model in changedModels) Notify(model);
        }
    }
}
