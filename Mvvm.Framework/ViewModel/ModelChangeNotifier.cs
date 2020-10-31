using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvvm.Framework.ViewModel
{
    /// <summary>
    /// A simplistic implementation of <see cref="IModelChangeNotifier"/> with simple object references
    /// and dictionaries.
    /// </summary>
    public class ModelChangeNotifier : IModelChangeNotifier
    {
        private readonly Dictionary<object, List<Action>> modelsToActions = new();

        public void Subscribe(object watchedModel, Action onChange)
        {
            if (!modelsToActions.TryGetValue(watchedModel, out var actionList))
            {
                actionList = new();
                modelsToActions.Add(watchedModel, actionList);
            }
            actionList.Add(onChange);
        }

        public void Notify(object changedModel)
        {
            if (modelsToActions.TryGetValue(changedModel, out var actionList))
            {
                foreach (var action in actionList) action();
            }
        }
    }
}
