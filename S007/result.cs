using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        var line = Console.ReadLine();
        string b = string.Empty;

        var stack = new List<int>();
        stack.Add(1);

        var isNumber = true;
        for (int i = 0; i < line.Length; i++)
        {
            var x = line[i];
            if (Char.IsDigit(x))
            {
                isNumber = true;
                b += x;
            }
            else
            {
                if (isNumber == true && b.Length > 0)
                {
                    var num = int.Parse(b);
                    stack[stack.Count - 1] = num;
                    b = string.Empty;
 
                }
                isNumber = false;
                if (x == '(')
                {
                    stack.Add(1);
                } else if (x == ')')
                {
                    stack.RemoveAt(stack.Count - 1);
                    stack[stack.Count - 1] = 1;
                }
                else
                {
                    Result[x] += Count(stack);
                    stack[stack.Count - 1] = 1;
                }

            }
        }

        foreach (var item in Result)
        {
            Console.WriteLine($"{item.Key} {item.Value}");
        }
    }

    static decimal Count(List<int> stack)
    {
        decimal result = 1;

        foreach (var item in stack)
        {
            result *= item;
        }
        return result;
    }

    static Dictionary<char, decimal> Result = new Dictionary<char, decimal>
    {
        {'a', 0},
        {'b', 0},
        {'c', 0},
        {'d', 0},
        {'e', 0},
        {'f', 0},
        {'g', 0},
        {'h', 0},
        {'i', 0},
        {'j', 0},
        {'k', 0},
        {'l', 0},
        {'m', 0},
        {'n', 0},
        {'o', 0},
        {'p', 0},
        {'q', 0},
        {'r', 0},
        {'s', 0},
        {'t', 0},
        {'u', 0},
        {'v', 0},
        {'w', 0},
        {'x', 0},
        {'y', 0},
        {'z', 0}
    };

}
