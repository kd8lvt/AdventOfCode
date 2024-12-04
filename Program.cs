namespace _2024
{
    public class Program
    {
        public static Day d1 = new Day(1, Day1.Run);
        public static Day d2 = new Day(2, Day2.Run);
        public static Day d3 = new Day(3, Day3.Run);
        public static RunArgs args = new RunArgs(0,false);
        static void Main(string[] _args)
        {
        DateTime now = DateTime.Now;
            Console.Clear();
            Logger.Info("Started {0} @ {1}", now.ToShortDateString(), now.ToShortTimeString());
            if (_args.Length < 1) throw new ArgumentOutOfRangeException("Please specify a day");
            int file = int.Parse(_args[0]);
            bool debug = false;
            if (_args.Length >= 2) bool.TryParse(_args[1], out debug);
            args = new RunArgs(file, debug);
            Days.Run(args);
        }

        public record RunArgs
        {
            public RunArgs(int day, bool debug) { this.day = day; this.debug = debug; file = day + ".txt"; }
            public int day;
            public string file;
            public bool debug;
        }
    }
}