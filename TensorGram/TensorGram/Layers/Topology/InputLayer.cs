using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers.Topology
{
    class InputLayer:Layer
    {
        
        public InputLayer()
        {
            this.Inboundlayer = null;
            this.LayerName = "input";
            this.Type = LayerTypes.InputLayer;
        }

        public InputLayer(string outbound)
        {
            this.LayerName = "input";
            this.OutboundLayer = outbound;
            this.Type = LayerTypes.InputLayer;
        }
    }
}
