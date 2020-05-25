using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class VariableNode
    {
        string type = "Variable";
        string value;
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public VariableNode(string val)
        {
            value = val;
        }
    }
}
