using System;

namespace TaskCommander
{
    public interface ITaskDescription
    {
        string Name { get; }
        string Description { get; }
    }
}
