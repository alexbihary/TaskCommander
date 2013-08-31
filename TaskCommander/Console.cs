using System;

namespace TaskCommander
{
    public class Console : TaskCommander.IConsole
    {
        public Console(Settings settings = null)
        {
            Settings = settings ?? new Settings();
            System.Console.Title = String.Format("{0} {1}", Settings.WindowTitle, Settings.WindowSubTitle);
        }

        public void WriteLine(string text = "") { WriteLine(text, Settings.DefaultColor); }
        public void WriteLine(string text, ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(text);
        }

        public void Write(string text) { Write(text, Settings.DefaultColor); }
        public void Write(string text, ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
            System.Console.Write(text);
        }

        public void Error(string text) { Write(text, Settings.ErrorMessageColor); }
        public void ErrorLine(string text) { WriteLine(text, Settings.ErrorMessageColor); }

        public void Warning(string text) { Write(text, Settings.WarningMessageColor); }
        public void WarningLine(string text) { WriteLine(text, Settings.WarningMessageColor); }

        public void Success(string text) { Write(text, Settings.SuccessMessageColor); }
        public void SuccessLine(string text) { WriteLine(text, Settings.SuccessMessageColor); }

        public string ReadLine() { return System.Console.ReadLine(); }
        public void Clear() { System.Console.Clear(); }

        public string Prompt(string text) { Write(text, Settings.PromptColor); return ReadLine(); }
        public string ValidatePrompt(string text, Func<string, bool> validator, string validationMessage)
        {
            var input = "";
            var tries = 0;
            var passedValidation = false;
            while (tries < Settings.MaxValidationAttempts && !passedValidation)
            {
                Write(text, Settings.PromptColor);
                input = ReadLine();
                if (validator == null || validator.Invoke(input))
                {
                    passedValidation = true;
                }
                else
                {
                    ErrorLine(validationMessage ?? "");
                }
                tries++;
            }

            if (passedValidation) return input;
            
            throw new Exception("Failed input validation.");
        }

        public Settings Settings { get; private set; }
    }
}
