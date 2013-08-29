﻿using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;

namespace TaskCommander.SampleApp
{
    [Task(Name="hello", 
        Description="Starter task that simply prints HelloWorld.",
        Help="hello [<options>]\n" +
             "      -name:<value>   Outputs 'Hello <value>'")]
    public class HelloWorldTask : ITask
    {
        public Prompt Run(IDictionary<string, string> args, IConsole console)
        {
            if (args.ContainsKey("name"))
            {
                console.WarningLine("Hello " + args["name"]);
            }
            else
            {
                var name = console.Prompt("What's your name? ");
                console.SuccessLine("HelloWorld " + name + "!");
            }
            return Prompt.Continue;
        }
    }
}
