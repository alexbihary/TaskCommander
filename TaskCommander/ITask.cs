using System;

namespace TaskCommander
{
    public interface ITask
    {
        bool Run(dynamic args, IConsole console);
    }
}
