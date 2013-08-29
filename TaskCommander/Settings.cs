using System;

namespace TaskCommander
{
    public class Settings
    {
        public virtual ConsoleColor DefaultColor { get { return ConsoleColor.Gray; } }
        public virtual ConsoleColor PromptColor { get { return ConsoleColor.White; } }
        public virtual ConsoleColor TitleColor { get { return ConsoleColor.DarkYellow; } }
        public virtual ConsoleColor ErrorMessageColor { get { return ConsoleColor.DarkRed; } }
        public virtual ConsoleColor WarningMessageColor { get { return ConsoleColor.DarkYellow; } }        
        public virtual ConsoleColor SuccessMessageColor { get { return ConsoleColor.DarkGreen; } }
        public virtual ConsoleColor ListKeyColor { get { return ConsoleColor.DarkCyan; } }
        public virtual ConsoleColor ListValueColor { get { return ConsoleColor.Gray; } }
        public virtual ConsoleColor HelpKeyColor { get { return ConsoleColor.DarkGreen; } }
        public virtual ConsoleColor HelpValueColor { get { return ConsoleColor.Gray; } }

        public virtual string WindowTitle { get { return "[TaskCommander]"; } }
        public virtual string WindowSubTitle { get { return ""; } }
    }
}
