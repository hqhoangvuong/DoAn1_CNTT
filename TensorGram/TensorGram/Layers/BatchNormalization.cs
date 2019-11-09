using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers
{
    class BatchNormalization : Layer
    {
        public int axis;
        public float epsilon;
        bool center;
        bool scale;

        public BatchNormalization()
        {
            this.Type = LayerTypes.BatchNormalization;
            this.axis = -1;
            this.epsilon = 0.0f;
            this.center = false;
            this.scale = false;
        }

        public BatchNormalization(string layername)
        {
            this.Type = LayerTypes.BatchNormalization;
            this.LayerName = layername;
            this.axis = -1;
            this.epsilon = 0.0f;
            this.center = false;
            this.scale = false;
        }

        public override void ReadAttribute(string _input)
        {
            try
            {
                int StartIndex;
                int EndIndex;
                // Doc filter
                EndIndex = _input.IndexOf(',', 0) ; 
                this.axis = int.Parse(_input.Substring(0, EndIndex));

                // Doc epsilon
                StartIndex = EndIndex + 1;
                EndIndex = _input.IndexOf(',', StartIndex);

                this.epsilon = float.Parse(_input.Substring(StartIndex, EndIndex - StartIndex));

                // Doc center
                StartIndex = EndIndex + 2;
                EndIndex = _input.IndexOf("','");
                this.center = bool.Parse(_input.Substring(StartIndex, EndIndex - StartIndex));

                // Doc scale
                StartIndex = EndIndex + 3;
                EndIndex = _input.IndexOf("name='");
                string debug = _input.Substring(StartIndex, EndIndex - StartIndex - 2);
                this.scale = bool.Parse(_input.Substring(StartIndex, EndIndex - StartIndex - 2));

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
            GraphicsNode.txtPropety_AddLine("asix = " + this.axis);
            GraphicsNode.txtPropety_AddLine("epsilon = " + this.epsilon);
        }

        public override List<string> ToString()
        {
            base.ToString();
            ReturnListToString.Add("\nAttributes");
            ReturnListToString.Add("     axis = " + this.axis);
            ReturnListToString.Add("     epsilon = " + this.epsilon);
            ReturnListToString.Add("     center = " + this.center);
            ReturnListToString.Add("     scale = " + this.scale);
            return ReturnListToString;
        }
    }
}
