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

            MainScrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            MainScrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            MainScrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            MainScrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            MainScrollViewer.MouseMove += OnMouseMove;
            this.Grid_ToolBox.Visibility = System.Windows.Visibility.Hidden;
            lbZoomRatio.Content = "100%";
            StartupLogo.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/TensorGram;component/Resources/Logo-01.png", UriKind.Absolute))};
        }

        private void BntExec_Click(object sender, RoutedEventArgs e)
        {
            string temp = new TextRange(txtCodeInput.Document.ContentStart, txtCodeInput.Document.ContentEnd).Text;
            if (temp == "")
                return;
            else
            {
                //try
                //{
                    InputHander = new TextInput_Hander(temp, ref this.Model);
                    RenderHander = new Render_MasterControl(MainCanvas, this.Model);
                    ViewCenter();

                    // Nap du lieu slidePanel_Control
                    SlidePanel_Control.Init_SlidePanel_Control(SlideMenu_StackPanel, SlidePanel_TextBlock, Resources["ShowMenu"] as Storyboard, SlidePanel_lvListLayers, SlidePanel_txtBoxFind, this.Model.Layers);
                    this.StartupLogo.Visibility = System.Windows.Visibility.Hidden;
                    this.Grid_ToolBox.Visibility = System.Windows.Visibility.Visible;
               // }
               // catch
               // {
               //    MessageBox.Show("An fatal error has been occured. Please check the input data and try again!", "System's Message", MessageBoxButton.OK, MessageBoxImage.Error);
               // }
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
                foreach (Layer _layer in Model.Layers)
                {
                    if (_layer.LayerName == e.AddedItems[0].ToString())
                    {
                        _layer.GraphicsNode.Highlight();
                        SlidePanel_Control.isLayerHighlighted = true;
                    }
                }
            }
        }

        #region Stuff For Mouse drag and Canvas Zoom

        private Point? lastCenterPositionOnTarget;
        private Point? lastMousePositionOnTarget;
        private Point? lastDragPoint;
        private bool isMoveMode = false;
        private double Zoom = 1;
        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(MainScrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                MainScrollViewer.ScrollToHorizontalOffset(MainScrollViewer.HorizontalOffset - dX);
                MainScrollViewer.ScrollToVerticalOffset(MainScrollViewer.VerticalOffset - dY);
            }
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isMoveMode)
            {
                var mousePos = e.GetPosition(MainScrollViewer);
                if (mousePos.X <= MainScrollViewer.ViewportWidth && mousePos.Y < MainScrollViewer.ViewportHeight)
                {
                    MainScrollViewer.Cursor = Cursors.SizeAll;
                    lastDragPoint = mousePos;
                    Mouse.Capture(MainScrollViewer);
                }
            }
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isMoveMode)
            {
                MainScrollViewer.Cursor = Cursors.Arrow;
                MainScrollViewer.ReleaseMouseCapture();
                lastDragPoint = null;
            }
        }

        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(MainScrollViewer.ViewportWidth / 2, MainScrollViewer.ViewportHeight / 2);
                        Point centerOfTargetNow = MainScrollViewer.TranslatePoint(centerOfViewport, MainCanvas);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(MainCanvas);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / MainCanvas.Width;
                    double multiplicatorY = e.ExtentHeight / MainCanvas.Height;

                    double newOffsetX = MainScrollViewer.HorizontalOffset - dXInTargetPixels * multiplicatorX;
                    double newOffsetY = MainScrollViewer.VerticalOffset - dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    MainScrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    MainScrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        private void LbMove_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToolBox_MouseEnter(object sender, MouseEventArgs e)
        {
            Label lb = sender as Label;
            lb.Foreground = Brushes.Green;
            Grid_ToolBox.Cursor = Cursors.Hand;
        }

        private void ToolBox_MouseLeave(object sender, MouseEventArgs e)
        {
            Label lb = sender as Label;
            lb.Foreground = Brushes.White;

            if (isMoveMode)
                lbMove.Foreground = Brushes.Red;
            Grid_ToolBox.Cursor = Cursors.Arrow;
            if(!lbZoomOut.IsEnabled)
                lb.Foreground = Brushes.Gray;
        }

        private void ToolBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label lb = sender as Label;
            switch (lb.Content)
            {
                case "Move":
                    this.isMoveMode = !isMoveMode;
                    break;
                case "Zoom in":
                    if (Zoom <= 2)
                    {
                        Zoom += .05;
                        scaleTransform.ScaleX = Zoom;
                        scaleTransform.ScaleY = Zoom;

                        var centerOfViewport = new Point(MainScrollViewer.ViewportWidth / 2, MainScrollViewer.ViewportHeight / 2);
                        lastCenterPositionOnTarget = MainScrollViewer.TranslatePoint(centerOfViewport, GridWorkspace);
                        if (!lbZoomOut.IsEnabled)
                        {
                            lbZoomOut.IsEnabled = true;
                            lbZoomOut.Foreground = Brushes.White;
                        }
                    }
                    else
                        lb.IsEnabled = false;
                    break;
                case "Zoom out":
                    if (Zoom > 0.3)
                    {
                        Zoom -= .05;
                        scaleTransform.ScaleX = Zoom;
                        scaleTransform.ScaleY = Zoom;

                        var centerOfViewport1 = new Point(MainScrollViewer.ViewportWidth / 2, MainScrollViewer.ViewportHeight / 2);
                        lastCenterPositionOnTarget = MainScrollViewer.TranslatePoint(centerOfViewport1, GridWorkspace);
                    }
                    else
                        lb.IsEnabled = false;
                    break;
            }
            lbZoomRatio.Content = (Zoom * 100).ToString() + "%";
        }
        #endregion

        private void txtCodeInput_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
