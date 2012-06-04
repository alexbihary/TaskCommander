using System;

namespace TaskCommander
{
    public class Console : TaskCommander.IConsole
    {
        public Console()
        {
            System.Console.Title = "TaskCommander";
        }

        public void WriteLine(string text)
        {
            System.Console.WriteLine(text);
        }

        public void Write(string text)
        {
            System.Console.Write(text);
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public void Clear()
        {
            System.Console.Clear();
        }
    }
}
