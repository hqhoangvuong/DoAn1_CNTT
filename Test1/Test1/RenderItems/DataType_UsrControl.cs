using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.RenderItems
{
    public class DataType_UsrControl
    {
        public DataType_UsrControl()
        {

        }
    }

    public class Point
    {
        protected double X_Coordinates;
        protected double Y_Coordinates;

        public Point()
        {

        }

        public Point(double x, double y)
        {
            this.X_Coordinates = x;
            this.Y_Coordinates = y;
        }

        public double X
        {
            get { return this.X_Coordinates; }
            set { this.X_Coordinates = Convert.ToDouble(value); }
        }

        public double Y
        {
            get { return this.Y_Coordinates; }
            set { this.Y_Coordinates = Convert.ToDouble(value); }
        }
    }

    public class AnchorPoint
    {
        protected Point AnchorTop;
        protected Point AnchorBottom;
        protected Point AnchorLeft;
        protected Point AnchorRight;
        public AnchorPoint(double Height, double Width, double Top, double Left)
        {
            this.AnchorTop = new Point(Left + Width / 2, Top);
            this.AnchorBottom = new Point(Left + Width / 2, Top + Height);
            this.AnchorLeft = new Point(Left, Top + Height / 2);
            this.AnchorRight = new Point(Left + Width, Top + Height / 2);
        }

        public Point Top
        {
            get { return this.AnchorTop; }
        }

        public Point Bottom
        {
            get { return this.AnchorBottom; }
        }

        public Point Left
        {
            get { return this.AnchorLeft; }
        }

        public Point Right
        {
            get { return this.AnchorRight; }
        }
    }
}
