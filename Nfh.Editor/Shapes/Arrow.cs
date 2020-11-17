using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Nfh.Editor.Shapes
{
    public class Arrow : Shape
    {
		public static readonly DependencyProperty X1Property = DependencyProperty.Register(
			"X1", 
			typeof(double), 
			typeof(Arrow), 
			new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty Y1Property = DependencyProperty.Register(
			"Y1", 
			typeof(double), 
			typeof(Arrow), 
			new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty X2Property = DependencyProperty.Register(
			"X2", 
			typeof(double), 
			typeof(Arrow), 
			new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty Y2Property = DependencyProperty.Register(
			"Y2", 
			typeof(double), 
			typeof(Arrow), 
			new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty HeadWidthProperty = DependencyProperty.Register(
			"HeadWidth", 
			typeof(double), 
			typeof(Arrow), 
			new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
		public static readonly DependencyProperty HeadHeightProperty = DependencyProperty.Register(
			"HeadHeight", 
			typeof(double), 
			typeof(Arrow), 
			new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

		[TypeConverter(typeof(LengthConverter))]
		public double X1
		{
			get => (double)GetValue(X1Property);
			set => SetValue(X1Property, value);
		}
		[TypeConverter(typeof(LengthConverter))]
		public double Y1
		{
			get => (double)GetValue(Y1Property);
			set => SetValue(Y1Property, value);
		}
		[TypeConverter(typeof(LengthConverter))]
		public double X2
		{
			get => (double)GetValue(X2Property);
			set => SetValue(X2Property, value);
		}
		[TypeConverter(typeof(LengthConverter))]
		public double Y2
		{
			get => (double)GetValue(Y2Property);
			set => SetValue(Y2Property, value);
		}
		[TypeConverter(typeof(LengthConverter))]
		public double HeadWidth
		{
			get => (double)GetValue(HeadWidthProperty);
			set => SetValue(HeadWidthProperty, value);
		}
		[TypeConverter(typeof(LengthConverter))]
		public double HeadHeight
		{
			get => (double)GetValue(HeadHeightProperty);
			set => SetValue(HeadHeightProperty, value);
		}

        public Arrow()
        {
			StrokeThickness = 1;
			Stroke = Brushes.Black;
			HeadWidth = 5;
			HeadHeight = 5;
        }

		protected override Geometry DefiningGeometry
		{
			get
			{
				// Create a StreamGeometry for describing the shape
				StreamGeometry geometry = new StreamGeometry();
				geometry.FillRule = FillRule.EvenOdd;
				using (StreamGeometryContext context = geometry.Open())
				{
					InternalDrawArrowGeometry(context);
				}
				// Freeze the geometry for performance benefits
				geometry.Freeze();
				return geometry;
			}
		}

		private void InternalDrawArrowGeometry(StreamGeometryContext context)
		{
			double theta = Math.Atan2(HeadWidth, HeadHeight);
			double sint = Math.Sin(theta);
			double cost = Math.Cos(theta);

			double l1 = Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1));
			double l2 = Math.Sqrt(HeadWidth * HeadWidth + HeadHeight * HeadHeight);

			Point pt1 = new Point(X1, Y1);
			Point pt2 = new Point(X2, Y2);

			Point pt3 = new Point(
				X2 + (l2 / l1) * ((X1 - X2) * cost + (Y1 - Y2) * sint),
				Y2 + (l2 / l1) * ((Y1 - Y2) * cost - (X1 - X2) * sint));

			Point pt4 = new Point(
				X2 + (l2 / l1) * ((X1 - X2) * cost - (Y1 - Y2) * sint),
				Y2 + (l2 / l1) * ((Y1 - Y2) * cost + (X1 - X2) * sint));

			context.BeginFigure(pt1, false, false);
			context.LineTo(pt2, true, true);
			context.LineTo(pt3, true, true);
			context.LineTo(pt2, true, true);
			context.LineTo(pt4, true, true);
		}
	}
}
