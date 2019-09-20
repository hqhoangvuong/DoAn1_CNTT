using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Topology
{
    public class Node
    {
        public Layer outbound_layer;
        public List<Layer> inbound_layers;
    }
}
