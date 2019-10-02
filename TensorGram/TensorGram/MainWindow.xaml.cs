using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TensorGram.RenderControl;
using TensorGram.Layers;
namespace TensorGram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TensorModel Model;
        TextInput_Hander InputHander;
        Render_MasterControl RenderHander;

        public MainWindow()
        {
            InitializeComponent();
            this.Model = new TensorModel();
        }

        private void BntExec_Click(object sender, RoutedEventArgs e)
        {
            string temp = new TextRange(txtCodeInput.Document.ContentStart, txtCodeInput.Document.ContentEnd).Text;
            if (temp == "")
                return;
            else
            {
                InputHander = new TextInput_Hander(temp, ref this.Model);
                RenderHander = new Render_MasterControl(MainCanvas, this.Model);
                ViewCenter();

                // Nap du lieu slidePanel_Control
                SlidePanel_Control.Init_SlidePanel_Control(SlideMenu_StackPanel, SlidePanel_TextBlock, Resources["ShowMenu"] as Storyboard, SlidePanel_lvListLayers, SlidePanel_txtBoxFind, this.Model.Layers);
            }
        }

        private void ViewCenter()
        {
            MainScrollViewer.ScrollToHorizontalOffset(MainCanvas.ActualWidth / 2 - 450);
        }

        private void bntHide_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["HideMenu"] as Storyboard;
            sb.Begin(SlideMenu_StackPanel);
            SlidePanel_Control.Slidepanel_Opened = false;

            // Xoá chọn layer
            if (SlidePanel_Control.isLayerHighlighted)
            {
                foreach (Layer _layer in Model.Layers)
                    if (_layer.GraphicsNode.isHighlighted)
                        _layer.GraphicsNode.DeHighlight();
                SlidePanel_Control.isLayerHighlighted = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Find_Click(object sender, RoutedEventArgs e)
        {
            SlidePanel_Control.SlidePanel_Show("", SlidePanel_Mode.LayerFind);
        }

        private void SlidePanel_txtBoxFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Listview_Data> data = new List<Listview_Data>();

            // Xoá chọn layer
            if (SlidePanel_Control.isLayerHighlighted)
            {
                foreach (Layer _layer in Model.Layers)
                    if (_layer.GraphicsNode.isHighlighted)
                        _layer.GraphicsNode.DeHighlight();
                SlidePanel_Control.isLayerHighlighted = false;
            }

            foreach (Layer _layer in Model.Layers)
            {
                SlidePanel_lvListLayers.ItemsSource = null;
                if (_layer.LayerName.Contains(SlidePanel_txtBoxFind.Text))
                {
                    data.Add(new Listview_Data() { Type = Enum.GetName(typeof(LayerTypes), _layer.Type), Name = _layer.LayerName });
                }
                SlidePanel_lvListLayers.ItemsSource = data;
            }
        }

        private void SlidePanel_txtBoxFind_TextInput(object sender, TextCompositionEventArgs e)
        {



        }

        private void SlidePanel_lvListLayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach(Layer _layer in Model.Layers)
                {
                    if (_layer.LayerName == e.AddedItems[0].ToString())
                    {
                        _layer.GraphicsNode.Highlight();
                        SlidePanel_Control.isLayerHighlighted = true;
                    }
                }
            }
        }

        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        // Mouse Drag
        Point scrollMousePoint = new System.Windows.Point();
        double hOff = 1;
        double vOff = 1;

        private void MainScrollViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (MainScrollViewer.IsMouseCaptured)
            {
                MainScrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(MainScrollViewer).X));
                MainScrollViewer.ScrollToVerticalOffset(vOff + (scrollMousePoint.Y - e.GetPosition(MainScrollViewer).Y));
            }
        }

        private void MainScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainScrollViewer.ReleaseMouseCapture();
        }

        private void MainScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (false)
            {
                MainScrollViewer.CaptureMouse();
                scrollMousePoint = e.GetPosition(MainScrollViewer);
                hOff = MainScrollViewer.HorizontalOffset;
                vOff = MainScrollViewer.VerticalOffset;
            }
        }

        private void MainScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            

        }
    }
}
