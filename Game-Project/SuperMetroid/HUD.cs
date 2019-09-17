using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SuperMetroid.Components;
using SuperMetroid.Controls;

namespace SuperMetroid
{
    /// <summary>
    /// Displays various information useful to the player, including: health, weapons/ammo, and a minimap.
    /// </summary>
    public class HUD
    {
        #region Fields
        private const int HUD_HEIGHT = 120;
        private const int X_BUFFER = 32;
        private const int Y_BUFFER = 16;

        private Game1 gameRef;
        private ControlManager m_controlManager;
        private SpriteFont m_font;

        private List<PictureBox> m_pbEnergyList = new List<PictureBox>();
        private PictureBox m_pbBackground;
        private PictureBox m_pbEnergy;
        private PictureBox m_pbMissile;
        private PictureBox m_pbSuperMissile;
        private PictureBox m_pbSuperBomb;
        private PictureBox m_pbGrapplingHook;
        private PictureBox m_pbXray;
        // TODO: HUD - add functioning minimap
        private PictureBox m_pbMap;

        private Label m_lEnergy;
        private Label m_lMissileCount;
        private Label m_lSuperMissileCount;
        private Label m_lSuperBombCount;

        // TODO: HUD - implement player inventory system and use those values instead of dummy values
        private int m_energyTanks = 8;
        private int m_energy = 1499;
        // NOT USED private int m_reserveTanks = 0;
        private int m_missileCount = 255;
        private int m_superMissileCount = 55;
        private int m_superBombCount = 11;
     
        private int m_hudWidth;        
        #endregion

        #region Constructor
        public HUD(Game game)
        {
            gameRef = (Game1)game;
            m_controlManager = new ControlManager(gameRef.Font);

            m_font = gameRef.Font;
            m_hudWidth = gameRef.ScreenRectangle.Width;

            Init();
        }
        #endregion

