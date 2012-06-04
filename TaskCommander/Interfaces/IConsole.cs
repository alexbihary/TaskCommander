using System;
namespace TaskCommander
{
    public interface IConsole
    {
        string ReadLine();
        void Write(string text = "");
        void WriteLine(string text = "");
        void Clear();
    }
}
