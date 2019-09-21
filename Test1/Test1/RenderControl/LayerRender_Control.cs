using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.RenderItems;
using Test1.Layers;

namespace Test1.RenderControl
{
    class LayerRender_Control
    {
        protected LayerNetwork Network_RawData;
        protected List<GraphLayer_UsrControl> GraphicsItemsList;
        public LayerRender_Control(LayerNetwork network)
        {
            this.Network_RawData = network;
            Data2GraphicsItems();
        }

        public void Data2GraphicsItems()
        {
            if (Network_RawData == null)
                return;
            else
            {
                GraphicsItemsList = new List<GraphLayer_UsrControl>();
                GraphLayer_UsrControl InputNode = new GraphLayer_UsrControl();
                InputNode.SetAsInputNode();
                InputNode.txtTitle = Network_RawData.NetworkName;
                GraphicsItemsList.Add(InputNode);
                foreach(Layer item in Network_RawData.ListLayers)
                {
                    GraphLayer_UsrControl layerItem = new GraphLayer_UsrControl();
                    layerItem.txtTitle = item.Lname;
                    foreach(string temp in item.Attribute2String())
                    {
                        layerItem.txtPropety_AddLine(temp);
                    }
                    GraphicsItemsList.Add(layerItem);
                }
            }
        }

        public List<GraphLayer_UsrControl> RenderSource
        {
            get { return this.GraphicsItemsList; }
        }

        public void Render(System.Windows.Controls.Canvas RenderZone, System.Windows.Controls.Grid ContainGrid)
        {
            double Zone_Width = RenderZone.Width / 2;
            double Space = 30;
            foreach (GraphLayer_UsrControl item in GraphicsItemsList)
            {
                System.Windows.Controls.Canvas.SetLeft(item, ContainGrid.ActualWidth / 2 - item.Width / 2);
                System.Windows.Controls.Canvas.SetTop(item, Space);
                Space += item.Height + 40;
                RenderZone.Children.Add(item);
            }
        }
    }
}
