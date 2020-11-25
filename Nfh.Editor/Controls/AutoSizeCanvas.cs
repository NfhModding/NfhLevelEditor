using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nfh.Editor.Controls
{
    public class AutoSizeCanvas : Canvas
    {
        private static double PropertyValueToDouble(object value)
        {
            if (value is double d) return d;
            return 0.0;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(constraint);

            double width = InternalChildren
                .OfType<UIElement>()
                .Select(i => i.DesiredSize.Width + PropertyValueToDouble(i.GetValue(LeftProperty)))
                .Append(0.0)
                .Max();
            double height = InternalChildren
                .OfType<UIElement>()
                .Select(i => i.DesiredSize.Height + PropertyValueToDouble(i.GetValue(TopProperty)))
                .Append(0.0)
                .Max();

            return new Size(width, height);
        }
    }
}
