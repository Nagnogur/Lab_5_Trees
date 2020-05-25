using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class ValueNode : Node
    {
        public string type = "Value";
        double value;
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public ValueNode(double val)
        {
            value = val;
        }
    }
}
