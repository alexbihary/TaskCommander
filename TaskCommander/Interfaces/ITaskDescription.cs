
namespace TaskCommander
{
    public interface ITaskDescription
    {
        string Name { get; }
        string Description { get; }
        string Help { get; }
    }
}
