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
        string[] opers = { "+", "-", "*", "/", "^", "=", ">", "<" };
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public OperationNode left = null;
        public OperationNode right = null;

        public OperationNode()
        {
            Type = "Operation";
            //value = s;
            // left = l;
            //right = r;
        }

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
            for (int i = expr.Length - 1; i >= 0; i--)
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

        public string[] CalcSubTree(Dictionary<string, double> table)
        {
            List<string> s = new List<string>();
            if (this.left != null)
            {
                s.AddRange(this.left.CalcSubTree(table));
            }
            double d;
            if (table.TryGetValue(this.Value, out d))
            {
                Console.Write(d + " ");
                s.Add(d.ToString());
            }
            else
            {
                Console.Write(Value + " ");
                s.Add(Value.ToString());
            }
                    
            if (this.right != null)
            {
                s.AddRange(this.right.CalcSubTree(table));
            }
            return s.ToArray();
        }
    }
}
