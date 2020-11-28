using Nfh.Editor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Nfh.Editor.Selectors
{
    public class LevelObjectDataTemplateSelector : DataTemplateSelector
    {
        private static readonly string actorTemplate = "actorDataTemplate";
        private static readonly string doorTemplate = "doorDataTemplate";
        private static readonly string levelObjecTemplate = "levelObjectDataTemplate";

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement? element = container as FrameworkElement;
            if (element != null && item != null && item is LevelObjectViewModel levelObject)
            {
                if (levelObject is ActorViewModel) return element.FindResource(actorTemplate) as DataTemplate;
                if (levelObject is DoorViewModel) return element.FindResource(doorTemplate) as DataTemplate;
                return element.FindResource(levelObjecTemplate) as DataTemplate;
            }
            return null;
        }
    }
}
