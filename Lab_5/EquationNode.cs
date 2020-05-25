using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class EquationNode : Node
    {
        public string type = "Equation";
        VariableNode variable;
        ValueNode value;

        public EquationNode(VariableNode var, ValueNode val)
        {
            variable = var;
            value = val;
        }
    }
}
