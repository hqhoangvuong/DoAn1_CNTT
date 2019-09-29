using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorGram.Layers.Topology;

namespace TensorGram.Layers.Topology
{
    class Conv2D:Layer
    {
        public int filter;
        public List<int> kernel_size;
        public string activation;

        public Conv2D()
        {
            this.Type = LayerTypes.Conv2D;
            this.kernel_size = new List<int>();
            this.activation = "";
            this.filter = -1;
        }

        public Conv2D(string layername)
        {
            this.LayerName = layername;
            this.Type = LayerTypes.Conv2D;
            this.kernel_size = new List<int>();
            this.activation = "";
            this.filter = -1;
        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex;
                int EndIndex;
                // Doc filter
                EndIndex = _input.IndexOf('(', 0) - 1; // Lay vi tri dau '(' dau tien trong chuoi - 1 ==> Vi tri dau ',' dau tien
                this.filter = int.Parse(_input.Substring(0, EndIndex));

                // Doc kernel_size
                StartIndex = EndIndex + 2; // Lay vi tri dau '(' dau tien trong chuoi + 1
                EndIndex = _input.LastIndexOf(')'); // Lay vi tri dau ')' cuoi cung trong chuoi
                //string temp = _input.Substring(StartIndex, EndIndex - StartIndex);
                foreach (string temp in _input.Substring(StartIndex, EndIndex - StartIndex).Split(','))
                {
                    kernel_size.Add(int.Parse(temp));
                }

                // Doc gia tri activation
                StartIndex = _input.IndexOf("activation='") + 12;
                EndIndex = _input.IndexOf("',");
                this.activation = _input.Substring(StartIndex, EndIndex - StartIndex);

                // Doc ten layer
                StartIndex = _input.IndexOf("name='") + 6;
                EndIndex = _input.LastIndexOf("'");
                this.LayerName = _input.Substring(StartIndex, EndIndex - StartIndex);
            }
            catch
            {

            }
        }

        [Obsolete]
        public override void GraphicsNodeInitialize()
        {
            this.GraphicsNode.NodeByType(Enum.GetName(typeof(LayerTypes), this.Type));

            GraphicsNode.txtPropety_AddLine("filter = " + filter);

            string temp1 = "";
            temp1 = "kernel_size = (";
            foreach (int i in kernel_size)
                temp1 += i.ToString() + ", ";
            temp1 = temp1.Remove(temp1.LastIndexOf(", "));
            temp1 += ")";
            GraphicsNode.txtPropety_AddLine(temp1);
            base.GraphicsNodeInitialize();
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     filter: " + this.filter.ToString());
            ReturnListToString.Add("     activation: " + this.activation);
            ReturnListToString.Add("\nInputs");
            ReturnListToString.Add("     kernel_size (" + string.Join(", ", kernel_size) +")");
            foreach (string i in this.Inboundlayer)
                ReturnListToString.Add("     " + i);
            ReturnListToString.Add("\nOutputs");
            ReturnListToString.Add("     " + this.OutboundLayer);
            return ReturnListToString;
        }
    }
}
