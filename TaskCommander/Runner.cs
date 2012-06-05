using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TaskCommander
{
    public class Runner
    {
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
            var originalCommand = command;
            Prompt prompt = Prompt.Continue;
            IDictionary<string, string> flags;
            command = ParseFlags(command, out flags);

            if (command.MatchesAny(new string[] { "x", "q", "exit", "quit" }))
            {
                _environment.Exit(0);
                return;
            }

            if (command.MatchesAny(new string[] {"list", "help", "h", "?"}))
            {
                _console.WriteLine();
                _console.WriteLine("Available commands:");
                _console.WriteLine(String.Format("  {0} : {1}", "list", "View all available commands. Same as 'help', 'h', or '?'."));
                _console.WriteLine(String.Format("  {0} : {1}", "clear", "Clear the screen. Same as 'cls'."));
                _console.WriteLine(String.Format("  {0} : {1}", "exit", "Exit the program. Same as 'quit', 'x', or 'q'."));
                _console.WriteLine();
                foreach (var task in _configuration.Tasks)
                    _console.WriteLine(String.Format("  {0} : {1}", task.Metadata.Name, task.Metadata.Description));
            }
            else if (command.MatchesAny(new string[] { "clear", "cls" }))
            {
                _console.Clear();
            }
            else if (!String.IsNullOrWhiteSpace(command))
            {
                var task = _configuration.Tasks.SingleOrDefault<Lazy<ITask, ITaskDescription>>(t => t.Metadata.Name.Matches(command));
                if (task == null)
                {
                    _console.WriteLine("Command not recognized.");
                }
                else
                {
                    prompt = task.Value.Run(flags, _console);
                }
            }

            if (prompt == Prompt.Error)
            {
                _console.WriteLine();
                _console.WriteLine(String.Format("An error occurred while running this command: {0}", originalCommand));
            }
            if (prompt != Prompt.Stop)
            {
                _console.WriteLine();
                _console.Write(" > ");

                var nextCommand = _console.ReadLine();
                RunCommand(nextCommand); 
            }
        }

        private string ParseFlags(string command, out IDictionary<string, string> flags)
        {
            command = command.Trim();
            flags = new Dictionary<string, string>();
            if (command.IndexOf(' ') > 0)
            {
                var flagString = command.Substring(command.IndexOf(' '));
                var regex = new Regex(@"\s+(-|/)?(?<flag>[\w-]+)([:=](?("")""(?<value>[^""]+)""|(?<value>\w+)))?");
                var matches = regex.Matches(flagString);
                foreach (Match match in matches)
                {
                    if (match.Groups["flag"].Success && !String.IsNullOrWhiteSpace(match.Groups["flag"].Value))
                    {
                        if (match.Groups["value"].Success && !String.IsNullOrWhiteSpace(match.Groups["value"].Value))
                        {
                            flags[match.Groups["flag"].Value] = match.Groups["value"].Value;
                        }
                        else
                        {
                            flags[match.Groups["flag"].Value] = null;
                        }
                    }
                }
                return command.Substring(0, command.IndexOf(' '));
            }
            else
            {
                return command;
            }
        }
    }
}
