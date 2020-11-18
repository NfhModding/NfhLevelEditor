using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Nfh.Editor.Adorners
{
    public class ContentPresenterAdorner : Adorner
    {
        private VisualCollection visuals;
        private ContentPresenter contentPresenter;

        protected override int VisualChildrenCount => visuals.Count;
        protected override Visual GetVisualChild(int index) => visuals[index];

        public object Content
        {
            get => contentPresenter.Content;
            set => contentPresenter.Content = value;
        }

        public ContentPresenterAdorner(UIElement adornedElement)
          : base(adornedElement)
        {
            visuals = new VisualCollection(this);
            contentPresenter = new ContentPresenter();
            visuals.Add(contentPresenter);
        }

        public ContentPresenterAdorner(UIElement adornedElement, Visual content)
          : this(adornedElement)
        { 
            Content = content; 
        }

        protected override Size MeasureOverride(Size constraint)
        {
            contentPresenter.Measure(constraint);
            return contentPresenter.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            contentPresenter.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            return contentPresenter.RenderSize;
        }
    }
}
