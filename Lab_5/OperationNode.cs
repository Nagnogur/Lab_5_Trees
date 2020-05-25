using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    class OperationNode : Node
    {
        string value;
        string[] opers = { "+", "-", "*", "/", "^" };
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public OperationNode left = null;
        public OperationNode right = null;

        public OperationNode(string s)//, OperationNode l, OperationNode r)
        {
            Type = "Operation";
            value = s;
           // left = l;
            //right = r;
        }

        public void WriteSubTree(OperationNode t)
        {
            if (t != null)
            {
                WriteSubTree(t.left);
                Console.Write(t.Value + " ");
                WriteSubTree(t.right);
            }
        }

        public OperationNode BuildSubTree(string[] expr)
        {
            Stack<OperationNode> st = new Stack<OperationNode>();
            for (int i = 0; i < expr.Length; i++)
            {
                if (!opers.Contains(expr[i]))
                {
                    st.Push(new OperationNode(expr[i]));
                }
                else
                {
                    OperationNode root = new OperationNode(expr[i]);
                    root.right = st.Pop();
                    root.left = st.Pop();
                    st.Push(root);
                }
            }
            return st.Pop();
        }
    }
}
