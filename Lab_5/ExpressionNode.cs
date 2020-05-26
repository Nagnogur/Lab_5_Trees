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

       
        public void CalculateTree(Dictionary<string, double> table)
        {
            for (int i = 0; i < this.child.Count; i++)
            {
                string type = this.child[i].Type;
                switch(type)
                {
                    case "Equation":
                        {
                            EquationNode t = (EquationNode)this.child[i];
                            if (table.ContainsKey(t.variable.Value))
                            {
                                table[t.variable.Value] = t.value.Value;
                            }
                            else
                            {
                                table.Add(t.variable.Value, t.value.Value);
                            }
                            break;
                        }
                    case "Operation":
                        {
                            OperationNode t = (OperationNode)this.child[i];
                            Console.WriteLine("result = " + t.Calc(table));
                            //Console.WriteLine();
                            break;
                        }
                    case "Condition":
                        {
                            IfNode ifNode = (IfNode)this.child[i];
                            if (ifNode.condition.Condition(table))
                            {
                                ifNode.expression.CalculateTree(table);
                            }
                            break;
                        }
                }
            }
        }
    }
}
