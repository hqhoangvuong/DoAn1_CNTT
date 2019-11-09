using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorGram.Layers
{
    public enum LayerTypes
    {
        Layer,
        Conv2D,
        Activation,
        Add,
        Average,
        AvgPool2D,
        BatchNormalization,
        Concatenate,
        Dense,
        Dropout,
        MaxPool2D,
        Softmax,
        InputLayer
    }

    public class Layer: Topology.ILayer, IDisposable
    {
        public bool disposed;
        public string LayerName;
        public LayerTypes Type;
        public List<string> Inboundlayer;
        public List<Layer> ChildLayer;
        public List<Layer> ParentLayer;
        public string OutboundLayer;
        public GraphicsObject.GraphicsNode_UsrCtrl GraphicsNode;
        public bool isRendered;
        public List<string> ReturnListToString;
        public int Level = -1;

        public Layer()
        {
            this.LayerName = "";
            this.Type = LayerTypes.Layer;
            this.Inboundlayer = new List<string>();
            this.OutboundLayer = "";
            GraphicsNode = new GraphicsObject.GraphicsNode_UsrCtrl();
            this.ChildLayer = new List<Layer>();
            this.ParentLayer = new List<Layer>();
            this.isRendered = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.LayerName = string.Empty;
                    this.Type = LayerTypes.Layer;
                    this.Inboundlayer = null;
                    this.OutboundLayer = string.Empty;
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override void ReadAttribute(string _input)
        {

        }

        public override void GraphicsNodeInitialize()
        {
            this.GraphicsNode.NodeByType(Enum.GetName(typeof(LayerTypes), this.Type));
            GraphicsNode.LayerName = this.LayerName;
        }

        public override List<string> GetAttribute()
        {
            return new List<string>();
        }

        public override List<string> ToString()
        {
            ReturnListToString = new List<string>();
            ReturnListToString.Add("     name: " + this.LayerName);
            ReturnListToString.Add("     type: " + Enum.GetName(typeof(LayerTypes), this.Type));
            ReturnListToString.Add("    ");
            ReturnListToString.Add("\nInputs");
            foreach (string i in this.Inboundlayer)
                ReturnListToString.Add("     " + i);
            ReturnListToString.Add("\nOutputs");
            ReturnListToString.Add("     " + this.OutboundLayer);
            ReturnListToString.Add("    ");
            return ReturnListToString;
        }

    }


}
