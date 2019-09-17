using Microsoft.Xna.Framework;

namespace SuperMetroid.GameStates
{
    /// <summary>
    /// Base class for <c>GameState</c> class.
    /// </summary>
    public class BaseGameState : GameState
    {
        protected Game1 GameRef;

        public BaseGameState(Game game)
            : base(game)
        {
            GameRef = (Game1)game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}