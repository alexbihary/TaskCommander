using System;

namespace TaskCommander
{
    public class Environment : TaskCommander.IEnvironment
    {
        public void Exit(int exitCode)
        {
            System.Environment.Exit(exitCode);
        }
    }
}
