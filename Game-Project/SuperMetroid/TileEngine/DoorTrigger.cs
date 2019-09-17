using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// Region that causes the player to move between levels.
    /// </summary>
    public class DoorTrigger
    {
        private string m_linksTo;
        private Vector2 m_posAfterWarping;
        private Texture2D texture;

        public Rectangle Position { get; }

        public DoorTrigger(Game game, Rectangle position, string linksTo, Vector2 posAfterWarping)
        {
            texture = game.Content.Load<Texture2D>(@"Misc/boundingBox");
            Position = position;
            m_linksTo = linksTo;
            m_posAfterWarping = posAfterWarping;
        }

        public void Execute(Game game, LevelManager lm)
        {
            Level l = new Level(game, lm, m_linksTo);
            l.Alpha = 2f;
            l.IsChangingLevel = true;
            l.IsFadingIn = true;
            l.AddActor(lm.Level.Player);

            lm.ChangeLevel(l, m_posAfterWarping);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
