using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Layers;
using Test1.Layers.Topology;

namespace Test1
{
    public class LayerNetwork
    {
        public InputLayer GraphStart;
        public string NetworkName;
        public List<Layer> ListLayers;

        public LayerNetwork(string networkname)
        {
            this.NetworkName = networkname;
            ListLayers = new List<Layer>();
            this.GraphStart = new InputLayer(networkname);
        }

        public void setOutboundGraphstart(List<string> input)
        {
            this.GraphStart.OutBoundLayer = input;
        }

        public void Addlayer(Layer input)
        {
            ListLayers.Add(input);
        }

    }
}
