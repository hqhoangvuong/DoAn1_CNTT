using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace TensorGram.GraphicsObject
{
    public static class Arrow
    {
        public static Color ArrowColor = (Color)ColorConverter.ConvertFromString("#4B4A4B");
        public static Shape DrawLinkArrow(Point p1, Point p2)
        {
            GeometryGroup lineGroup = new GeometryGroup();
            double theta = Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            Point p = new Point(p1.X + ((p2.X - p1.X)), p1.Y + ((p2.Y - p1.Y)));
            pathFigure.StartPoint = new System.Windows.Point(p.X, p.Y);

            Point lpoint = new Point(p.X + 3, p.Y + 10);
            Point rpoint = new Point(p.X - 3, p.Y + 10);
            LineSegment seg1 = new LineSegment();
            seg1.Point = new System.Windows.Point(lpoint.X, lpoint.Y);
            pathFigure.Segments.Add(seg1);

            LineSegment seg2 = new LineSegment();
            seg2.Point = new System.Windows.Point(rpoint.X, rpoint.Y);
            pathFigure.Segments.Add(seg2);

            LineSegment seg3 = new LineSegment();
            seg3.Point = new System.Windows.Point(p.X, p.Y);
            pathFigure.Segments.Add(seg3);

            pathGeometry.Figures.Add(pathFigure);
            RotateTransform transform = new RotateTransform();
            transform.Angle = theta + 90;
            transform.CenterX = p.X;
            transform.CenterY = p.Y;
            pathGeometry.Transform = transform;
            lineGroup.Children.Add(pathGeometry);

            LineGeometry connectorGeometry = new LineGeometry();
            connectorGeometry.StartPoint = new System.Windows.Point(p1.X, p1.Y);
            connectorGeometry.EndPoint = new System.Windows.Point(p2.X, p2.Y);
            lineGroup.Children.Add(connectorGeometry);
            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path();
            path.Data = lineGroup;
            path.StrokeThickness = 2;

            //path.Stroke = path.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4B4A4B"));
            path.Stroke = path.Fill = Brushes.White;

            return path;
        }
    }
}
