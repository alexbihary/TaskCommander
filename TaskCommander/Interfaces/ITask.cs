using System;
using System.Collections.Generic;

namespace TaskCommander
{
    public interface ITask
    {
        Prompt Run(IDictionary<string, string> args, IConsole console);
    }
}
