using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using TensorGram.Layers;
using System.Windows.Documents;
using System.Windows;

namespace TensorGram.RenderControl
{
    public enum SlidePanel_Mode
    {
        LayerPropeties,
        LayerFind
    }

    public class Listview_Data
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public static class SlidePanel_Control
    {
        public static StackPanel RenderControl_SlidePanel;
        public static TextBlock RenderControl_SlidePanel_Textblock;
        public static Storyboard RenderControl_sb;
        public static ListView RenderControl_SlidePanel_ListView;
        public static TextBox RenderControl_SlidePanel_Textbox;
        public static List<Layer> RenderControl_ModelLayersData;
        public static bool isLayerHighlighted;
        public static bool Slidepanel_Opened;
        public static void Init_SlidePanel_Control(StackPanel pn, TextBlock tb, Storyboard sb, ListView lv, TextBox tbx, List<Layer> layers)
        {
            RenderControl_SlidePanel = pn;
            RenderControl_SlidePanel_Textblock = tb;
            RenderControl_sb = sb;
            RenderControl_SlidePanel_ListView = lv;
            RenderControl_SlidePanel_Textbox = tbx;
            RenderControl_ModelLayersData = layers;
            Slidepanel_Opened = false;
            isLayerHighlighted = false;
        }

        public static void SlidePanel_Show(string namelayer, SlidePanel_Mode displayMode)
        {
            if (displayMode == SlidePanel_Mode.LayerPropeties)
            {
                foreach (Layer _layer in RenderControl_ModelLayersData)
                {
                    if (_layer.LayerName == namelayer)
                    {
                        RenderControl_SlidePanel_ListView.Visibility = System.Windows.Visibility.Hidden;
                        RenderControl_SlidePanel_Textbox.Visibility = System.Windows.Visibility.Hidden;
                        RenderControl_SlidePanel_Textblock.Visibility = System.Windows.Visibility.Visible;
                        RenderControl_SlidePanel_Textblock.Text = string.Empty;
                        foreach (string temp in _layer.ToString())
                        {
                            //switch (temp)
                            //{
                            //    case "\nAttributes":
                            //        RenderControl_SlidePanel_Textblock.Inlines.Add(new Run("Attributes\n") { Foreground = Brushes.Blue, FontStyle = FontStyles.Italic });
                            //        break;
                            //        case 
                            //    default:
                            //        //RenderControl_SlidePanel_Textblock.Text += temp + System.Environment.NewLine;
                            //        RenderControl_SlidePanel_Textblock.Inlines.Add(new Run(temp + "\n") /*{ Foreground = Brushes.Blue, FontStyle = FontStyles.Italic }*/);
                            //        break;
                            //}

                            if (temp == "\nOutputs" || temp == "\nAttributes" || temp == "\nInputs")
                            {
                                RenderControl_SlidePanel_Textblock.Inlines.Add(new Run(temp + "\n") { Foreground = Brushes.Blue, FontStyle = FontStyles.Italic });
                            }
                            else
                            {
                                RenderControl_SlidePanel_Textblock.Inlines.Add(new Run(temp + "\n") /*{ Foreground = Brushes.Blue, FontStyle = FontStyles.Italic }*/);
                            }
                        }
                    }
                }
            }
            else if(displayMode == SlidePanel_Mode.LayerFind)
            {
                RenderControl_SlidePanel_ListView.Visibility = System.Windows.Visibility.Visible;
                RenderControl_SlidePanel_Textbox.Visibility = System.Windows.Visibility.Visible;
                RenderControl_SlidePanel_Textblock.Visibility = System.Windows.Visibility.Hidden;

                List<Listview_Data> data = new List<Listview_Data>();
                foreach(Layer _layer in RenderControl_ModelLayersData)
                {
                    data.Add(new Listview_Data() { Type = Enum.GetName(typeof(LayerTypes), _layer.Type), Name = _layer.LayerName });
                }
                RenderControl_SlidePanel_ListView.ItemsSource = data;
            }

            if (!Slidepanel_Opened)
            {
                RenderControl_sb.Begin(RenderControl_SlidePanel);
                Slidepanel_Opened = true;
            }
        }
    }
}
