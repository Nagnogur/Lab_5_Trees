using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class ExpressionNode : Node
    {
        public List<Node> child = new List<Node>();

        public ExpressionNode()
        {
            Type = "Expression";
        }
        public void AddNode(Node node)
        {
            child.Add(node);
        }
    }
}
