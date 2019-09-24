using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TensorGram.Layers;
using TensorGram.Layers.Topology;

namespace TensorGram
{
    public class TextInput_Hander
    {
        protected List<string> RawTextData;
        public TextInput_Hander(string textinput)
        {
            try
            {
                // Get du lieu dau vao thanh tung dong, luu vao List RawTextData, dong thoi luoc bo nhung dong trong
                RawTextData = new List<string>();
                foreach (string myString in textinput.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Luoc bo nhung khoang trong (space) trong tung dong
                    RawTextData.Add(myString.Replace(" ", string.Empty));
                }

                ReadModel(RawTextData);
            }
            catch
            {
                // Bat loi
            }
        }

        public TensorModel ReadModel(List<string> _RawData)
        {
            TensorModel Model = new TensorModel();
            try
            {
                // Doc dong dau tien, tao lop InputLayer va set ten cho model
                var _stringtemp = _RawData[0].Split(new char[] { '(', ')' });
                Model.ModelName = _stringtemp[0].Replace("def", string.Empty);
                InputLayer _inputlayer;
                Model.Layers.Add(_inputlayer = new InputLayer(_stringtemp[1]));

                // Doc cac dong tiep theo la thu tu cac lop cua Model
                for (int i = 1; i < _RawData.Count; i++)
                {
                    Layer _layertemp = new Layer();
                    Model.Layers.Add(LayerReader(_RawData[i]));
                }
            }
            catch
            {

            }
            // Tra ve Tensorflow Model
            return Model;
        }

        public Layer LayerReader(string _input)
        {
            Layer layertemp = new Layer();
            int TempIndexStart;
            int TempIndexEnd;

            // Lay type cua layer
            // Tao layer theo type cua no
            TempIndexStart = _input.IndexOf(".", 0) + 1; // Vi tri dau '.' dau tien trong chuoi + 1
            TempIndexEnd = _input.IndexOf('(', 0); // Vi tri dau '(' dau tien trong chuoi
            string layerType = _input.Substring(TempIndexStart, TempIndexEnd - TempIndexStart);
            layertemp = CreateLayerByType(layerType);

            // Lay Outbound cua layer
            layertemp.OutboundLayer = _input.Split(new string[] { "=layers." }, StringSplitOptions.None)[0];

            // Lay list Inbound cua layer
            TempIndexStart = _input.LastIndexOf('(') + 1; // Vi tri dau '(' cuoi cung trong chuoi + 1
            TempIndexEnd = _input.Length; // Do dai chuoi
            foreach(string temp in _input.Substring(TempIndexStart, TempIndexEnd - TempIndexStart - 1).Split(','))
            {
                layertemp.Inboundlayer.Add(temp);
            }

            // Lay chuoi chua Attribute cua string
            TempIndexStart = _input.IndexOf('(', 0) + 1; // Vi tri dau '(' dau tien trong chuoi + 1
            TempIndexEnd = _input.LastIndexOf('(') - 1; // Vi tri dau '(' cuoi cung trong chuoi - 1
            string Attribute = _input.Substring(TempIndexStart, TempIndexEnd - TempIndexStart);

            // Tao layer theo type cua no roi truyen string Attribute vao doc du lieu => Dua ra layer hoan chinh
            // Ham doc du lieu tu string Attribute thi moi layer co mot ham rieng, goi tu layer do
            layertemp.ReadAttribute(Attribute);

            return layertemp;
        }

        public Layer CreateLayerByType(string type)
        {
            switch(type)
            {
                case "Conv2D":
                    return new Conv2D();
                case "AvgPool2D":
                    return new AvgPool2D();
                case "Add":
                    break;
            }

            return new Layer();
        }
    }
}
