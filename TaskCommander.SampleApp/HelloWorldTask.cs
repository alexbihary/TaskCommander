using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;

namespace TaskCommander.SampleApp
{
    [Task(Name="hello", Description="Starter task that simply prints HelloWorld.")]
    public class HelloWorldTask : ITask
    {
        public void Run(IDictionary<string, string> args, IConsole console)
        {
            if (args.ContainsKey("name"))
            {
                console.WriteLine("Hello " + args["name"]);
            }
            else
            {
                console.WriteLine("HelloWorld");
            }
        }
    }
}
