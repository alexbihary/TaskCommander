using System;
using System.ComponentModel.Composition;

namespace TaskCommander
{
    public class Configuration
    {
        [ImportMany]
        public Lazy<ITask, ITaskDescription>[] Tasks { get; set; }
    }
}
