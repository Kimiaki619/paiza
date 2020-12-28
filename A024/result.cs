using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        var data = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

        MyList.Calculate(data[1], data[0] * 2);
        Console.WriteLine(MyList.Result.Count);
    }
}

public struct Level
{
    public int A;
    public int B;

    public int Distance => (A == 0 || B == 0) ? 1 : Math.Abs(A - B);
}

public class MyList
{
    public static List<MyList> Result = new List<MyList>();
    static int MaxDistance;
    static int Count;
    public List<Level> Levels{ get; set; }
    public static void Calculate(int maxDistance, int count)
    {
        Count = count;
        MaxDistance = maxDistance;

        var list1 = new MyList(new Level[0]);
        list1.AddToA(1);
        var list2 = new MyList(new Level[0]);
        list2.AddToB(1);
    }

    MyList(Level[] levels)
    {
        Levels = new List<Level>(levels);
    }

    void AddToA(int value)
    {
        var id = Levels.FindIndex(x => x.A == 0);
        if(id == -1)
        {
            if(Levels.Count >= Count / 2)
            {
                return;
            }
            Levels.Add(new Level());
            id = Levels.Count - 1;
        }
        var level = Levels[id];
        level.A = value;
        Levels[id] = level;
        nextLevel(value);
    }

    void AddToB(int value)
    {
        var id = Levels.FindIndex(x => x.B == 0);
        if (id == -1)
        {
            if (Levels.Count >= Count / 2)
            {
                return;
            }
            Levels.Add(new Level());
            id = Levels.Count - 1;
        }
        var level = Levels[id];
        level.B = value;
        Levels[id] = level;
        nextLevel(value);
    }

    void nextLevel(int value)
    {
        if(value == Count)
        {
            if( Levels.Sum(x => x.Distance) <= MaxDistance && Levels.All(x => x.A > 0 && x.B > 0))
            {
                Result.Add(this);
            }
            return;
        }
        if(value < Count && Levels.Sum(x => x.Distance) <= MaxDistance)
        {

            var list1 = new MyList(Levels.ToArray());
            var list2 = new MyList(Levels.ToArray());
            list1.AddToA(value + 1);
            list2.AddToB(value + 1);
        }
    }
}
