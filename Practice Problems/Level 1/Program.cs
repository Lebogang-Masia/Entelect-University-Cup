using System;
using System.Collections.Generic;
using System.IO;

internal abstract class Solution {

    private static int Values = 5;
    private static int[] Parent = new int[Values];
    private static int INF = int.MaxValue;

    private static int Find(int i) {
        while (Parent[i] != i)
            i = Parent[i];
        return i;
    }

    private static void Union(int i, int j) {
        int a = Find(i);
        int b = Find(j);
        Parent[a] = b;
    }

    private static List<string> MST(int [,]cost) {
        List<string> result = new List<string>();
        int mincost = 0;

        for (int i = 0; i < Values; i++)
            Parent[i] = i;

        int edge_count = 0;
        while (edge_count < Values - 1) {
            int min = INF, a = -1, b = -1;
            for (int i = 0; i < Values; i++) {
                for (int j = 0; j < Values; j++) {
                    if (Find(i) != Find(j) && cost[i, j] < min) {
                        min = cost[i, j];
                        a = i;
                        b = j;
                    }
                }
            }

            Union(a, b);
            edge_count++;
            result.Add(Diction(b));
            result.Add(Diction(a));
            mincost += min;
        }

        return result;
    }

    private static string Diction(int index) {
        Dictionary<int, string> dictionary = new Dictionary<int, string>();
        dictionary.Add(0, "Carrots");
        dictionary.Add(1, "Tomatoes");
        dictionary.Add(2, "Spinach");
        dictionary.Add(3, "Corn");
        dictionary.Add(4, "Yams");

        return dictionary[index];
    } 

    public static void Main(String[] args) {

        int [,]cost = {
            { INF, 4, 3, INF, INF },
            { 4, INF, INF, 5, INF },
            { 3, INF, INF, 2, 3 },
            { INF, 5, 2, INF, 1 },
            { INF, INF, 3, 1, INF },
        };

        List<string> result = MST(cost);
        List<string> final = new List<string>();

        foreach (string element in result){
            if (final.Contains(element)) continue;
            else final.Add(element);
        }

        // Writing to file "Solution.txt"
        using (StreamWriter writer = new StreamWriter("Solution.txt")) {
            writer.Write("[");
            for (int i = 0; i < final.Count; i++) {
                writer.Write("\"" + final[i] + "\"" + (i < final.Count - 1 ? "," : ""));
            }
            writer.Write("]");
        }

        Console.WriteLine("Output written to Solution.txt.");
    }
}
