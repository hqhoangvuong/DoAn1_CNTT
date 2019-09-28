using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using TensorGram.Layers;
using System.Windows.Controls;
using TensorGram.GraphicsObject;

namespace TensorGram.RenderControl
{
    class ConnectorRender_Control
    {
        public List<Shape> ListConnector;
        public List<Layer> Layers;
        public int LineOffset = 30;

        public ConnectorRender_Control(List<Layer> _layers)
        {
            this.ListConnector = new List<Shape>();
            this.Layers = _layers;
            CalcConnector();
        }

        public ConnectorRender_Control()
        {

        }

        public void CalcConnector()
        {
            //double tempX1 = parent.GraphicsNode.AnchorPoint.Left.X - LineOffset;
            double tempX1 = Layers[1].GraphicsNode.AnchorPoint.Left.X - LineOffset;
            foreach (Layer parent in Layers)
            {
                int ChildCount = parent.ChildLayer.Count;
                if (parent.ChildLayer.Count != 0)
                {
                    foreach (Layer child in parent.ChildLayer)
                    {
                        if (Canvas.GetTop(child.GraphicsNode) - (Canvas.GetTop(parent.GraphicsNode) + parent.GraphicsNode.Height) == 50)
                        {
                            createConnectorPointDown(parent.GraphicsNode, child.GraphicsNode, ref ListConnector);
                            ChildCount--;
                        }
                        else
                        {
                            Line _line = new Line();
                            _line.X2 = parent.GraphicsNode.AnchorPoint.Left.X;
                            _line.Y2 = parent.GraphicsNode.AnchorPoint.Left.Y + 5;
                            if (parent.GraphicsNode.AnchorPoint.Left.X - LineOffset * ChildCount < tempX1)
                                _line.X1 = parent.GraphicsNode.AnchorPoint.Left.X - LineOffset * ChildCount;
                            else
                                _line.X1 = tempX1;
                            _line.Y1 = parent.GraphicsNode.AnchorPoint.Left.Y + 5;

                            // Create a red Brush  
                            SolidColorBrush redBrush = new SolidColorBrush();
                            redBrush.Color = Colors.Red;

                            // Set Line's width and color  
                            _line.StrokeThickness = 2;
                            _line.Stroke = redBrush;
                            ListConnector.Add(_line);

                            createConnector(tempX1, parent.GraphicsNode.AnchorPoint.Left.Y + 5, tempX1, child.GraphicsNode.AnchorPoint.Left.Y - 5);
                            Shape _arrow = Arrow.DrawLinkArrow(new Point(tempX1, child.GraphicsNode.AnchorPoint.Left.Y - 5), new Point(child.GraphicsNode.AnchorPoint.Left.X, child.GraphicsNode.AnchorPoint.Left.Y - 5));
                            ListConnector.Add(_arrow);
                            tempX1 -= LineOffset;
                        }
                    }
                }
            }
        }

        public void createConnector(double x1, double y1, double x2, double y2)
        {
            Line _line = new Line();
            _line.X2 = x2;
            _line.Y2 = y2;
            _line.X1 = x1;
            _line.Y1 = y1;

            // Create a red Brush  
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;

            // Set Line's width and color  
            _line.StrokeThickness = 2;
            _line.Stroke = redBrush;
            ListConnector.Add(_line);
        }
        
        public void createConnectorPointDown(GraphicsNode_UsrCtrl parent, GraphicsNode_UsrCtrl child, ref List<Shape> _listconnector)
        {
            Shape arrow = Arrow.DrawLinkArrow(parent.AnchorPoint.Bottom, child.AnchorPoint.Top);
            _listconnector.Add(arrow);
        }
    }
}
