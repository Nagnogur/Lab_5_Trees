using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class IfNode : Node
    {
        bool isTrue;

        public bool IsTrue
        {
            get { return isTrue; }
            set { isTrue = value; }
        }

        public OperationNode condition;
        public ExpressionNode expression;

        public IfNode()
        {
            Type = "Condition";
        }
    }
}
