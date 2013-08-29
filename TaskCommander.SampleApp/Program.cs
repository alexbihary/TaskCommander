using System;

namespace TaskCommander.SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var runner = new Runner(); //default settings

            var runner = new Runner(new CustomSettings()); //or override for custom settings
            runner.Run();
        }
    }
}
