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
        public string OutboundLayer;

        public Layer()
        {
            this.LayerName = "";
            this.Type = LayerTypes.Layer;
            this.Inboundlayer = new List<string>();
            this.OutboundLayer = "";
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

    }


}
