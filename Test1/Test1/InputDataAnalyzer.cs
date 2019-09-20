using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test1.Layers;

namespace Test1
{
    public class InputDataAnalyzer
    {

        public string InputStandardizer(string raw)
        {
            string Temp = "";
            string[] strDestination = raw.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string pStr in strDestination)
            {
                Temp += pStr;
            }

            return Temp.Normalize();
        }

        public Layer DataConvert(string dataLine)
        {
            //dataLine = InputStandardizer(dataLine);
            Layer result = new Null();
            char[] delimiterChars = { '=', '.', '(', ')', ',', ' ', '\u0027' };
            string[] temp = dataLine.Split(delimiterChars);
            List<string> words = new List<string>();
            for(int i = 0; i< temp.Length; i++)
            {
                if (temp[i] != "")
                    words.Add(temp[i]);
            }
            int ArgumentStartIndex = -1;
            int ArgumentEndIndex = -1;
            for(int i = 0; i< words.Count; i++)
            {
                if (words[i] == "layers")
                {
                    LayerCreatorByName(words[i + 1], ref result);
                    ArgumentStartIndex = i + 2;
                    result.OutBoundLayer.Add(words[i - 1]);
                    break;
                }
            }

            for(int i = words.Count -1; i >= 0; i--)
            {
                result.Inboundlayer.Add(words[i]);
                ArgumentEndIndex = i - 1;
                if (words[i - 2] == "name")
                    break;
            }
            result.LayerDataInput(words.GetRange(ArgumentStartIndex, ArgumentEndIndex - ArgumentStartIndex + 1));
            return result;
        }

        public bool ReadInitNetwork(string input, ref string name, ref List<string> inbound)
        {
            bool Are = false;
            char[] delimiterChars = {'\u0020', ':', '(', ')'};
            string[] temp = input.Split(delimiterChars);
            List<string> words = new List<string>();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != "")
                    words.Add(temp[i]);
            }
 
            for (int i = 0; i < words.Count; i++)
            {
                if(words[i] == "def")
                {
                    i++;
                    name = words[i++];

                    for (; i < words.Count; i++)
                    {
                        inbound.Add(words[i]);
                    }
                    Are = true;
                    break;
                }
            }
            return Are;
        }

        public LayerNetwork InputDataHander(List<string> DataLines)
        {
            LayerNetwork network = null;
            for(int i = 0; i < DataLines.Count; i++)
            {
                string nwName = "";
                List<string> outbound = new List<string>();
                if (ReadInitNetwork(DataLines[i], ref nwName, ref outbound))
                {
                    network = new LayerNetwork(nwName);
                    network.setOutboundGraphstart(outbound);
                }
                else
                    network.Addlayer(DataConvert(DataLines[i]));
            }
            return network;
        }

        public static Layer NameCheck(string word)
        {
            Layer conv2d = new Null();
            LayerNames outValue;
            if (LayerNames.TryParse(word, out outValue))
            {
                conv2d = new Conv2D();
                return conv2d;
            }
            return conv2d;
        }

        public void LayerCreatorByName(string name, ref Layer input)
        {
            Layer temp = new Null();
            try
            {
                switch (name)
                {
                    case ("Conv2D"):
                        input = new Conv2D();
                        break;
                }
            }
            catch
            {

            }
        }
    }
}
