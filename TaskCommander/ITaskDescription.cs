using System;

namespace TaskCommander
{
    public interface ITaskDescription
    {
        string Task { get; }
        string Description { get; }
    }
}
