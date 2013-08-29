using System;
using System.Collections.Generic;

namespace TaskCommander.SampleApp
{
    [Task(Name="error", Description="report than an error occurred.")]
    public class ErrorTask : ITask
    {
        public Prompt Run(IDictionary<string, string> args, IConsole console)
        {
            console.ErrorLine("Yikes, an error!");
            throw new Exception("what@!!@@");

            //or handle errors internally and just return Prompt.Error
            //return Prompt.Error;
        }
    }
}
