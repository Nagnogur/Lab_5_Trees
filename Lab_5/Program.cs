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
        static void Main(string[] args)
        {
            string[] inp = File.ReadAllLines(@"input.txt");
                List<Tuple<char, int, string>> operators = new List<Tuple<char, int, string>>();
                char[] opers = { '+', '-', '*', '/' };
                //char[] nums = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
                operators.Add(Tuple.Create('+', 2, "left"));
                operators.Add(Tuple.Create('-', 2, "left"));
                operators.Add(Tuple.Create('*', 3, "left"));
                operators.Add(Tuple.Create('/', 3, "left"));

                string s = "";
                Stack<string> output = new Stack<string>();
                Stack<char> operatorStack = new Stack<char>();

                string input = Console.ReadLine().Replace(" ", "");
                int length = input.Length;

                for (int i = 0; i < length; i++)
                {
                    if (input[i] == '(')  //   (
                    {
                        operatorStack.Push(input[i]);
                    }

                    else if (input[i] == ')')   //      )
                    {
                        while (operatorStack.Peek() != '(')
                        {
                            output.Push(Convert.ToString(operatorStack.Peek()));
                            operatorStack.Pop();
                        }
                        operatorStack.Pop();
                    }

                    else if (opers.Contains(input[i]))      //      +,-,*,/,^
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
                                output.Push(Convert.ToString(operatorStack.Peek()));
                                operatorStack.Pop();
                            }
                            operatorStack.Push(input[i]);
                        }
                    }

                    else      //          numbers
                    {

                        s += input[i];
                        while (i < length - 1)
                        {
                            if (opers.Contains(input[i + 1]))
                            {
                                break;
                            }
                            else
                            {
                                i++;
                                s += input[i];
                            }

                        }

                        output.Push(s);

                        s = "";
                    }
                    //  Console.WriteLine(output.Peek());
                }
                while (operatorStack.Count > 0)
                {
                    output.Push(Convert.ToString(operatorStack.Peek()));
                    operatorStack.Pop();
                }

                string[] inf = new string[output.Count];
                PrintValues(output, inf);
                /*char[] rev = inf.ToCharArray();
                Console.WriteLine(inf);
                char[] st = inf.ToCharArray();*/
                
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
