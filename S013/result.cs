using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        var vertices = new List<Vertex>();
        var count = Console.ReadLine();
        var costsLine = Console.ReadLine();
        var safeMapLine = Console.ReadLine();
        var safeMap = new Dictionary<int, bool>();
        var costs = costsLine.Split(' ').Select(x => int.Parse(x)).ToList();
        for (int i = 0; i < int.Parse(count); i++)
        {
            var vertex = new Vertex(i + 1);
            vertices.Add(vertex);

            foreach (var item in vertices.Where(x => x.Number != vertex.Number))
            {
                var edge1 = new Edge(vertex, item, (vertex.Number > item.Number) ? costs[1] : costs[0]);
                var edge2 = new Edge(item, vertex, (vertex.Number > item.Number) ? costs[0] : costs[1]);
                vertex.Edges.Add(edge1);
                item.Edges.Add(edge2);
            }
        }
        for (int i = 0; i < safeMapLine.Length; i++)
        {
            safeMap.Add(i + 1, safeMapLine[i] == 's');
        }
        new Cycle(vertices.Last(),safeMap, vertices.Count);
        var result = string.Join(" ", Cycle.AllCycles.OrderBy(x => x.Cost).First().Vertices.Select(x => x.Number));

        System.Console.WriteLine(result);
    }
}

public class Vertex
{
    public int Number { get; set; }
    public HashSet<Edge> Edges { get; set; }


    public Vertex(int number)
    {
        Edges = new HashSet<Edge>();
        Number = number;
    }
}

public class Edge
{
    public int Cost { get; }
    public Vertex Source { get; }
    public Vertex Target { get; }

    public Edge(Vertex source, Vertex target, int cost)
    {
        Source = source;
        Target = target;
        Cost = cost;
    }
}

public class Cycle
{
    public static List<Cycle> AllCycles = new List<Cycle>();
    public static Vertex FirstVertex;
    public static int MinCost = 0;
    public static int CountVertex;

    private Cycle(Vertex[] edges, int[] goedVertices, int cost, Dictionary<int, bool> safeMap)
    {
        Vertices = new List<Vertex>(edges);
        GoedVertices = new HashSet<int>(goedVertices);
        Cost = cost;
        SafeMap = safeMap;
    }
    public Cycle(Vertex firstVertex, Dictionary<int, bool> safeMap, int count)
    {
        Vertices = new List<Vertex> { firstVertex};
        GoedVertices = new HashSet<int> { firstVertex.Number };
        FirstVertex = firstVertex;
        CountVertex = count;

        foreach (var item in firstVertex.Edges.Where(x => safeMap[x.Target.Number])
                .OrderBy(x => GoedVertices.Contains(x.Target.Number)).ThenByDescending(x => x.Target.Number))
        {
            MinCost = 0;
            var map = new Dictionary<int, bool>();
            for (int i = 1; i <= safeMap.Count; i++)
            {
                map.Add(i, i > item.Target.Number ? !safeMap[i] : safeMap[i]);
            }
            SafeMap = map;
            AddVertex(item);  
        }
    }

    public void AddVertex(Edge edge)
    {
        if (edge.Target.Number == FirstVertex.Number && GoedVertices.Count == CountVertex)
        {
            var cost = Cost + edge.Cost;
            if(MinCost == 0  || cost <= MinCost)
            {
                MinCost = cost;
                var cycle = new Cycle(Vertices.ToArray(), GoedVertices.ToArray(), cost, SafeMap);
                cycle.Vertices.Add(edge.Target);
                AllCycles.Add(cycle);
            }
        }
        else if ((MinCost != 0 && Cost > MinCost) || Vertices.Count > (CountVertex * CountVertex))
        {
            return;
        }
        else
        {
            var edges = edge.Target.Edges.Where(x => SafeMap[x.Target.Number])
                .OrderBy(x => GoedVertices.Contains(x.Target.Number)).ThenByDescending(x => x.Target.Number);
            foreach (var item in edges)
            {
                var map = new Dictionary<int, bool>();
                for (int i = 1; i <= SafeMap.Count; i++)
                {
                    map.Add(i, i > item.Target.Number ? !SafeMap[i] : SafeMap[i]);
                }

                var cycle = new Cycle(Vertices.ToArray(), GoedVertices.ToArray() , Cost, map);
                cycle.Vertices.Add(edge.Target);
                cycle.GoedVertices.Add(edge.Target.Number);
                cycle.Cost += edge.Cost;
                cycle.AddVertex(item);
            }
        }
    }

    public List<Vertex> Vertices { get; }
    public HashSet<int> GoedVertices { get; }
    public Dictionary<int,bool> SafeMap{ get; set; }

    public int Cost { get; set; }

}
