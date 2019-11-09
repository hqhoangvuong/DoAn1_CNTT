using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers.Topology
{
    public abstract class ILayer
    {
        public ILayer()
        {

        }

        public abstract void ReadAttribute(string _input);

        public abstract void GraphicsNodeInitialize();

        public abstract List<string> GetAttribute();

        public abstract List<string> ToString();

    }
}
