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
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public Node()
        {
            InitializeComponent();
        }

        public String textBox1
        {
            get { return this.txtBox1.Text; }
            set { this.txtBox1.Text = value; }
        }

        public String textBox2
        {
            get { return this.txtBox2.Text; }
            set { this.txtBox2.Text = value; }
        }
    }
}
