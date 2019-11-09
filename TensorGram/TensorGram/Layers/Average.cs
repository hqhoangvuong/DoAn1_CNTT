using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers
{
    class Average:Layer
    {
        public Average()
        {
            this.Type = LayerTypes.Average;
        }

        public Average(string layername)
        {
            this.Type = LayerTypes.Average;
            this.LayerName = layername;

        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex;
                int EndIndex;
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
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     None");
            return ReturnListToString;
        }
    }
}
