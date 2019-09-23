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

namespace TensorGram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BntExec_Click(object sender, RoutedEventArgs e)
        {
            string temp = new TextRange(txtCodeInput.Document.ContentStart, txtCodeInput.Document.ContentEnd).Text;
            if (temp == "")
                return;
            else
            {
                TextInput_Hander InputHander = new TextInput_Hander(temp);

            }
        }

        private void bntHide_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = Resources["HideMenu"] as Storyboard;
            sb.Begin(LeftMenu);
        }
    }
}
