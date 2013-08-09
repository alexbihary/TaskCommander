using System;
namespace TaskCommander
{
    public interface IConsole
    {
        string ReadLine();
        void Write(string text = "", ConsoleColor color = ConsoleColor.White);
        void WriteLine(string text = "", ConsoleColor color = ConsoleColor.White);
        void Clear();
    }
}
