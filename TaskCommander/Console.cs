using System;

namespace TaskCommander
{
    public class Console : TaskCommander.IConsole
    {
        private ConsoleColor _color;
        public Console(ConsoleColor color = ConsoleColor.White)
        {
            System.Console.Title = "TaskCommander";
            _color = color;
        }

        public void WriteLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(text);
        }

        public void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            System.Console.ForegroundColor = color;
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
