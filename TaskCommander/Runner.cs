using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace TaskCommander
{
    public class Runner
    {
        private const char SEPARATOR = ':';
        private IConsole _console;
        private IEnvironment _environment;
        private Configuration _configuration;

        public Runner()
        {
            _console = new Console();
            _environment = new Environment();
            ComposeConfiguration();
        }

        public Runner(IConsole console, IEnvironment environment)
        {
            _console = console;
            _environment = environment;
            ComposeConfiguration();
        }

        private void ComposeConfiguration()
        {
            _configuration = new Configuration();
            var catalog = new AssemblyCatalog(Assembly.GetEntryAssembly());
            var container = new CompositionContainer(catalog);
            container.ComposeParts(_configuration);
        }

        public void Run()
        {
            _console.WriteLine("[TaskCommander]");
            _console.WriteLine("Type 'list' to view all available commands. Type 'x' to exit.");
            _console.Write(" > ");
            var command = _console.ReadLine();
            try
            {
                RunCommand(command);
            }
            catch (Exception ex)
            {
                _console.WriteLine();
                _console.WriteLine("Sorry, an exception occurred...");
                _console.WriteLine(String.Format("{0}: {1}", ex.GetType().FullName, ex.Message));
                _console.WriteLine(String.Format("Source: {0}", ex.Source));
                _console.WriteLine(String.Format("Stacktrace: {0}", ex.StackTrace));
                _console.WriteLine();
                Run();
            }
        }

        private void RunCommand(string command)
        {
            if (command.MatchesAny(new string[] { "x", "q", "exit", "quit" }))
            {
                _environment.Exit(0);
                return;
            }

            if (command.MatchesAny(new string[] {"list", "help"}))
            {
                _console.WriteLine();
                _console.WriteLine("Available commands:");
                foreach (var task in _configuration.Tasks)
                    _console.WriteLine(String.Format("  {0} : {1}", task.Metadata.Task, task.Metadata.Description));
            }
            else if (!String.IsNullOrWhiteSpace(command))
            {
                var task = _configuration.Tasks.SingleOrDefault<Lazy<ITask, ITaskDescription>>(t => t.Metadata.Task.Matches(command));
                var result = task.Value.Run(null, _console);
            }

            _console.WriteLine();
            _console.Write(" > ");

            var nextCommand = _console.ReadLine();
            RunCommand(nextCommand);
        }
    }
}
