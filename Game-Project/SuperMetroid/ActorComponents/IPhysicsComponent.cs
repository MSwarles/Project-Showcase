using Microsoft.Xna.Framework;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Interface for Physics Components.
    /// </summary>
    public interface IPhysicsComponent : IComponent
    {
        Rectangle Hitbox { get; }
        bool IsColliding { get; }
    }
}
