using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorGram.Layers.Topology;

namespace TensorGram.Layers
{
    class AvgPool2D : Layer
    {
        public List<int> pool_size;
        public List<int> strides;
        public string padding;
        public string data_format;

        public AvgPool2D()
        {
            this.pool_size = new List<int>();
            this.strides = new List<int>();
            this.padding = "";
            this.data_format = "";
        }

        public AvgPool2D(string name)
        {
            this.LayerName = name;
            this.pool_size = new List<int>();
            this.strides = new List<int>();
            this.padding = "";
            this.data_format = "";
        }

        public override void ReadAttribute(string _input)
        {
            base.ReadAttribute(_input);
        }
    }
}
