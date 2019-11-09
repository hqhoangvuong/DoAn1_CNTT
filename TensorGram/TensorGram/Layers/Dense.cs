using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers
{
    class Dense:Layer
    {
        public int units;
        public string activation;

        public Dense()
        {
            this.Type = LayerTypes.Dense;
            this.units = -1;
            activation = "";
        }

        public Dense(string layername)
        {
            this.Type = LayerTypes.Dense;
            this.LayerName = layername;
            this.units = -1;
            activation = "";
        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex;
                int EndIndex;
                // Doc axis
                EndIndex = _input.IndexOf(',', 0); // Lay vi tri dau '(' dau tien trong chuoi - 1 ==> Vi tri dau ',' dau tien
                this.units = int.Parse(_input.Substring(0, EndIndex));

                StartIndex = EndIndex + 2;
                EndIndex = _input.IndexOf("name='");
                this.activation = _input.Substring(StartIndex, EndIndex - 6);

                // Doc ten layer
                StartIndex = EndIndex + 6;
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
            GraphicsNode.txtPropety_AddLine("units = " + this.units);
            GraphicsNode.txtPropety_AddLine("activation = " + this.activation);
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     units = " + this.units);
            ReturnListToString.Add("     activation = " + this.activation);
            return ReturnListToString;
        }
    }
}
