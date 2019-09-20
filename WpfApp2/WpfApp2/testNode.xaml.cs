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
    /// Interaction logic for testNode.xaml
    /// </summary>
    public partial class testNode : UserControl
    {
        private int maxLength = 0;
        public testNode()
        {
            InitializeComponent();
        }

        public String txtPropety
        {
            get { return this.txtblockPropety.Text; }
            set { this.txtblockPropety.Text = string.Format(value); }
        }

        public String txtTitle
        {
            get { return this.txtblockTitle.Text; }
            set { this.txtblockTitle.Text = string.Format(value); }
        }

        public void txtPropety_AddLine(string lineInput)
        {
            if (txtblockPropety.Text == "")
            {
                txtblockPropety.Text += lineInput;
                this.Height -= 25;
            }
            else
                txtblockPropety.Text += System.Environment.NewLine + lineInput;

            if (maxLength < lineInput.Length)
            {
                maxLength = lineInput.Length;
                this.Width = maxLength * 10;
            }

            this.Height += 28;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            TitleZone.Background = new SolidColorBrush(Colors.Gray);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            TitleZone.Background = new SolidColorBrush(Colors.Green);
        }
    }
}
