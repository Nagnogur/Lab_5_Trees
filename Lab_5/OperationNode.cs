using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class OperationNode : Node
    {
        public string type = "operation";
        Node left;
        Node right;

        public OperationNode(Node l, Node r)
        {
            left = l;
            right = r;
        }
    }
}
