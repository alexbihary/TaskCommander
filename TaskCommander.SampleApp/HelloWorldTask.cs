using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq;

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
                var integerStr = console.ValidatePrompt("Enter integer: ", Helpers.Validation.IntValidator(1, 10), "Not an integer.");
                var doubleStr = console.ValidatePrompt("Enter double: ", Helpers.Validation.DoubleValidator(), "Not an double.");
                var boolStr = console.ValidatePrompt("Enter bool: ", Helpers.Validation.BoolValidator(), "Not an bool.");
            }
            else
            {
                var name = console.Prompt("What's your name? ");
                var iceCreamChoice = console.ValidatePrompt("Pick your favorite ice cream (vanilla, chocolate): ",
                    s => {
                        var isVanilla = s.Equals("vanilla", StringComparison.OrdinalIgnoreCase);
                        var isChocolate = s.Equals("chocolate", StringComparison.OrdinalIgnoreCase);
                        return isVanilla || isChocolate;
                    },
                    //2 alternate ways to pass in a Validation function
                    //s => new[] { "vanilla", "chocolate" }.Any(iceCream => iceCream.Equals(s, StringComparison.OrdinalIgnoreCase)),
                    //Validate, //or write the validation function elsewhere and reference it

                    "Please choose from the specified list.");
                console.SuccessLine("HelloWorld " + name + "! You like " + iceCreamChoice + " ice cream!");
            }
            return Prompt.Continue;
        }

        private bool Validate(string input)
        {
            return new[] { "vanilla", "chocolate" }.Any(iceCream => iceCream.Equals(input, StringComparison.OrdinalIgnoreCase));
        }
    }
}
