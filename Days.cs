namespace _2024
{
    public static class Days
    {
        private static Dictionary<int, Day> days = new Dictionary<int, Day>();
        public static bool Run(Program.RunArgs args)
        {
            Logger.Debug("{0} days registered",days.Count);
            return days[args.day].Invoke(args);
        }

        internal static void Register(Day day)
        {
            days.Add(day.dayNum,day);
        }
    }

    public class Day
    {
        public int dayNum;
        Func<Program.RunArgs, bool> runnable;
        public Day(int _dayNum, Func<Program.RunArgs, bool> _runnable)
        {
            dayNum = _dayNum;
            runnable = _runnable;
            Days.Register(this);
        }
        public bool Invoke(Program.RunArgs args)
        {
            return runnable.Invoke(args);
        }
    }
}
