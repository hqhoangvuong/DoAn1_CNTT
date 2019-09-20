using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Layers
{
    
    public class Layer
    {
        public string Lname;
        public List<string> Inboundlayer;
        public List<string> OutBoundLayer;

        public Layer()
        {
            this.Lname = "";
            this.Inboundlayer = new List<string>();
            this.OutBoundLayer = new List<string>();
        }

        public virtual bool LayerDataInput(List<string> rawData)
        {
            return true;
        }

        public virtual List<string> Attribute2String()
        {
            List<string> returnValue = new List<string>();
            return returnValue;
        }
    }
}
