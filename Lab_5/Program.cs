﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{

    class Program
    {
        
        // 3 +4 *2/( 1- 5)^ 2^3
        // if(...){...}
        static void Main(string[] args)
        {
            string[] inp = File.ReadAllLines(@"input.txt");

            List<Tuple<char, int, string>> operators = new List<Tuple<char, int, string>>();
            char[] opers = { '+', '-', '*', '/', '^', '=' , '<' , '>'};
            operators.Add(Tuple.Create('=', 1, "left"));
            operators.Add(Tuple.Create('+', 2, "left"));
            operators.Add(Tuple.Create('-', 2, "left"));
            operators.Add(Tuple.Create('*', 3, "left"));
            operators.Add(Tuple.Create('/', 3, "left"));
            operators.Add(Tuple.Create('^', 4, "right"));
            operators.Add(Tuple.Create('<', 1, "left"));
            operators.Add(Tuple.Create('>', 1, "left"));

            Dictionary<string, double> values = new Dictionary<string, double>();

            List<string> fullStr = new List<string>();
            string s1 = "";

            for (int i = 0; i < inp.Length; i++)
            {

                inp[i] = inp[i].Replace(" ", "");
                inp[i] = inp[i].Replace("\t", "");
                int j = 0;
                while (j < inp[i].Length)
                {
                    bool flag = false;
                    switch (inp[i][j])
                    {
                        case ';':
                            {
                                fullStr.Add(s1);
                                s1 = "";
                                flag = true;
                                break;
                            }
                        case '{':
                            {
                                fullStr.Add("{");
                                fullStr.Add(s1);
                                
                                s1 = "";
                                flag = true;
                                break;
                            }
                        case '}':
                            {
                                fullStr.Add("}");
                                s1 = "";
                                flag = true;
                                break;
                            }
                    }
                    if (!flag)
                        s1 += inp[i][j];
                    j++;
                }
            }


            /* string[] sss = { "=", "a", "c" }; //{ "-", "c", "+", "b", "a" };
             // OperationNode op1 = new OperationNode("+", new OperationNode("-", new OperationNode("a", null, null), new OperationNode("6.2", null, null)), new OperationNode("c", null, null));
             OperationNode op1 = new OperationNode("").BuildSubTree(sss);
             op1.WriteSubTree(op1);*/


            /*
                        ExpressionNode expr = new ExpressionNode();
                        VariableNode abc = new VariableNode("abc");
                        ValueNode vvv = new ValueNode(6);
                        EquationNode f = new EquationNode(abc, vvv);
                        expr.AddNode(f);
                        Console.WriteLine(expr.child[0].Type);
                        if (expr.child[0].Type == "Equation")
                        {
                            EquationNode eq = (EquationNode)expr.child[0];
                            Console.WriteLine(eq.variable.Value + " = " + eq.value.Value);
                        }*/

            ExpressionNode expression = new ExpressionNode();
            ExpressionNode ifExpr = new ExpressionNode();
            IfNode ifNode = new IfNode();

            bool ifState = false;
            bool statement = false;

            int len = fullStr.Count;
            for (int i = 0; i < fullStr.Count; i++)
            {
                bool equation = false;
                if (ifState)
                {
                    ifState = false;
                    statement = true;
                }
                if (fullStr[i][0] == '}')
                {
                    ifNode.expression = ifExpr;
                    expression.AddNode(ifNode);
                    
                    statement = false;
                    ifNode = null;
                    ifExpr = null;
                    continue;
                }
                else if (fullStr[i][0] == '{')
                {
                    i++;
                    s1 = fullStr[i].Substring(3, fullStr[i].Length - 4);
                    ifState = true;
                }
                else
                {
                    s1 = fullStr[i];
                }
                ///////////////////////////////////
                Stack<string> output = new Stack<string>();
                Stack<char> operatorStack = new Stack<char>();
                bool negative = true;
                string s = "";

                for (int j = 0; j < s1.Length; j++)
                {
                    if (s1[j] == '=')
                    {
                        equation = true;
                    }
                    //Console.WriteLine(s1[j]);
                    if (s1[j] == '(')  //   (
                    {
                        operatorStack.Push(s1[j]);
                        negative = true;
                    }

                    else if (s1[j] == ')')   //      )
                    {
                        while (operatorStack.Peek() != '(')
                        {
                            output.Push(Convert.ToString(operatorStack.Peek()));
                            operatorStack.Pop();
                        }
                        operatorStack.Pop();
                        negative = false;
                    }

                    else if (!negative && opers.Contains(s1[j]))      //      +,-,*,/,^
                    {
                        int index = Array.IndexOf(opers, s1[j]);
                        if (operatorStack.Count == 0 || operatorStack.Peek() == '(' || (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 < operators[index].Item2))
                        {
                            operatorStack.Push(s1[j]);
                        }
                        else
                        {
                            while (operatorStack.Count > 0 && operatorStack.Peek() != '(' && (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 > operators[index].Item2 || (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 == operators[index].Item2 && operators[index].Item3 == "left")))
                            {

                                output.Push(Convert.ToString(operatorStack.Peek()));
                                operatorStack.Pop();
                            }
                            operatorStack.Push(s1[j]);
                        }
                        negative = true;
                    }

                    else if (negative || !opers.Contains(s1[j]))      //          numbers
                    {
                        s += s1[j];
                        negative = false;
                        if (j != s1.Length - 1 && !opers.Contains(s1[j + 1]))
                        {
                            j++;
                            while (j < s1.Length && !opers.Contains(s1[j]))
                            {
                                s += s1[j];
                                j++;
                            }
                            j--;
                        }
                        output.Push(s);
                        s = "";
                    }
                    //Console.WriteLine(fullStr[i]);
                }
                while (operatorStack.Count > 0)
                {
                    output.Push(Convert.ToString(operatorStack.Pop()));
                }
                List<string> state = new List<string>();
                while (output.Count > 0)
                {
                    state.Add(output.Pop());
                   // Console.Write(output.Pop() + " ");
                }
                //Console.WriteLine();
                if (ifState)
                {
                    OperationNode op = new OperationNode().BuildSubTree(state.ToArray());
                    ifNode.condition = op;
                }
                else if (statement)
                {
                    if (equation)
                    {
                        EquationNode eq = new EquationNode(new VariableNode(state[2]), new ValueNode(Convert.ToDouble(state[1])));
                        ifExpr.AddNode(eq);
                    }
                    else
                    {
                        OperationNode op = new OperationNode().BuildSubTree(state.ToArray());
                        ifExpr.AddNode(op);
                    }
                }
                else if (equation)
                {
                    EquationNode eq = new EquationNode(new VariableNode(state[2]), new ValueNode(Convert.ToDouble(state[1])));
                    expression.AddNode(eq);
                }
                else if (state != null)
                {
                    OperationNode op = new OperationNode("").BuildSubTree(state.ToArray());
                    expression.AddNode(op);
                }

            }

            Dictionary<string, double> table = new Dictionary<string, double>();
            expression.CalculateTree(table);
            
           /* for (int i = 0; i < expression.child.Count; i++)
            {
                string type = expression.child[i].Type;
                switch(type)
                {
                    case "Equation":
                        {
                            EquationNode t = (EquationNode)expression.child[i];
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
                            OperationNode t = (OperationNode)expression.child[i];
                            t.WriteSubTree(t);
                            Console.WriteLine();
                            break;
                        }
                }
            }*/

            /*foreach (var entry in table)
                Console.WriteLine("[{0} {1}]", entry.Key, entry.Value);*/

            /*for (int i = 0; i < fullStr[j].Length; i++)
            {
                if (fullStr[j][i] == '(')  //   (
                {
                    operatorStack.Push(fullStr[j][i]);
                    negative = true;
                }

                else if (fullStr[j][i] == ')')   //      )
                {
                    while (operatorStack.Peek() != '(')
                    {
                        CalcExpression(output, operatorStack.Peek());
                        operatorStack.Pop();
                    }
                    operatorStack.Pop();
                    negative = false;
                }

                else if (!negative && opers.Contains(fullStr[j][i]))      //      +,-,*,/,^
                {
                    int index = Array.IndexOf(opers, fullStr[j][i]);
                    if (operatorStack.Count == 0 || operatorStack.Peek() == '(' || (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 < operators[index].Item2))
                    {
                        operatorStack.Push(fullStr[j][i]);
                    }
                    else
                    {
                        while (operatorStack.Count > 0 && operatorStack.Peek() != '(' && (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 > operators[index].Item2 || (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 == operators[index].Item2 && operators[index].Item3 == "left")))
                        {

                            CalcExpression(output, operatorStack.Peek());
                            operatorStack.Pop();
                        }
                        operatorStack.Push(fullStr[j][i]);
                    }
                    negative = true;
                }

                else if (negative || !opers.Contains(fullStr[j][i]))      //          numbers
                {
                    s += fullStr[j][i];
                    negative = false;
                    if (i != fullStr[j].Length - 1 && !opers.Contains(fullStr[j][i + 1]))
                    {
                        i++;
                        while (i < fullStr[j].Length && !opers.Contains(fullStr[j][i]))
                        {
                            s += fullStr[j][i];
                            i++;
                        }
                        i--;
                    }
                    output.Push(s);
                    s = "";
                }


            }*/

            /*string s = "";
            Stack<string> output = new Stack<string>();
            Stack<char> operatorStack = new Stack<char>();
            string input = "";

            foreach (string arg in args)
            {
                input += arg;
            }
            int length = input.Length;
            bool negative = true;

            for (int i = 0; i < length; i++)
            {
                if (input[i] == '(')  //   (
                {
                    operatorStack.Push(input[i]);
                    negative = true;
                }

                else if (input[i] == ')')   //      )
                {
                    while (operatorStack.Peek() != '(')
                    {
                        CalcExpression(output, operatorStack.Peek());
                        operatorStack.Pop();
                    }
                    operatorStack.Pop();
                    negative = false;
                }

                else if (!negative && opers.Contains(input[i]))      //      +,-,*,/,^
                {
                    int index = Array.IndexOf(opers, input[i]);
                    if (operatorStack.Count == 0 || operatorStack.Peek() == '(' || (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 < operators[index].Item2))
                    {
                        operatorStack.Push(input[i]);
                    }
                    else
                    {
                        while (operatorStack.Count > 0 && operatorStack.Peek() != '(' && (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 > operators[index].Item2 || (operators[Array.IndexOf(opers, operatorStack.Peek())].Item2 == operators[index].Item2 && operators[index].Item3 == "left")))
                        {

                            CalcExpression(output, operatorStack.Peek());
                            operatorStack.Pop();
                        }
                        operatorStack.Push(input[i]);
                    }
                    negative = true;
                }

                else if (negative || nums.Contains(input[i]))      //          numbers
                {
                    s += input[i];
                    negative = false;
                    if (i != length - 1 && nums.Contains(input[i + 1]))
                    {
                        i++;
                        while (i < length && nums.Contains(input[i]))
                        {
                            s += input[i];
                            i++;
                        }
                        i--;
                    }
                    output.Push(s);
                    s = "";
                }


            }
            while (operatorStack.Count > 0)
            {
                CalcExpression(output, operatorStack.Peek());
                operatorStack.Pop();
            }
            *//*PrintValues(output);
            Console.Write("  ||  ");
            PrintValues(operatorStack);
            Console.WriteLine();*//*
            Console.WriteLine("Result: {0}", output.Peek());*/
            Console.ReadKey();
        }

        

        public static void PrintValues(Stack<string> stack, string[] p)
        {
            int count = stack.Count;
            for (int i = 0; i < count; i++)
            {
                // Console.WriteLine(stack.Count);
                p[i] = stack.Pop();
            }
        }
    }
}
