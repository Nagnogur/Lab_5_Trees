using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class ValueNode : Node
    {
        double value;
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public ValueNode(double val)
        {
            Type = "Value";
            value = val;
        }
    }
}
