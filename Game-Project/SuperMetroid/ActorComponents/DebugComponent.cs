using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.TileEngine;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Displays information for various in-game systems, which is used for debugging.
    /// </summary>
    public class DebugComponent
    {
        #region TODO List
        // TODO: DEBUG - design a better debug system
        #endregion

        #region Fields
        private Vector2 DEFAULT_MSG_POS = new Vector2(10, 130);
        private const float MSG_Y_BUFFER = 28f;

        private ContentManager m_content;
        private List<string> m_debugStrings = new List<string>();
        private SpriteFont m_font;
        #endregion

        #region Properties
        public bool DebugMap { get; set; } = false;
        public bool DebugPlayerGraphics { get; set; } = true;
        public bool DebugPlayerInput { get; set; } = false;
        public bool DebugPlayerPhysics { get; set; } = true;
        #endregion

        #region Constructor
        public DebugComponent(ContentManager content)
        {
            m_content = content;
            LoadContent();
        }
        #endregion

        #region XNA Methods
        protected void LoadContent()
        {
            m_font = m_content.Load<SpriteFont>(@"Fonts\InterfaceFont");
        }

        public void Update(GameTime gameTime, Actor player, Camera camera, LevelManager world)
        {
            m_debugStrings.Clear();

            if (player != null && (DebugPlayerGraphics || DebugPlayerInput || DebugPlayerPhysics))
            {
                if (DebugPlayerGraphics && player.Graphics != null)
                {
                    PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
                    m_debugStrings.Add("----------GRAPHICS----------");
                    m_debugStrings.Add("State: " + (graphics.State != null ? graphics.State.ToString() : "null"));
                    m_debugStrings.Add("Next State: " + (graphics.NextState != null ? graphics.NextState.ToString() : "null"));
                    m_debugStrings.Add("CurrentAnimation: " + (graphics.CurrentAnimationKey != null ? graphics.CurrentAnimationKey : "null"));
                    m_debugStrings.Add("CurrentFrameIndex: " + graphics.CurrentFrameIndex);
                    m_debugStrings.Add("FacingRight: " + graphics.IsFacingRight.ToString());
                    m_debugStrings.Add("ChangeFacing: " + graphics.ChangeFacing.ToString());
                    m_debugStrings.Add("DoneAnimating: " + graphics.IsDoneAnimating);
                }

                if (DebugPlayerInput && player.Input != null)
                {
                    PlayerInputComponent input = (PlayerInputComponent)player.Input;
                    m_debugStrings.Add("----------INPUT----------");
                    m_debugStrings.Add("IsUpPressed: " + input.IsUpKeyPressed + "   IsUpDown: " + input.IsUpKeyDown);
                    m_debugStrings.Add("IsDownPressed: " + input.IsDownKeyPressed + "   IsDownDown: " + input.IsDownKeyDown);
                }

                if (DebugPlayerPhysics && player.Physics != null)
                {
                    PlayerPhysicsComponent physics = (PlayerPhysicsComponent)player.Physics;
                    m_debugStrings.Add("----------PHYSICS----------");
                    m_debugStrings.Add("State: " + physics.State);
                    m_debugStrings.Add("Vel.X: " + physics.Velocity.X + "  Vel.Y: " + physics.Velocity.Y);
                    m_debugStrings.Add("Hitbox.Width: " + physics.Hitbox.Width + "  Hitbox.Height: " + physics.Hitbox.Height);
                    m_debugStrings.Add("IsOnGround: " + physics.IsOnGround);
                }

                if (DebugMap)
                {
                    m_debugStrings.Add("Pos.X: " + player.Position.X + "  Pos.Y: " + player.Position.Y);
                    m_debugStrings.Add("Camera X: " + camera.Position.X + "  " + "Camera Y: " + camera.Position.Y);
                    m_debugStrings.Add("Camera Zoom:  " + camera.Zoom);
                    m_debugStrings.Add("Map Width: " + world.WidthInPixels + "  " + "Map Height:  " + world.HeightInPixels);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 msgPos = DEFAULT_MSG_POS;

            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                null);
            foreach (string s in m_debugStrings)
            {
                spriteBatch.DrawString(m_font, s, msgPos, Color.White);
                msgPos.Y += MSG_Y_BUFFER;
            }
            spriteBatch.End();
        }
        #endregion

        public void AddString(string s) => m_debugStrings.Add(s);
    }
}