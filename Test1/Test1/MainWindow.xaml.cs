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
using System.IO;
using Test1.Layers;
using Test1.RenderControl;
using Test1.RenderItems;

namespace Test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtCodeInput.Document.Blocks.Clear();
        }

        private void BntExec_Click(object sender, RoutedEventArgs e)
        {
            string temp = new TextRange(txtCodeInput.Document.ContentStart, txtCodeInput.Document.ContentEnd).Text;
            List<string> DataInbound_Lines = new List<string>();
            try
            {
                using(var reader = new StringReader(temp))
                {
                    bool isEnd = false;
                    while (!isEnd)
                    {
                        string temp1 = reader.ReadLine();
                        if (temp1 == null)
                        {
                            isEnd = true;
                            break;
                        }
                        else
                            DataInbound_Lines.Add(temp1);
                    }
                }    
            }
            catch
            {
                MessageBox.Show("An error occoured!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InputCodeAnalyzer(DataInbound_Lines);
        }

        private void InputCodeAnalyzer(List<string> Data)
        {
            
            InputDataAnalyzer az = new InputDataAnalyzer();
            LayerNetwork la = az.InputDataHander(Data);
            LayerRender_Control lrC = new LayerRender_Control(la);
            lrC.Render(MainCanvas, GridWorkspace);
        }
    }
}
