using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 自分の得意な言語で
        // Let's チャレンジ！！
        var size = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        var lands = new List<Land>();
        var islands = new List<Island>();

        for (int x = 0; x < size[0]; x++)
        {
            var values = Console.ReadLine().Select(t => t == '#').ToArray();
            for (int y = 0; y < values.Length; y++)
            {
                if (values[y])
                {
                    var land = new Land(x, y);
                    lands.Add(land);

                    if(x > 0 )
                    {
                        var help = lands.FirstOrDefault(l => l.X == x - 1 && l.Y == y);
                        if(help != null)
                        {
                            help.Neighbords.Add(land);
                            land.Neighbords.Add(help);
                        }
                    }

                    if (y > 0)
                    {
                        var help = lands.FirstOrDefault(l => l.X == x && l.Y == y - 1);
                        if (help != null)
                        {
                            help.Neighbords.Add(land);
                            land.Neighbords.Add(help);
                        }
                    }
                }
            }
        }

        while (lands.Any(x => x.Island == null))
        {
            var land = lands.First(x => x.Island == null);
            var island = new Island();
            islands.Add(island);

            land.SetIsland(island);

            island.Calculate();
        }

        foreach (var item in islands.OrderByDescending(x => x.Size).ThenByDescending(x => x.Shoreline))
        {
            Console.WriteLine($"{item.Size} {item.Shoreline}");
        }

    }
}

class Land
{
    public int X { get; set; }
    public int Y { get; set; }

    public Land(int x, int y)
    {
        X = x;
        Y = y;
        Neighbords = new List<Land>();
    }

    public List<Land> Neighbords{ get; set; }
    public Island Island { get; set; }

    public void SetIsland(Island island)
    {
        Island = island;
        island.Lands.Add(this);
        foreach (var neighbord in Neighbords.Where(x => x.Island == null))
        {
            neighbord.SetIsland(island);
        }
    }

    public int GetShoreline()
    {
        return 4 - Neighbords.Count;
    }
}

class Island
{
    public Island()
    {
        Lands = new List<Land>();
    }

    public int Size { get; set; }
    public int Shoreline { get; set; }

    public List<Land> Lands { get; }

    internal void Calculate()
    {
        Size = Lands.Count;
        Shoreline = Lands.Sum(x => x.GetShoreline());
    }
}
