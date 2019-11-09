using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers
{
    class Activation:Layer 
    {
        public string activation;

        public Activation()
        {
            this.Type = LayerTypes.Activation;
            this.activation = "";
        }

        public Activation(string layername)
        {
            this.LayerName = layername;
            this.activation = "";
        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex;
                int EndIndex;

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

        public override void GraphicsNodeInitialize()
        {

            GraphicsNode.txtPropety_AddLine("activation = " + this.activation);
            base.GraphicsNodeInitialize();
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     activation: " + this.activation);
            ReturnListToString.Add("\nInputs");
            foreach (string i in this.Inboundlayer)
                ReturnListToString.Add("     " + i);
            ReturnListToString.Add("\nOutputs");
            ReturnListToString.Add("     " + this.OutboundLayer);
            return ReturnListToString;
        }
    }
}
