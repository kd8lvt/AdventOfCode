using _2024;
using System.Collections;
using System.Diagnostics;
using System.Linq;

public static class Util
{
    public static string[] ReadFile(string path)
    {
        int ms = 0;
        string[] lines = Timers.TimeExecution("Read File", File.ReadAllLines, path, out ms);
        Logger.Debug("Total lines read: {0}", lines.Length);
        return lines;
    }

    public static T[] Without<T>(T[] arr,int idx)
    {
        ArrayList ret = [.. arr];
        ret.RemoveAt(idx);
        return [..ret.Cast<T>()];
    }

    public static string StringifyArr<T>(T[] arr)
    {
        return string.Format("[{0}]",string.Join(",",arr));
    }
}

public static class Logger
{
    private static ConsoleColor oldTextColor = Console.ForegroundColor;
    private static void SetTextColor(ConsoleColor color)
    {
        oldTextColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
    }
    private static void ResetTextColor()
    {
        Console.ForegroundColor = oldTextColor;
    }
    public static void Log(string level, string format, params object[] args)
    {
        Console.WriteLine("[" + level + "] " + format, args);
    }
    public static void Info(string format, params object[] formatArgs)
    {
        SetTextColor(ConsoleColor.White);
        Log("INFO", format, formatArgs);
        ResetTextColor();
    }

    public static void Debug(string format, params object[] formatArgs)
    {
        SetTextColor(ConsoleColor.DarkGray);
        if (Aoc2024.args.debug) Log("DEBUG", format, formatArgs);
        ResetTextColor();
    }

    public static void Result(string format, params object[] formatArgs)
    {
        SetTextColor(ConsoleColor.Green);
        Log("RESULT",format,formatArgs);
        ResetTextColor();
    }
}

public static class Timers
{
    private static ArrayList timers = new ArrayList();
    public static int startTimer()
    {
        timers.Add(Stopwatch.StartNew());
        return timers.Count - 1;
    }
    public static int stopTimer(int timerId)
    {
        if (timers[timerId] is not Stopwatch timer) return 0;
        timers.Remove(timerId);
        timer.Stop();
        return timer.Elapsed.Milliseconds;
    }

    public static R TimeExecution<R, A>(string label, Func<A, R> cb, A arg, out int ms)
    {
        int timer = startTimer();
        R retVal = cb.Invoke(arg);
        ms = stopTimer(timer);
        return retVal;
    }
    static R TimeExecution<R, A, B>(string label, Func<A, B, R> cb, A arg, B arg2, out int ms)
    {
        int timer = startTimer();
        R retVal = cb.Invoke(arg, arg2);
        ms = stopTimer(timer);
        return retVal;
    }

    public static R TimeExecution<R, A>(string label, Func<A, R> cb, A arg)
    {
        R retVal = TimeExecution(label, cb, arg, out int ms);
        Logger.Info("Executed '{0}' in {1}ms", label, ms);
        return retVal;
    }
    static R TimeExecution<R, A, B>(string label, Func<A, B, R> cb, A arg, B arg2)
    {
        R retVal = TimeExecution(label, cb, arg, arg2, out int ms);
        Logger.Info("Executed '{0}' in {1}ms", label, ms);
        return retVal;
    }
}
