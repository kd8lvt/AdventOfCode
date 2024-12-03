namespace _2024
{
    public class Program
    {
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
            switch (args.day)
            {
                case 1:
                    Timers.TimeExecution("Everything", Day1.Run, args);
                    break;
                case 2:
                    Timers.TimeExecution("Everything", Day2.Run, args);
                    break;
            }
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