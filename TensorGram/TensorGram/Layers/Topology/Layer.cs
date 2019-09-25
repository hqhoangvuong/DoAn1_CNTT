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
        SoftMax,
        InputLayer
    }

    public class Layer:IDisposable
    {
        public bool disposed;
        public string LayerName;
        public LayerTypes Type;
        public List<string> Inboundlayer;
        public List<Layer> ChildNode;
        public string OutboundLayer;
        public double Mod;
        public GraphicsObject.GraphicsNode_UsrCtrl GraphicsNode;
        public bool isRendered;
        public List<string> ReturnListToString;

        public Layer()
        {
            this.LayerName = "";
            this.Type = LayerTypes.Layer;
            this.Inboundlayer = new List<string>();
            this.OutboundLayer = "";
            GraphicsNode = new GraphicsObject.GraphicsNode_UsrCtrl();
            this.ChildNode = new List<Layer>();
            isRendered = false;
            this.Mod = 0;
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

        public virtual void ReadAttribute(string _input)
        {

        }

        [Obsolete]
        public virtual void GraphicsNodeInitialize()
        {
            GraphicsNode.LayerName = this.LayerName;
        }

        public virtual List<string> GetAttribute()
        {
            return new List<string>();
        }

        public virtual List<string> ToString()
        {
            ReturnListToString = new List<string>();
            ReturnListToString.Add("Layer name: " + this.LayerName);
            ReturnListToString.Add("Layer type: " + Enum.GetName(typeof(LayerTypes), this.Type));
            return ReturnListToString;
        }

    }


}
