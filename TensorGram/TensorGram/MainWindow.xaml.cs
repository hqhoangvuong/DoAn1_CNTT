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
                SlidePanel_Control.Init_SlidePanel_Control(SlideMenu_StackPanel, SlidePanel_TextBlock, Resources["ShowMenu"] as Storyboard, this.Model.Layers);
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
        }
    }
}
