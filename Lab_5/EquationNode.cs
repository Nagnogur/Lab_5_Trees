using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class EquationNode : Node
    {
        public VariableNode variable;
        public ValueNode value;

        public EquationNode(VariableNode var, ValueNode val)
        {
            Type = "Equation";
            variable = var;
            value = val;
        }
    }
}
