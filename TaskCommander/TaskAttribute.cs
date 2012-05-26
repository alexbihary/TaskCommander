using System;
using System.ComponentModel.Composition;

namespace TaskCommander
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=true)]
    public class TaskAttribute : ExportAttribute, ITaskDescription
    {
        public TaskAttribute() : base(typeof(ITask)) { }
        
        public string Task { get; set; }
        public string Description { get; set; }
    }
}
