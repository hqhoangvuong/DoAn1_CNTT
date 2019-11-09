using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers
{
    class Dropout : Layer
    {
        public float rate;
        public int noise_shape;
        public int seed;

        public Dropout()
        {
            this.Type = LayerTypes.Dropout;
            
        }

        public Dropout(string layername)
        {
            this.Type = LayerTypes.Dropout;
            this.LayerName = layername;
        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex;
                int EndIndex;
                // Doc rate
                EndIndex = _input.IndexOf(',', 0);
                this.rate = float.Parse(_input.Substring(0, EndIndex));

                // Doc noise_shape
                StartIndex = EndIndex + 1;
                EndIndex = _input.IndexOf(',', StartIndex);

                this.noise_shape = int.Parse(_input.Substring(StartIndex, EndIndex - StartIndex));

                // Doc seed
                StartIndex = EndIndex + 1;
                EndIndex = _input.IndexOf(",name");
                string temp = _input.Substring(StartIndex, EndIndex - StartIndex);
                this.seed = int.Parse(_input.Substring(StartIndex, EndIndex - StartIndex));

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
            base.GraphicsNodeInitialize();
            GraphicsNode.txtPropety_AddLine("rate = " + this.rate);
            GraphicsNode.txtPropety_AddLine("noise_shape = " + this.noise_shape);
            GraphicsNode.txtPropety_AddLine("seed = " + this.seed);
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     rate = " + this.rate);
            ReturnListToString.Add("     noise_shape = " + this.noise_shape);
            ReturnListToString.Add("     seed = " + this.seed);
            return ReturnListToString;
        }
    }
}
