using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using TensorGram.Layers;

namespace TensorGram.RenderControl
{
    public static class SlidePanel_Control
    {
        public static StackPanel RenderControl_SlidePanel;
        public static TextBlock RenderControl_SlidePanel_Textblock;
        public static Storyboard RenderControl_sb;
        public static List<Layer> RenderControl_ModelLayersData;
        public static bool Slidepanel_Opened;
        public static void Init_SlidePanel_Control(StackPanel pn, TextBlock tb, Storyboard sb, List<Layer> layers)
        {
            RenderControl_SlidePanel = pn;
            RenderControl_SlidePanel_Textblock = tb;
            RenderControl_sb = sb;
            RenderControl_ModelLayersData = layers;
            Slidepanel_Opened = false;
        }

        public static void SlidePanel_Show(string namelayer)
        {

            foreach (Layer _layer in RenderControl_ModelLayersData)
            {
                if (_layer.LayerName == namelayer)
                {
                    RenderControl_SlidePanel_Textblock.Text = string.Empty;
                    foreach (string temp in _layer.ToString())
                    {
                        RenderControl_SlidePanel_Textblock.Text += temp + System.Environment.NewLine;
                        if (temp == "<Attributes>") ;

                    }
                    if (!Slidepanel_Opened)
                    {
                        RenderControl_sb.Begin(RenderControl_SlidePanel);
                        Slidepanel_Opened = true;
                    }
                }
            }
        }
    }
}
