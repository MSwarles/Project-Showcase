using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.TileEngine;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Interface for components used by Actor class.
    /// </summary>
    public interface IComponent
    {
        void Update(GameTime gameTime, LevelManager level, Actor actor);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, Actor actor);
    }
}
