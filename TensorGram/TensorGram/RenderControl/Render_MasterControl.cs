using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using TensorGram.Layers.Topology;
using TensorGram.Layers;
using TensorGram.GraphicsObject;

namespace TensorGram.RenderControl
{
    class Render_MasterControl
    {
        protected TensorModel Model;
        protected Canvas MainCanvas;
        protected StackPanel SlideMenu_StackPanel;
        protected TextBlock SlideMenu_TextBlock;

        protected double Offset_Top = 10;
        protected double Offset_Left = 50;

        protected double MaxLeft;
        protected double MaxRight;


        public Render_MasterControl()
        {

        }

        public Render_MasterControl(Canvas _maincanvas, StackPanel _slidemenu_stackpanel, TextBlock _slidemenu_textblock, TensorModel _model)
        {
            this.MainCanvas = _maincanvas;
            this.SlideMenu_StackPanel = _slidemenu_stackpanel;
            this.SlideMenu_TextBlock = _slidemenu_textblock;
            this.Model = _model;
            LayerRender(Model);
        }

        protected void GetChild(ref List<Layer> _Layers)
        {
            for (int i = 0; i < _Layers.Count - 1; i++)
            {
                for (int j = i + 1; j < _Layers.Count; j++)
                {
                    if (_Layers[j].Inboundlayer.Contains(_Layers[i].OutboundLayer))
                        _Layers[i].ChildNode.Add(_Layers[j]);
                }
            }
        }

        public void LayerRender(TensorModel _model)
        {

        }

        protected void RenderChild(Layer _input, ref double _CurrenTop, Canvas _maincanvas)
        {

        }
    }
}
