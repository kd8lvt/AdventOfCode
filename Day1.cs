using _2024;
using System.Collections;
public static class Day1
{
    static Aoc2024.RunArgs args = new Aoc2024.RunArgs(1, false);
    public static bool Run(Aoc2024.RunArgs _args)
    {
        args = _args;
        bool debug = Aoc2024.args.debug;
        string[] lines = Util.ReadFile(Aoc2024.args.file);

        int[][] pairs = Timers.TimeExecution("Parse Value Pairs", ParsePairs, lines);
        Logger.Debug("Total Pairs: {0}", pairs[0].Length);

        Array distances = Timers.TimeExecution("Get Distances", GetDistances, pairs);

        int total = 0;
        foreach (int distance in distances)
        {
            total += distance;
        }

        Logger.Result("Total distance: {0}", total);
        return true;
    }

    static int[][] ParsePairs(string[] input)
    {
        int[] firstSet = new int[input.Length];
        int[] secondSet = new int[input.Length];

        bool readingKey = true;
        string key = "";
        string value = "";
        for (int i = 0; i < input.Length; i++)
        {
            string str = input[i];
            foreach (char ch in str.ToCharArray())
            {
                if (string.IsNullOrWhiteSpace(ch.ToString()))
                {
                    readingKey = false;
                    continue;
                }
                if (readingKey) key += ch;
                else value += ch;
            }
            firstSet[i] = int.Parse(key);
            secondSet[i] = int.Parse(value);
            readingKey = true;
            key = "";
            value = "";
        }
        return [firstSet, secondSet];
    }

    static int[] GetDistances(int[][] pairs)
    {
        if (pairs == null || pairs[0] == null || pairs[1] == null) return Array.Empty<int>();
        int[] ret = new int[pairs[0].Length];
        int count = pairs[0].Length;
        ArrayList keys = [.. pairs[0]];
        ArrayList values = [.. pairs[1]];
        int i = 0;
        try
        {
            for (i = 0; i < count; i++)
            {
                //Welcome to unboxing hell, would you like a cookie?
                int lowKey = keys.Count - 1;
                int lowValue = values.Count - 1;
                for (int j = 0; j < keys.Count; j++)
                {
                    if ((int)keys[j] < (int)keys[lowKey]) lowKey = j;
                    if ((int)values[j] < (int)values[lowValue]) lowValue = j;
                }
                if ((int)keys[lowKey] > (int)values[lowValue]) ret[i] = ((int)keys[lowKey] - (int)values[lowValue]);
                else ret[i] = ((int)values[lowValue] - (int)keys[lowKey]);
                keys.RemoveAt(lowKey);
                values.RemoveAt(lowValue);
            }
        }
        catch (Exception)
        {
            Console.WriteLine(i);
            throw;
        }
        return ret;
    }
}
