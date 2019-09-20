using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1.Layers
{
    class Conv2D : Layer
    {
        public int filters;
        public List<int> kernel_size;
        public string activation;

        public Conv2D()
        {
            this.filters = -1;
            this.kernel_size = new List<int>();
            this.activation = "null";
        }


        public override bool LayerDataInput(List<string> rawData)
        {
            string prevousWord = "";
            foreach(string currentWord in rawData)
            {
                int tempNum = -1;
                if(int.TryParse(currentWord, out tempNum) && prevousWord == "")
                {
                    filters = tempNum;
                    prevousWord = "filters";
                    continue;
                }

                if(int.TryParse(currentWord, out tempNum) && (prevousWord == "filters" || prevousWord == "kernel_size"))
                {
                    kernel_size.Add(tempNum);
                    prevousWord = "kernel_size";
                    continue;
                }

                if(currentWord == "activation" && prevousWord == "kernel_size")
                {
                    prevousWord = "activation";
                    continue;
                }

                if(prevousWord == "activation")
                {
                    this.activation = currentWord;
                    prevousWord = "name";
                }

                if(prevousWord == "name")
                {
                    this.Lname = currentWord;
                }
            }
            return true;
        }

        public override List<string> Attribute2String()
        {
            List<string> returnValue = new List<string>();
            // For filter
            string s1 = "filter <" + filters.ToString() + ">";
            returnValue.Add(s1);

            // For kernel_size
            s1 = "kernel_size <"; 
            foreach(int i in kernel_size)
            {
                s1 += i.ToString() + ", ";
            }
            s1 = s1.Remove(s1.Length - 1) + ">";
            returnValue.Add(s1);

            //For Activation
            s1 = "Activation <" + activation + ">";
            returnValue.Add(s1);
            return returnValue;
        }
    }
}
