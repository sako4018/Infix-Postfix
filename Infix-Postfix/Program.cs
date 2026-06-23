using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infix_Postfix
{
    internal class Program
    {
        //mitko ne znae kakvo e VS
         
        static void Main()
        {
            string notation = EnterExpression();
            Stack<char> stack = new Stack<char>();

            string postfix = ToPostfix(notation, stack);
            postfix = ToContinue(stack, postfix);

            DisplayExpression(postfix);
        }

        static string EnterExpression()
        {
            Console.Write("Expression: ");
            return Console.ReadLine();
        }

        static string ToPostfix(string notation, Stack<char> stack)
        {
            string result = "";

            for (int i = 0; i < notation.Length; i++)
            {
                char token = notation[i];

                if (char.IsLetterOrDigit(token))
                {
                    result += token;
                }
                else if (token == '(')
                {
                    stack.Push(token);
                }
                else if (token == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                        result += stack.Pop();

                    if (stack.Count > 0)
                        stack.Pop();
                }
                else
                {
                    int current = GetPriority(token);

                    while (stack.Count > 0 && GetPriority(stack.Peek()) >= current)
                    {
                        if (stack.Peek() == '(') break;
                        result += stack.Pop();
                    }

                    stack.Push(token);
                }
            }

            return result;
        }

        static string ToContinue(Stack<char> stack, string result)
        {
            while (stack.Count > 0)
            {
                result += stack.Pop();
            }

            return result;
        }

        static int GetPriority(char op)
        {
            if (op == '+' || op == '-') return 1;
            if (op == '*' || op == '/') return 2;
            if (op == '^') return 3;
            return 0;
        }

        static void DisplayExpression(string postfix)
        {
            Console.WriteLine("Postfix: " + postfix);
        }
    }
}
