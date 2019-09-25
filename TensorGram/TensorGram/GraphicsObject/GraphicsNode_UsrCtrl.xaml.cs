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
using TensorGram.RenderControl;

namespace TensorGram.GraphicsObject
{
    /// <summary>
    /// Interaction logic for GraphicsNode_UsrCtrl.xaml
    /// </summary>
    public partial class GraphicsNode_UsrCtrl : UserControl
    {
        private double maxLength;
        //public AnchorPoint AnchorPoint;
        public string NodeType;

        public string CurrentTitleColor;
        public string CurrentAttributeColor;

        public string LayerName;

        public GraphicsNode_UsrCtrl()
        {
            InitializeComponent();
            this.NodeType = "";
            this.CurrentTitleColor = "#30302F";
            this.CurrentAttributeColor = "#2D2D2D";
            this.maxLength = txtblockPropety.Width;
            ApplyColor();
        }

        public GraphicsNode_UsrCtrl(string _type)
        {
            InitializeComponent();
            this.NodeType = _type;
            this.CurrentTitleColor = "#30302F";
            this.CurrentAttributeColor = "#2D2D2D";
            this.maxLength = txtblockPropety.Width;
            ApplyColor();
        }

        public void ApplyColor()
        {
            BrushConverter bc = new BrushConverter();
            TitleZone.Background = (Brush)bc.ConvertFrom(CurrentTitleColor);
            AttributesZone.Background = (Brush)bc.ConvertFrom(CurrentAttributeColor);
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

        public void SetAsInputNode()
        {
            this.Height = 25;
        }

        [Obsolete]
        public void txtPropety_AddLine(string lineInput)
        {
            if (txtblockPropety.Text == "")
            {
                txtblockPropety.Text += lineInput;
                this.Height -= 28;
            }
            else
                txtblockPropety.Text += System.Environment.NewLine + lineInput;


            FormattedText formattedText = new FormattedText(
            lineInput,
            new System.Globalization.CultureInfo("en-us", false),
            FlowDirection.LeftToRight,
            new Typeface("Segoe UI"),
            18,
            Brushes.Black);

            if (maxLength < formattedText.MinWidth)
            {
                this.Width = formattedText.MinWidth + 24;
                maxLength = lineInput.Length;
            }

            this.Height += 28;
        }

        public void Transform2SingleLine()
        {
            AttributeGrid.Visibility = System.Windows.Visibility.Hidden;
            TitleZone.CornerRadius = new CornerRadius(10, 10, 10, 10);
        }

        //public void CalcAnchorPoint()
        //{
        //    AnchorPoint = new AnchorPoint(this.Height, this.Width, Canvas.GetTop(this), Canvas.GetLeft(this));
        //}

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            TitleZone.Background = new SolidColorBrush(Colors.Gray);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            ApplyColor();
        }

        public void NodeByType(string _type)
        {
            switch (_type)
            {
                case "Conv2D":
                    txtTitle = "Conv2D";
                    CurrentTitleColor = "#385073";
                    ApplyColor();
                    break;
                case "InputLayer":
                    txtTitle = "Input";
                    CurrentTitleColor = "#414141";
                    ApplyColor();
                    Transform2SingleLine();
                    break;
                case "Add":
                    txtTitle = "Add";
                    CurrentTitleColor = "#30302F";
                    ApplyColor();
                    Transform2SingleLine();
                    break;
                case "Average":
                    txtTitle = "Average";
                    CurrentTitleColor = "#385037";
                    ApplyColor();
                    Transform2SingleLine();
                    break;
                case "AvgPool2D":
                    txtTitle = "AvgPool2D";
                    CurrentTitleColor = "#385037";
                    ApplyColor();
                    break;
                case "MaxPool2D":
                    txtTitle = "MaxPool2D";
                    CurrentTitleColor = "#385037";
                    ApplyColor();
                    break;
                default:
                    break;
            }
        }

        private void MainGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SlidePanel_Control.SlidePanel_Show(this.LayerName);
        }
    }
}
