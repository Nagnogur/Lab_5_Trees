using System;
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
            char[] opers = { '+', '-', '*', '/', '^' };
            operators.Add(Tuple.Create('+', 2, "left"));
            operators.Add(Tuple.Create('-', 2, "left"));
            operators.Add(Tuple.Create('*', 3, "left"));
            operators.Add(Tuple.Create('/', 3, "left"));
            operators.Add(Tuple.Create('^', 4, "right"));

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
                    switch(inp[i][j])
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

            Stack<string> output = new Stack<string>();
            Stack<char> operatorStack = new Stack<char>();
            bool negative = true;
            bool ifState = false;

            int len = fullStr.Count;
            for (int i = 0; i < fullStr.Count; i++)
            {   
                if (fullStr[i][0] == '}')
                {
                    ifState = false;
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


                Console.WriteLine(fullStr[i]);
            }

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

        public static void CalcExpression(Stack<string> stack1, char action)
        {
            int u = Convert.ToInt32(stack1.Peek());
            stack1.Pop();
            int v = Convert.ToInt32(stack1.Peek());
            stack1.Pop();
            int ans = 0;

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
