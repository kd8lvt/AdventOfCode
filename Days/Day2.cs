using _2024;
using System.Collections;
using System.Runtime.CompilerServices;

public static class Day2
{
    static Program.RunArgs args = new Program.RunArgs(2,false);
    public static bool Run(Program.RunArgs _args)
    {
        args = _args;
        int ms = 0;
        string[] lines = Timers.TimeExecution("Read File", File.ReadAllLines, args.file,out ms);
        int[][] reports = Timers.TimeExecution("Parse Report Data", ParseReports, lines, out ms);
        int[] safeReports = Timers.TimeExecution("Validate Reports", NumSafe, reports, out ms);
        Logger.Result("[Part 1] Safe reports: {0}", safeReports[0]);
        Logger.Result("[Part 2] Safe reports: {0}", safeReports[0]+safeReports[1]);
        return true;
    }

    static int[][] ParseReports(string[] lines)
    {
        int[][] reports = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            ArrayList reportValues = new ArrayList();
            string val = "";
            foreach (char ch in line)
            {
                if (char.IsDigit(ch)) val += ch;
                else
                {
                    reportValues.Add(int.Parse(val));
                    val = "";
                }
            }
            if (val != "") reportValues.Add(int.Parse(val));
            reports[i] = [.. reportValues.Cast<int>()];
        }
        return reports;
    }

    static bool IsSafe(int[] report, bool shouldDampen = true)
    {
        bool safe = true;
        bool increasing = report[0] < report[1];
        for (int i = 1; i < report.Length; i++)
        {
            int delta = report[i] - report[i - 1]; //Delta = difference between the current value and the previous

            safe &= (delta != 0); //There was no change from previous value
            if (!safe) break;
            safe &= (int.IsPositive(Math.Sign(delta)) == increasing); //Delta is in the correct direction (+ when increasing, - when not)
            if (!safe) break;
            safe &= (Math.Abs(delta) < 4); //Absolute value of Delta is >= 4
            if (!safe) break;

            //If all of the above is true, this is a safe pair.
        }
        if (shouldDampen && !safe) safe |= ProblemDamping(report);
        return safe;
    }

    static bool ProblemDamping(int[] report)
    {
        for (int i=0; i<report.Length; i++)
        {
            if (IsSafe(Util.Without(report, i), false)) return true;
        }
        return false;
    }

    static int[] NumSafe(int[][] reports)
    {
        int pt1Count = 0;
        int unsafeCount = 0;
        int pt2Count = 0;
        int[][] unsafes = new int[reports.Length][];
        for (int i = 0; i < reports.Length; i++)
        {
            bool safe = IsSafe(reports[i],false);
            if (!safe) 
            {
                unsafes[unsafeCount] = reports[i];
                unsafeCount++;
            }
            else pt1Count++;
        }
        for (int i = 0; i < unsafeCount; i++) pt2Count += IsSafe(reports[i], true)?1:0;
        return [pt1Count,pt2Count];
    }
}