using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Topology
{
    public abstract class Layer
    {
        public string name;
        public List<Node> outbound_nodes;
        public List<Node> inbound_nodes;

    }
}
