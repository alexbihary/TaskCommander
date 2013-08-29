using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskCommander.SampleApp
{
    public class CustomSettings : Settings
    {
        public override ConsoleColor ErrorMessageColor { get { return ConsoleColor.Red; } }
    }
}
