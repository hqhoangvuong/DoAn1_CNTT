using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.GraphicsObject
{
    public class AnchorPoint
    {
        public Point Top;
        public Point Bottom;
        public Point Left;
        public Point Right;

        public AnchorPoint()
        {

        }

        public AnchorPoint(double Height, double Width, double Top, double Left)
        {
            this.Top = new Point(Left + Width / 2, Top);
            this.Bottom = new Point(Left + Width / 2, Top + Height);
            this.Left = new Point(Left, Top + Height / 2);
            this.Right = new Point(Left + Width, Top + Height / 2);
        }
    }

    public class Point
    {
        public double X;
        public double Y;
        public Point()
        {

        }

        public Point(double _x, double _y)
        {
            this.X = _x;
            this.Y = _y;
        }
    }
}
