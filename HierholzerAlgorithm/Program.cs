using HierholzerAlgorithm;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();

        while (true)
        {
            List<string> list = Console.ReadLine().Split(" ").ToList();
            if (list.Count < 2) break;
            if (edges.ContainsKey(list[0]))
            {
                edges[list[0]].Add(list[1]);
            }
            else
            {
                edges.Add(list[0], new List<string> { list[1] });
            }
            if (edges.ContainsKey(list[1])) edges[list[1]].Add(list[0]);
            else edges.Add(list[1], new List<string>() { list[0] });
        }

        int size = edges.Count;
        int[,] n = new int[size, size];

        for (int y = 0; y < size; y++)
        {
            var elem = edges.ElementAt(y);
            for (int i = 0; i < elem.Value.Count; i++)
            {
                var ind = edges.IndexOf(elem.Value[i]);
                n[y, ind]++;
            }
        }
        PrintMatrix(n);

        if (!IsGraphEulerian(n))
        {
            Console.WriteLine("Graph is not Eulerian");
            return;
        }

        List<List<string>> cycles = new List<List<string>>();
        int cIndex = 0;
        var cVertex = edges.ElementAt(cIndex);
        while (IsGraphPartConnected(n))
        {
            List<string> cycle = new List<string>();
            cycle.Add(edges.ElementAt(cIndex).Key);

            Console.WriteLine();
            while (true)
            {
                for (int i = 0; ; i++)
                {
                    if (n[cIndex, i] > 0)
                    {
                        cycle.Add(edges.ElementAt(i).Key);
                        n[cIndex, i]--;
                        n[i, cIndex]--;
                        cIndex = i;
                        break;
                    }
                }
                if (cVertex.Key == edges.ElementAt(cIndex).Key)
                {
                    cycles.Add(cycle);
                    break;
                }
            }

            for (int i = 0; i < cycles.Count; i++)
            {
                for (int j = 0; j < cycles[i].Count; j++)
                {
                    var ind = edges.IndexOf(cycles[i][j]);
                    for (int t = 0; t < n.GetLength(0); t++)
                    {
                        if (n[ind, t] > 0)
                        {
                            cIndex = ind;
                            cVertex = edges.ElementAt(cIndex);
                        }
                    }
                }
            }
        }

        PrintAllCycles(cycles);

        while (cycles.Count > 1)
        {
            for (int t = 0; t < cycles.Count; t++)
            {
                string kElem = cycles[t][0];
                bool flag = false;
                for (int i = 0; i < cycles.Count; i++)
                {
                    if (i == t) continue;
                    if (cycles[i].Contains(kElem))
                    {
                        int ind = cycles[i].IndexOf(kElem);
                        cycles[i].Remove(kElem);
                        cycles[i].InsertRange(ind, cycles[t]);
                        cycles.RemoveAt(t);
                        flag = true;
                        break;
                    }
                }
                if(flag)break;
            }

        }
        PrintCycle(cycles[0]);
        Console.WriteLine();
    }
    private static void PrintAllCycles(List<List<string>> cycles)
    {
        Console.WriteLine("-----------------");
        foreach (var cycle in cycles)
        {
            PrintCycle(cycle);
        }
        Console.WriteLine("-----------------");
    }
    private static void PrintCycle(List<string> cycle)
    {
        for(int i = 0; i < cycle.Count - 1; i++)
        {
            Console.Write(cycle[i] + "->");
        }
        Console.WriteLine(cycle[cycle.Count - 1]);
    }
    private static void PrintMatrix(int[,] matrix)
    {
        for(int y = 0;y<matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                Console.Write(matrix[y, x] + " ");
            }
            Console.WriteLine();
        }
    } 
    private static bool IsGraphEulerian(int[,] matrix)
    {
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            int sum = 0;
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                sum += matrix[y, x];
            }
            if (sum % 2 != 0) return false;
        }
        return true;
    }
    private static bool IsGraphConnected(int[,] matrix)
    {
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            bool flag = false;
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (matrix[y, x] != 0)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag) return false;
        }
        return true;
    }

    private static bool IsGraphPartConnected(int[,] matrix)
    {
        bool flag = false;
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                if (matrix[y, x] != 0)
                {
                    flag = true;
                    break;
                }
            }
            if (flag) break;
        }
        if (flag) return true;
        return false;
    }
}