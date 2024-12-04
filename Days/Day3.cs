using _2024;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.Serialization.Json;
using System.Runtime.Versioning;
using System.Transactions;

public static class Day3
{
    static Program.RunArgs args = new Program.RunArgs(3,false);
    public record MulInstruction
    {
        public int val1 = 0;
        public int val2 = 0;
        public string instruction = "";
        public int end = 0;
        public bool valid = true;
        public MulInstruction(int val1, int val2, int end, string instruction)
        {
            this.val1 = val1;
            this.val2 = val2;
            this.instruction = instruction;
            this.end = end;
            valid = true;
        }
        public MulInstruction()
        {
            this.valid = false;
        }
    }

    public static bool Run(Program.RunArgs _args)
    {
        args = _args;
        int ms = 0;
        string[] lines = Timers.TimeExecution("Read File", File.ReadAllLines, args.file);
        int total = Timers.TimeExecution("Process Instructions", FindAllInstructions, lines);
        Logger.Info("Took {0}ms to locate and process instructions in data", ms, lines.Length);
        Logger.Result("Total: {0}",total);
        return true;
    }

    public static int ProcessInstructions(MulInstruction[] instructions)
    {
        int ret = 0;
        foreach (MulInstruction instruction in instructions) ret += instruction.val1 * instruction.val2;
        return ret;
    }

    public static int FindAllInstructions(string[] lines) {
        int total = 0;
        int count = 0;
        foreach (string line in lines)
        {
            string newline = line;
            int[]? instruction = FindNextInstructon(line);
            while (instruction != null)
            {
                count++;
                total += instruction[0];
                newline = newline[instruction[1]..];
                instruction = FindNextInstructon(newline);
            }
        }
        return total;
    }

    public static int[]? FindNextInstructon(string line)
    {
        Dictionary<char,char> dict = new Dictionary<char, char>
        {
          //{ 'm', ' ' }; //for readability's sake
            { 'u', 'm' },
            { 'l', 'u' },
            { '(', 'l' },
            { ',', '(' },
            { ')', ',' }
        };

        int offset = -1;
        int end = -1;
        int start = -1;
        char? last = null;
        string val1 = "";
        string val2 = "";
        foreach (char ch in line)
        {
            offset++;
            dict.TryGetValue(ch, out char val);
            if (ch == 'm' && last == null)
            {
                last = 'm';
                start = offset;
            }
            else if (ch == ')')
            {
                end = offset;
                if (val1 != "" && val2 != "") return [int.Parse(val1) * int.Parse(val2), end];
                else continue;
            }
            else if (char.IsDigit(ch) && (last == '(' || last == ','))
            {
                if (last == '(') val1 += ch;
                else if (last == ',') val2 += ch;
            }
            else if (dict.ContainsKey(ch) && val == last) last = ch;
            else
            {
                val1 = val2 = "";
                last = null;
            }
        }
        return null;
    }


}