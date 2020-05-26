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
            double d;
            if (table.TryGetValue(this.Value, out d))
            {
                //Console.Write(d + " ");
                s.Add(d.ToString());
            }
            else
            {
               // Console.Write(Value + " ");
                s.Add(Value.ToString());
            }
            if (this.left != null)
            {
                s.AddRange(this.left.CalcSubTree(table));
            }
            
                    
            if (this.right != null)
            {
                s.AddRange(this.right.CalcSubTree(table));
            }
            return s.ToArray();
        }

        public static void CalcExpression(Stack<string> stack1, char action)
        {
            double u = Convert.ToDouble(stack1.Peek());
            stack1.Pop();
            double v = Convert.ToDouble(stack1.Peek());
            stack1.Pop();
            double ans = 0;

            if (action == '+')
            {
                ans = v + u;
            }
            else if (action == '-')
            {
                ans = v - u;
            }
            else if (action == '*')
            {
                ans = v * u;
            }
            else if (action == '/')
            {
                ans = v / u;
            }
            else if (action == '^')
            {
                ans = (int)Math.Pow(v, u);
            }
            stack1.Push(Convert.ToString(ans));
        }
        public double Calc(Dictionary<string, double> table)
        {
            Stack<string> stack1 = new Stack<string>();
            Stack<string> stackOp = new Stack<string>();
            string[] s = this.CalcSubTree(table);
            s.Reverse();
            foreach (string to in s)
            {
                if (opers.Contains(to))
                {
                    stackOp.Push(to);
                }
                else
                {
                    stack1.Push(to);
                }
            }
            while(stackOp.Count > 0)
            {
                CalcExpression(stack1, Convert.ToChar(stackOp.Peek()));
                stackOp.Pop();
            }
            return Convert.ToDouble(stack1.Peek());
        }

        public bool Condition(Dictionary<string, double> table)
        {
            string action = this.Value;
            if (Array.IndexOf(opers, action) <= 4)
            {
                double ans = this.Calc(table);
                if (ans == 0)
                    return false;
                else
                    return true;
            }
            else
            {
                double l = this.left.Calc(table);
                double r = this.right.Calc(table);
                switch (action)
                {
                    case "=":
                        {
                            if (l == r)
                                return true;
                            else
                                return false;
                        }
                    case ">":
                        {
                            if (l > r)
                                return true;
                            else
                                return false;
                        }
                    case "<":
                        {
                            if (l < r)
                                return true;
                            else
                                return false;
                        }
                }
            }
            Console.WriteLine("This code should not work");
            return false;
        }
    }
}
