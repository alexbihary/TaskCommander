using System;
using NUnit.Framework;
using Moq;

namespace TaskCommander.Tests
{
    [TestFixture]
    public class BuiltInCommandsTests
    {
        private Mock<IConsole> _console;
        private Mock<IEnvironment> _environment;

        [SetUp]
        public void Setup()
        {
            _console = new Mock<IConsole>();
            _environment = new Mock<IEnvironment>();
        }

        [Test]
        public void Should_Exit_When_Exit_Command_Issued()
        {
            _console.Setup(c => c.ReadLine()).Returns("x");
            
            var runner = new Runner(_console.Object, _environment.Object);
            runner.Run();

            _environment.Verify(e => e.Exit(0), Times.Once());
        }

        [Test]
        public void Should_Execute_List_Command()
        {
            _console.Setup(c => c.ReadLine()).Returns("list");

            var runner = new Runner(_console.Object, _environment.Object);
            runner.Run();

            _console.Verify(c => c.WriteLine("Available Commands:"), Times.Once());
        }
    }
}
