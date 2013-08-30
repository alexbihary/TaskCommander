using System;
namespace TaskCommander
{
    public interface IConsole
    {
        string ReadLine();
        void Write(string text);
        void Write(string text, ConsoleColor color);
        void WriteLine(string text = "");
        void WriteLine(string text, ConsoleColor color);
        void Error(string text);
        void ErrorLine(string text);
        void Warning(string text);
        void WarningLine(string text);
        void Success(string text);
        void SuccessLine(string text);
        void Clear();
        string Prompt(string text);
        string ValidatePrompt(string text, Func<string, bool> validator, string validationMessage);

        Settings Settings { get; }
    }
}
