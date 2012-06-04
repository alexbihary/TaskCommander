using System;
namespace TaskCommander
{
    public interface IEnvironment
    {
        void Exit(int exitCode);
    }
}
