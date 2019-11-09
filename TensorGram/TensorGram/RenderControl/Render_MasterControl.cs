using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Shapes;
using TensorGram.Layers.Topology;
using TensorGram.Layers;
using TensorGram.GraphicsObject;
using TensorGram.Tree;

namespace TensorGram.RenderControl
{
    class Render_MasterControl
    {
        protected TensorModel Model;
        protected Canvas MainCanvas;

        //protected double Offset_Y = 50;
        //protected double Offset_X = 50;

        public Render_MasterControl()
        {

        }

        public Render_MasterControl(Canvas _maincanvas, TensorModel _model)
        {
            this.MainCanvas = _maincanvas;
            this.Model = _model;
            _maincanvas.Children.Clear();
            LayerRender(Model);
        }

        protected void GetChild(ref List<Layer> _Layers)
        {
            for (int i = 0; i < _Layers.Count - 1; i++)
            {
                for (int j = i + 1; j < _Layers.Count; j++)
                {
                    if (_Layers[j].Inboundlayer.Contains(_Layers[i].OutboundLayer))
                        _Layers[i].ChildLayer.Add(_Layers[j]);
                }
            }
        }

        protected void GetParent(ref List<Layer> _Layers)
        {
            for (int i = _Layers.Count - 1; i >= 1; i--)
            {
                _Layers[i].Inboundlayer =_Layers[i].Inboundlayer.OrderByDescending(o => o).ToList();
                for (int j = i - 1; j >= 0; j--)
                {
                    if (_Layers[i].Inboundlayer.Contains(_Layers[j].OutboundLayer))
                        _Layers[i].ParentLayer.Add(_Layers[j]);
                }
            }
        }

        public void LayerRender(TensorModel _model)
        {
            // Thu nghiem
            // double Y = 50;
            double X = MainCanvas.ActualWidth / 2;
            GetParent(ref _model.Layers);
            GetChild(ref _model.Layers);

            //foreach (Layer item in _model.Layers)
            //{
            //    var _graphicItem = item.GraphicsNode;
            //    Canvas.SetTop(_graphicItem, Y);
            //    Canvas.SetLeft(_graphicItem, X - _graphicItem.Width / 2);
            //    MainCanvas.Children.Add(_graphicItem);
            //    Y += Offset_Y + _graphicItem.Height;
            //    item.GraphicsNode.CalcAnchorPoint();
            //}

            //ConnectorRender_Control cr = new ConnectorRender_Control(_model.Layers);
            //foreach (Shape connector in cr.ListConnector)
            //{
            //    MainCanvas.Children.Add(connector);
            //}

            int CurrentLevel = 0;
            foreach(Layer _layer in Model.Layers)
            {
                if(_layer.Level == -1)
                {
                    _layer.Level = CurrentLevel++;
                }

                if (_layer.ChildLayer.Count != 0)
                {
                    foreach (Layer _child in _layer.ChildLayer)
                    {
                        if (_child.Level == -1 && _child.ParentLayer[0] == _layer)
                            _child.Level = CurrentLevel;
                    }
                    CurrentLevel++;
                }
            }
            SetPosition(CurrentLevel, ref Model.Layers, MainCanvas);
            //MainCanvas.Children.Add(Arrow.DrawLinkArrow(new Point(X, Y), new Point(X, Y + 100)));
        }

        protected void SetPosition(int CurrentLevel, ref List<Layer> _layers, Canvas DisplayZone)
        {
            // Testing Zone
            double Y = 50;
            double offsetX = 50;
            double offsetY = 150;
            // Testing Zone

            Dictionary<string, List<Layer>> TreeLevel = new Dictionary<string, List<Layer>>();
            int i = 0;
            // int j = 0;

            for (i = 0; i < CurrentLevel; i++)
            {
                List<Layer> tempList = new List<Layer>();
                foreach (Layer _item in _layers)
                {
                    if (_item.Level == i)
                    {
                        tempList.Add(_item);
                    }
                }
                // j = 0;
                if(tempList.Count > 0)
                    tempList.Sort((x, y) => x.ChildLayer.Count.CompareTo(y.ChildLayer.Count));

                TreeLevel.Add(String.Format(i.ToString()), tempList);
            }

            for(i = 0; i < CurrentLevel; i++)
            {
                double X = MainCanvas.ActualWidth / 2;
                List<Layer> temp = TreeLevel[i.ToString()];
                foreach (Layer item in temp)
                {
                    var _graphicItem = item.GraphicsNode;
                    Canvas.SetTop(_graphicItem, Y);
                    Canvas.SetLeft(_graphicItem, X - _graphicItem.Width / 2);
                    MainCanvas.Children.Add(_graphicItem);

                    X += offsetX + _graphicItem.Width;
                    item.GraphicsNode.CalcAnchorPoint();
                }

                Y = offsetY * (i + 1);
            }

            // Parent Centerlizer



            ConnectorRender_Control cr = new ConnectorRender_Control(TreeLevel);
            foreach (Shape connector in cr.ListConnector)
            {
                DisplayZone.Children.Add(connector);
            }
        }
    }
}
