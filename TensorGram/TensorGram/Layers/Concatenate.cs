using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers
{
    class Concatenate : Layer
    {
        public int axis;

        public Concatenate()
        {
            this.Type = LayerTypes.Concatenate;
            this.axis = -1;
        }

        public Concatenate(string layername)
        {
            this.Type = LayerTypes.Concatenate;
            this.LayerName = layername;
            this.axis = -1;
        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex;
                int EndIndex;
                // Doc axis
                EndIndex = _input.IndexOf(',', 0); // Lay vi tri dau '(' dau tien trong chuoi - 1 ==> Vi tri dau ',' dau tien
                this.axis = int.Parse(_input.Substring(0, EndIndex));

                // Doc ten layer
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
            this.GraphicsNode.NodeByType(Enum.GetName(typeof(LayerTypes), this.Type));
            GraphicsNode.txtPropety_AddLine("axis = " + this.axis);
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     axis = " + this.axis);
            return ReturnListToString;
        }
    }
}
