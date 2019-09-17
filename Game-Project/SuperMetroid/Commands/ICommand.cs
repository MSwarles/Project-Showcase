
namespace SuperMetroid.Commands
{
    /// <summary>
    /// Interface for commands.
    /// </summary>
    public interface ICommand
    {
        void Execute(Actor actor);
    }
}