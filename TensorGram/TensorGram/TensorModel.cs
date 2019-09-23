using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorGram.Layers;
using TensorGram.Layers.Topology;

namespace TensorGram
{
    public class TensorModel
    {
        public string ModelName;
        public List<Layer> Layers;

        public TensorModel()
        {
            this.ModelName = "";
            this.Layers = new List<Layer>();
        }

        public TensorModel(string name)
        {
            this.ModelName = name;
            this.Layers = new List<Layer>();
        }
    }
}
