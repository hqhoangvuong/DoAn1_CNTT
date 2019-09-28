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

namespace TensorGram.RenderControl
{
    class Render_MasterControl
    {
        protected TensorModel Model;
        protected Canvas MainCanvas;

        protected double Offset_Y = 50;
        protected double Offset_X = 50;

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
            double Y = 50;
            double X = MainCanvas.ActualWidth / 2;
            GetParent(ref _model.Layers);
            GetChild(ref _model.Layers);

            foreach (Layer item in _model.Layers)
            {
                var _graphicItem = item.GraphicsNode;
                Canvas.SetTop(_graphicItem, Y);
                Canvas.SetLeft(_graphicItem, X - _graphicItem.Width / 2);
                MainCanvas.Children.Add(_graphicItem);
                Y += Offset_Y + _graphicItem.Height;
                item.GraphicsNode.CalcAnchorPoint();
            }

            ConnectorRender_Control cr = new ConnectorRender_Control(_model.Layers);
            foreach (Shape connector in cr.ListConnector)
            {
                MainCanvas.Children.Add(connector);
            }

            //MainCanvas.Children.Add(Arrow.DrawLinkArrow(new Point(X, Y), new Point(X, Y + 100)));
        }

        protected void RenderChild(Layer _input, ref double _CurrenTop, Canvas _maincanvas)
        {

        }
    }
}
