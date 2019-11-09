using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorGram.Layers.Topology;

namespace TensorGram.Layers
{
    class MaxPool2D : Layer
    {
        public List<int> pool_size;
        public List<int> strides;
        public string padding;
        public string data_format;

        public MaxPool2D()
        {
            this.Type = LayerTypes.MaxPool2D;
            this.pool_size = new List<int>();
            this.strides = new List<int>();
            this.padding = "";
            this.data_format = "";
        }

        public MaxPool2D(string name)
        {
            this.Type = LayerTypes.MaxPool2D;
            this.LayerName = name;
            this.pool_size = new List<int>();
            this.strides = new List<int>();
            this.padding = "";
            this.data_format = "";
        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex = -1;
                int EndIndex = -1;
                // ** Doc pool_size **
                if (_input[0] == '(')
                {
                    // Truong hop pool_size la tuple of 2 integers
                    StartIndex = 1; // Lay vi tri dau '(' dau tien trong chuoi + 1
                    EndIndex = _input.IndexOf(')', 0); // Lay vi tri dau ')' dau tien trong chuoi
                    foreach (string temp in _input.Substring(StartIndex, EndIndex - StartIndex).Split(','))
                    {
                        pool_size.Add(int.Parse(temp));
                    }
                }
                else
                {
                    // Truong hop pool_size la integer
                    EndIndex = _input.IndexOf(',', 0); // Lay vi tri dau ',' dau tien
                    this.pool_size.Add(int.Parse(_input.Substring(0, EndIndex)));
                }

                // ** Doc strides **
                EndIndex = _input.IndexOf(",padding", 0);
                if (pool_size.Count == 2)
                {
                    StartIndex = _input.IndexOf(')', 0) + 2;
                }
                else if (pool_size.Count == 1)
                {
                    StartIndex = _input.IndexOf(',', 0) + 1;
                }
                // string temp1 = _input.Substring(StartIndex, EndIndex - StartIndex);
                if (_input.Substring(StartIndex, EndIndex - StartIndex) == "None")
                {

                    this.strides = this.pool_size;
                }
                else
                {
                    if (_input.Substring(StartIndex, EndIndex - StartIndex)[0] == '(')
                    {
                        // Truong hop strides la tuple of 2 integers
                        StartIndex++;
                        EndIndex--;
                        foreach (string temp in _input.Substring(StartIndex, EndIndex - StartIndex).Split(','))
                        {
                            this.strides.Add(int.Parse(temp));
                        }
                    }
                    else
                    {
                        this.strides.Add(int.Parse(_input.Substring(StartIndex, EndIndex - StartIndex)));
                    }
                }

                // ** Doc padding **
                StartIndex = _input.IndexOf("padding='") + 9;
                EndIndex = _input.IndexOf("',data_format='");
                this.padding = _input.Substring(StartIndex, EndIndex - StartIndex);

                // ** Doc data_format
                StartIndex = EndIndex + 15;
                EndIndex = _input.IndexOf("',name='");
                this.data_format = _input.Substring(StartIndex, EndIndex - StartIndex);

                // ** Doc ten layer **
                StartIndex = _input.IndexOf("name='") + 6;
                EndIndex = _input.LastIndexOf("'");
                this.LayerName = _input.Substring(StartIndex, EndIndex - StartIndex);
            }
            catch
            {

            }

        }

        public override void GraphicsNodeInitialize()
        {
            base.GraphicsNodeInitialize();
            string temp1 = "";
            temp1 = "pool_size = (";
            foreach (int i in pool_size)
                temp1 += i.ToString() + ", ";
            temp1 = temp1.Remove(temp1.LastIndexOf(", "));
            temp1 += ")";
            GraphicsNode.txtPropety_AddLine(temp1);

            temp1 = "strides = (";
            foreach (int i in strides)
                temp1 += i.ToString() + ", ";
            temp1 = temp1.Remove(temp1.LastIndexOf(", "));
            temp1 += ")";
            GraphicsNode.txtPropety_AddLine(temp1);
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     strides(" + string.Join(", ", strides) + ")");
            ReturnListToString.Add("     padding: " + this.padding.ToString());
            ReturnListToString.Add("     data format: " + this.data_format);
            return ReturnListToString;
        }
    }
}
