using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class ExpressionNode
    {
        string type = "expression";
        List<Node> child = new List<Node>();

        public void AddNode(Node node)
        {
            child.Add(node);
        }
    }
}
