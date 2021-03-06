﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TaskCommander
{
    public class Runner
    {
        private IConsole _console;
        private IEnvironment _environment;
        private Configuration _configuration;
        private string _prompt = "$ ";
        private IDictionary<string, string> _baseCommands;
        private Settings _settings;
        const string TenSpaces = "           ";

        public Runner(Settings settings = null)
        {
            _settings = settings ?? new Settings();
            _console = new Console(_settings);
            _environment = new Environment();
            ComposeConfiguration();
            Setup();
        }

        public Runner(IConsole console, IEnvironment environment)
        {
            _console = console;
            _environment = environment;
            _settings = _console.Settings;
            ComposeConfiguration();
            Setup();
        }

        private void Setup()
        {
            _baseCommands = new Dictionary<string, string>();
            
            _baseCommands.Add("clear", "Clear the screen. Same as 'cls'.");
            _baseCommands.Add("exit", "Exit the program. Same as 'quit', 'x', or 'q'.");
            _baseCommands.Add("help", "See more information on a specific command.");
            _baseCommands.Add("list", "View all available commands.");
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
            _console.WriteLine(String.Format("{0} {1}", _settings.WindowTitle, _settings.WindowSubTitle), _settings.TitleColor);
            _console.WriteLine("Type 'list' to view all available commands. Type 'x' to exit.");
            var prompt = Prompt.Continue;
            try
            {
                while (prompt != Prompt.Stop)
                {
                    _console.WriteLine();
                    _console.Write(_prompt, _settings.PromptColor);
                    var command = _console.ReadLine();
                    prompt = RunCommand(command);
                }
                
            }
            catch (Exception ex)
            {
                _console.WriteLine();
                _console.ErrorLine("Sorry, an exception occurred...");
                _console.WriteLine(String.Format("Type: {0}", ex.GetType().FullName));
                _console.WriteLine(String.Format("Message: {0}", ex.Message));
                _console.WriteLine(String.Format("Source: {0}", ex.Source));
                _console.WriteLine(String.Format("Stacktrace: {0}", ex.StackTrace));
                _console.SuccessLine("Recovering...");
                _console.WriteLine();
                Run();
            }
        }

        private Prompt RunCommand(string command)
        {
            var originalCommand = command;
            Prompt prompt = Prompt.Continue;
            IDictionary<string, string> flags;
            command = ParseFlags(command, out flags);

            if (command.MatchesAny(new string[] { "x", "q", "exit", "quit" }))
            {
                _environment.Exit(0);
                return Prompt.Stop;
            }

            if (command.Matches("list"))
            {
                _console.WriteLine("Available commands:");
                foreach (var cmd in _baseCommands)
                {
                    WriteCommandHelp(cmd.Key, cmd.Value);
                }
                _console.WriteLine();
                foreach (var task in _configuration.Tasks)
                {
                    WriteCommandHelp(task.Metadata.Name.Substring(0, Math.Min(TenSpaces.Length, task.Metadata.Name.Length)), task.Metadata.Description);
                }
                _console.WriteLine();
                WriteHelpInfomation();
            }
            else if (command.Matches("help"))
            {
                if (flags.Count == 1)
                {
                    var task = _configuration.Tasks.SingleOrDefault<Lazy<ITask, ITaskDescription>>(t => t.Metadata.Name.Matches(flags.Single().Key));
                    if (task == null)
                    {
                        WriteCommandWarning();
                        WriteHelpInfomation();
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(task.Metadata.Help))
                        {
                            WriteHelpInfomation();
                        }
                        else
                        {
                            _console.WriteLine(task.Metadata.Help);
                        }
                    }
                }
                else
                {
                    WriteHelpInfomation();
                }
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
                    WriteCommandWarning();
                }
                else
                {
                    prompt = task.Value.Run(flags, _console);
                }
            }

            if (prompt == Prompt.Error)
            {
                _console.WriteLine(String.Format("An error occurred while running this command: {0}", originalCommand), _settings.ErrorMessageColor);
            }
            return prompt;
        }

        private void WriteHelpInfomation()
        {
            _console.WriteLine("Type 'help <command>' for more information on a specific command.");
        }

        private void WriteCommandWarning()
        {
            _console.WriteLine("Command not recognized. Type 'list' to view all commands.", _settings.WarningMessageColor);
        }

        private void WriteCommandHelp(string name, string description)
        {
            _console.Write("  " + name, _settings.HelpKeyColor);
            _console.Write(TenSpaces.Substring(0, TenSpaces.Length - name.Length));
            _console.WriteLine(description, _settings.HelpValueColor);
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
