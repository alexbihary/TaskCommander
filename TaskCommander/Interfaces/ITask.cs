using System;
using System.Collections.Generic;

namespace TaskCommander
{
    public interface ITask
    {
        void Run(IDictionary<string, string> args, IConsole console);
    }
}
