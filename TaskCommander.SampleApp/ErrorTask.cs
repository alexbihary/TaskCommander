using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskCommander.SampleApp
{
    [Task(Name="error", Description="report than an error occurred.")]
    public class ErrorTask : ITask
    {
        public Prompt Run(IDictionary<string, string> args, IConsole console)
        {
            console.WriteLine("Yikes, an error!");
            return Prompt.Error;
        }
    }
}