        #region XNA Methods
        public void Init()
        {
            ContentManager content = gameRef.Content;

            Texture2D activeTexture = content.Load<Texture2D>(@"Misc\lars");
            Texture2D inactiveTexture;

            m_pbBackground = new PictureBox(activeTexture, new Rectangle(0, 0, m_hudWidth, HUD_HEIGHT));
            m_pbBackground.Color = Color.Black;
            m_controlManager.Add(m_pbBackground);

            Vector2 position = new Vector2(X_BUFFER, m_pbBackground.Size.Y);

            m_lEnergy = new Label();
            m_lEnergy.Text = "ENERGY           " + (m_energy % 100).ToString("00.##");
            m_lEnergy.Size = m_font.MeasureString(m_lEnergy.Text);
            m_lEnergy.Position = new Vector2(position.X, HUD_HEIGHT - m_lEnergy.Size.Y);
            m_controlManager.Add(m_lEnergy);

            for (int x = 1; x <= m_energyTanks; x++)
            {
                activeTexture = content.Load<Texture2D>(@"HUD\energyNode");
                inactiveTexture = content.Load<Texture2D>(@"HUD\energyNodeInactive");

                m_pbEnergy = new PictureBox(activeTexture, inactiveTexture, new Rectangle(0, 0, activeTexture.Width, activeTexture.Height));
                m_pbEnergy.Size = new Vector2(activeTexture.Width, activeTexture.Height);

                if (x <= 7)
                { 
                    m_pbEnergy.Position = new Vector2(position.X + activeTexture.Width * (x - 1), m_lEnergy.Position.Y - activeTexture.Height);
                }
                else
                { 
                    m_pbEnergy.Position = new Vector2(position.X + activeTexture.Width * (x - 8), m_lEnergy.Position.Y - activeTexture.Height * 2);
                }

                if (x > m_energyTanks) { m_pbEnergy.IsVisible = false; }

                if (m_energy / x > 100) { m_pbEnergy.Active = true; }

                m_pbEnergyList.Add(m_pbEnergy);
            }

            foreach (Control c in m_pbEnergyList)
            {
                m_controlManager.Add(c);
            }

            position.X += m_lEnergy.Size.X + (X_BUFFER * 5);
            position.Y = Y_BUFFER;
          
            activeTexture = content.Load<Texture2D>(@"HUD\missileSelected");
            inactiveTexture = content.Load<Texture2D>(@"HUD\missileIcon");
            m_pbMissile = new PictureBox(activeTexture, inactiveTexture, new Rectangle(0, 0, activeTexture.Width, activeTexture.Height));
            m_pbMissile.Size = new Vector2(activeTexture.Width, activeTexture.Height);
            m_pbMissile.Position = position;
            m_pbMissile.IsTabStop = true;
            m_controlManager.Add(m_pbMissile);

            m_lMissileCount = new Label();
            m_lMissileCount.Text = m_missileCount.ToString("000.##");
            m_lMissileCount.Size = m_font.MeasureString(m_lMissileCount.Text);
            m_lMissileCount.Position = new Vector2(position.X + ((m_pbMissile.Size.X - m_lMissileCount.Size.X) / 2), HUD_HEIGHT - m_lMissileCount.Size.Y);
            m_controlManager.Add(m_lMissileCount);

            position.X += m_pbMissile.Size.X + (X_BUFFER);

            activeTexture = content.Load<Texture2D>(@"HUD\superMissileSelected");
            inactiveTexture = content.Load<Texture2D>(@"HUD\superMissileIcon");
            m_pbSuperMissile = new PictureBox(activeTexture, inactiveTexture, new Rectangle(0, 0, activeTexture.Width, activeTexture.Height));
            m_pbSuperMissile.Size = new Vector2(activeTexture.Width, activeTexture.Height);
            m_pbSuperMissile.Position = position;
            m_pbSuperMissile.IsTabStop = true;
            m_controlManager.Add(m_pbSuperMissile);

            m_lSuperMissileCount = new Label();
            m_lSuperMissileCount.Text = m_superMissileCount.ToString("00.##");
            m_lSuperMissileCount.Size = m_font.MeasureString(m_lSuperMissileCount.Text);
            m_lSuperMissileCount.Position = new Vector2(position.X + ((m_pbSuperMissile.Size.X - m_lSuperMissileCount.Size.X) / 2), HUD_HEIGHT - m_lSuperMissileCount.Size.Y);
            m_controlManager.Add(m_lSuperMissileCount);

            position.X += m_pbSuperMissile.Size.X + (X_BUFFER);

            activeTexture = content.Load<Texture2D>(@"HUD\superBombSelected");
            inactiveTexture = content.Load<Texture2D>(@"HUD\superBombIcon");
            m_pbSuperBomb = new PictureBox(activeTexture, inactiveTexture, new Rectangle(0, 0, activeTexture.Width, activeTexture.Height));
            m_pbSuperBomb.Size = new Vector2(activeTexture.Width, activeTexture.Height);
            m_pbSuperBomb.Position = position;
            m_pbSuperBomb.IsTabStop = true;
            m_controlManager.Add(m_pbSuperBomb);

            m_lSuperBombCount = new Label();
            m_lSuperBombCount.Text = m_superBombCount.ToString("00.##");
            m_lSuperBombCount.Size = m_font.MeasureString(m_lSuperBombCount.Text);
            m_lSuperBombCount.Position = new Vector2(position.X + ((m_pbSuperBomb.Size.X - m_lSuperBombCount.Size.X) / 2), HUD_HEIGHT - m_lSuperBombCount.Size.Y);
            m_controlManager.Add(m_lSuperBombCount);

            position.X += m_pbSuperBomb.Size.X + (X_BUFFER);

            activeTexture = content.Load<Texture2D>(@"HUD\grapplingHookSelected");
            inactiveTexture = content.Load<Texture2D>(@"HUD\grapplingHookIcon");
            m_pbGrapplingHook = new PictureBox(activeTexture, inactiveTexture, new Rectangle(0, 0, activeTexture.Width, activeTexture.Height));
            m_pbGrapplingHook.Size = new Vector2(activeTexture.Width, activeTexture.Height);
            m_pbGrapplingHook.Position = position;
            m_pbGrapplingHook.IsTabStop = true;
            m_controlManager.Add(m_pbGrapplingHook);

            position.X += m_pbGrapplingHook.Size.X + (X_BUFFER);

            activeTexture = content.Load<Texture2D>(@"HUD\xRaySelected");
            inactiveTexture = content.Load<Texture2D>(@"HUD\xRayIcon");
            m_pbXray = new PictureBox(activeTexture, inactiveTexture, new Rectangle(0, 0, activeTexture.Width, activeTexture.Height));
            m_pbXray.Size = new Vector2(activeTexture.Width, activeTexture.Height);
            m_pbXray.Position = position;
            m_pbXray.IsTabStop = true;
            m_controlManager.Add(m_pbXray);

            activeTexture = content.Load<Texture2D>(@"HUD\mapTemp");
            m_pbMap = new PictureBox(activeTexture, new Rectangle(0, 0, activeTexture.Width, activeTexture.Height));
            m_pbMap.Size = new Vector2(activeTexture.Width, activeTexture.Height);
            m_pbMap.Position = new Vector2(m_pbBackground.Size.X - m_pbMap.Size.X - X_BUFFER, Y_BUFFER);
            m_pbMap.IsTabStop = true;
            m_controlManager.Add(m_pbMap);
        }

        public void Update(Camera camera)
        {
            // REMOVE LATER - FOR TESTING ONLY
            if (Xin.KeyDown(Keys.OemMinus))
            {
                m_energy -= 3;
            }
            else if (Xin.KeyDown(Keys.OemPlus))
            {
                m_energy += 3;
            }

            if (Xin.KeyPressed(Keys.Tab)) { m_controlManager.NextControl(); }

            m_energy = MathHelper.Clamp(m_energy, 0, m_energyTanks * 100 + 99);

            for (int x = 1; x <= m_energyTanks; x++)
            {
                if (m_energy / x < 100)
                { 
                    m_pbEnergyList[x - 1].Active = false;
                }
                else
                { 
                    m_pbEnergyList[x - 1].Active = true;
                }
            }

            foreach (Control c in m_controlManager)
            {
                if (c is Label)
                {
                    if (c.Equals(m_lEnergy))
                    {
                        c.Text = "ENERGY           " + (m_energy % 100).ToString("00.##");
                    }
                    else if (c.Equals(m_lMissileCount))
                    {
                        c.Text = m_missileCount.ToString("000.##");
                    }
                    else if (c.Equals(m_lSuperMissileCount))
                    {
                        c.Text = m_superMissileCount.ToString("00.##");
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin();
            m_controlManager.Draw(spriteBatch);
            spriteBatch.End();
        }
        #endregion
    }
}
