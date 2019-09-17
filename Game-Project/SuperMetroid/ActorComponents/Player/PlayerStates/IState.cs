using Microsoft.Xna.Framework;

namespace SuperMetroid.PlayerStates
{
    /// <summary>
    /// Interface for player states used by graphics component.
    /// </summary>
    public interface IState
    {
        bool IsDoneTransitioning { get; set; }
        void Update(GameTime gameTime, Actor actor);
        void OnExit(Actor actor);
    }
}
