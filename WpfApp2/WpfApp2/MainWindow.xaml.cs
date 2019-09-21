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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Node usrcontrol = new Node();
        testNode usrcontrol2 = new testNode();
        ScaleTransform st = new ScaleTransform();

        public MainWindow()
        {
            InitializeComponent();
            mainCanvas.Height = 900;
            mainCanvas.RenderTransform = st;
        }

        private void Bnt1_Click(object sender, RoutedEventArgs e)
        {
            usrcontrol.Width = 150;
            usrcontrol.Height = 50;
            Canvas.SetLeft(usrcontrol, 100);
            Canvas.SetTop(usrcontrol, 400);
            mainCanvas.Children.Add(usrcontrol);
        }

        private void Bnt2_Click(object sender, RoutedEventArgs e)
        {
            usrcontrol2.SetAsInputNode();
        }

        private void TxBox1_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void TxBox2_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void TxBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //usrcontrol2.txtPropety= txBox1.Text;
        }

        private void TxBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            usrcontrol.textBox2 = txBox2.Text;
        }

        private void Bnt3_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(usrcontrol2, 100);
            Canvas.SetTop(usrcontrol2, 400);
            //txBox1.Text = usrcontrol2.txtPropety;
            mainCanvas.Children.Add(usrcontrol2);
            usrcontrol2.CalcAnchorPoint();
        }

        private void BntAddLine_Click(object sender, RoutedEventArgs e)
        {
            usrcontrol2.txtPropety_AddLine(txtAddLine.Text);
        }

        const double ScaleRate = 1.1;
        private void MainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            st.ScaleX *= ScaleRate;
            st.ScaleY *= ScaleRate;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            st.ScaleX /= ScaleRate;
            st.ScaleY /= ScaleRate;
        }
    }
}
