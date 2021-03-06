﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskCommander.SampleApp
{
    [Task(Name="goodbye", Description="Simple task that says goodbye.")]
    public class GoodbyeTask : ITask
    {
        public Prompt Run(IDictionary<string, string> args, IConsole console)
        {
            console.WriteLine("Goodbye...");
            return Prompt.Stop;
        }
    }
}
