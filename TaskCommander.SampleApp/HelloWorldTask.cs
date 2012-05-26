using System;
using System.ComponentModel.Composition;

namespace TaskCommander.SampleApp
{
    [Task(Task="hello", Description="Starter task that simply prints HelloWorld.")]
    public class HelloWorldTask : ITask
    {
        public bool Run(dynamic args, IConsole console)
        {
            console.WriteLine("HelloWorld");
            return true;
        }
    }
}
