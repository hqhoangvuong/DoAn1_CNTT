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
        public List<Shape> ListConnector { get; }
        public Dictionary<string, List<Layer>> TreeLevel;
        public int LineOffset = 30;
        public List<double> LevelBound;

        public ConnectorRender_Control(Dictionary<string, List<Layer>> _treelevel)
        {
            this.ListConnector = new List<Shape>();
            this.TreeLevel = _treelevel;
            calcLevelBound();
            CalcConnector();
        }

        public ConnectorRender_Control()
        {

        }

        //public void CalcConnector()
        //{
        //    //double tempX1 = parent.GraphicsNode.AnchorPoint.Left.X - LineOffset;
        //    double tempX1 = Layers[1].GraphicsNode.AnchorPoint.Left.X - LineOffset;
        //    foreach (Layer parent in Layers)
        //    {
        //        int ChildCount = parent.ChildLayer.Count;
        //        if (parent.ChildLayer.Count != 0)
        //        {
        //            foreach (Layer child in parent.ChildLayer)
        //            {
        //                if (Canvas.GetTop(child.GraphicsNode) - (Canvas.GetTop(parent.GraphicsNode) + parent.GraphicsNode.Height) == 50)
        //                {
        //                    createConnectorPointDown(parent.GraphicsNode, child.GraphicsNode, ref ListConnector);
        //                    ChildCount--;
        //                }
        //                else
        //                {
        //                    Line _line = new Line();
        //                    _line.X2 = parent.GraphicsNode.AnchorPoint.Left.X;
        //                    _line.Y2 = parent.GraphicsNode.AnchorPoint.Left.Y + 5;
        //                    if (parent.GraphicsNode.AnchorPoint.Left.X - LineOffset * ChildCount < tempX1)
        //                        _line.X1 = parent.GraphicsNode.AnchorPoint.Left.X - LineOffset * ChildCount;
        //                    else
        //                        _line.X1 = tempX1;
        //                    _line.Y1 = parent.GraphicsNode.AnchorPoint.Left.Y + 5;

        //                    // Create a red Brush  
        //                    SolidColorBrush redBrush = new SolidColorBrush();
        //                    redBrush.Color = Colors.White;

        //                    // Set Line's width and color  
        //                    _line.StrokeThickness = 2;
        //                    _line.Stroke = redBrush;
        //                    ListConnector.Add(_line);

        //                    createConnector(tempX1, parent.GraphicsNode.AnchorPoint.Left.Y + 5, tempX1, child.GraphicsNode.AnchorPoint.Left.Y - 5);
        //                    Shape _arrow = Arrow.DrawLinkArrow(new Point(tempX1, child.GraphicsNode.AnchorPoint.Left.Y - 5), new Point(child.GraphicsNode.AnchorPoint.Left.X, child.GraphicsNode.AnchorPoint.Left.Y - 5));
        //                    ListConnector.Add(_arrow);
        //                    tempX1 -= LineOffset;
        //                }
        //            }
        //        }
        //    }
        //}

        public void CalcConnector()
        {
            for (int i = 0; i < TreeLevel.Count; i++)
            {
                List<Layer> temp = TreeLevel[i.ToString()];
                foreach (Layer item in temp)
                {
                    double parentAnchorX = item.GraphicsNode.AnchorPoint.Bottom.X;
                    double parentAnchorY = item.GraphicsNode.AnchorPoint.Bottom.Y;
                    if (item.ChildLayer.Count != 0)
                        foreach (Layer child in item.ChildLayer)    
                        {
                            double childAnchorX = child.GraphicsNode.AnchorPoint.Top.X;
                            double childAnchorY = child.GraphicsNode.AnchorPoint.Top.Y;
                            if (item.Level + 1 == child.Level)
                            {
                                createArrow(parentAnchorX, parentAnchorY, childAnchorX, childAnchorY);
                            }
                            else
                            {
                                double offsetX = FindBound(item.Level, child.Level, item.OutboundLayer, child.OutboundLayer);
                                if (offsetX == -1)
                                {
                                    createArrow(parentAnchorX, parentAnchorY, childAnchorX, childAnchorY);
                                }
                                else
                                {
                                    double offsetY = 75;
                                    createConnector(parentAnchorX, parentAnchorY, offsetX, parentAnchorY + offsetY);
                                    createConnector(offsetX, parentAnchorY + offsetY, offsetX, childAnchorY - offsetY);
                                    createArrow(offsetX, childAnchorY - offsetY, childAnchorX, childAnchorY);
                                }
                            }
                        }
                }
            }
        }

        // Ham tim phan nho ra lon nhat giua cac level
        public double FindBound(int levelSrc, int levelDes, string src, string des)
        {
            double srcPos = Canvas.GetLeft(TreeLevel[levelSrc.ToString()].Find(x => x.OutboundLayer.Equals(src)).GraphicsNode);
            double bound = srcPos;
            double offset = 250;
            double offset1 = 50;
            int index = 0;
            for(int i = levelSrc +1; i < levelDes; i++)
            {
                if (bound < LevelBound[i])
                {
                    bound = LevelBound[i];
                    index = i;
                }
            }

            if (bound == srcPos)
            {
                int desIndex = TreeLevel[levelDes.ToString()].IndexOf(TreeLevel[levelDes.ToString()].Find(x => x.OutboundLayer.Equals(des)));
                double BoundTemp = Canvas.GetLeft(TreeLevel[levelDes.ToString()][desIndex].GraphicsNode);

                for (int i = levelDes - 1; i > levelSrc; i--)
                {
                    List<Layer> temp = TreeLevel[i.ToString()];
                    if (desIndex < temp.Count - 1 && BoundTemp < Canvas.GetLeft(temp[temp.Count - 1].GraphicsNode))
                    {
                        BoundTemp = Canvas.GetLeft(temp[temp.Count - 1].GraphicsNode);
                    }
                }

                if (BoundTemp == Canvas.GetLeft(TreeLevel[levelDes.ToString()][desIndex].GraphicsNode))
                    return -1;
                else
                    bound = BoundTemp + 20;
            }


            LevelBound[index] = bound + offset1;
            return bound + offset;
        }

        public void calcLevelBound()
        {
            LevelBound = new List<double>();
            for (int i = 0; i < TreeLevel.Count; i++)
            {
                List<Layer> temp = TreeLevel[i.ToString()];
                if (temp.Count != 0)
                {
                    LevelBound.Add(Canvas.GetLeft(temp[temp.Count - 1].GraphicsNode));
                }
            }
        }

        private void createConnector(double x1, double y1, double x2, double y2)
        {
            Line _line = new Line();
            _line.X2 = x2;
            _line.Y2 = y2;
            _line.X1 = x1;
            _line.Y1 = y1;

            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.White;

            _line.StrokeThickness = 2;
            _line.Stroke = redBrush;
            ListConnector.Add(_line);
        }

        private void createArrow(double x1, double y1, double x2, double y2)
        {
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Shape arrow = Arrow.DrawLinkArrow(p1, p2);
            ListConnector.Add(arrow);
        }
    }
}
