using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.ActorComponents;
using SuperMetroid.TileEngine;

namespace SuperMetroid
{
    /// <summary>
    /// The base class for a game object. Consists of a list of <code>IComponents</code> which 
    /// alter the behavior of the game object.
    /// </summary>
    public class Actor
    {
        #region Fields
        private Game1 m_gameRef;
        private List<IComponent> m_componentList = new List<IComponent>();
        #endregion

        #region Properties
        public IGraphicsComponent Graphics { get; set; }
        public IInputComponent Input { get; set; }
        public IPhysicsComponent Physics { get; set; }
        public Vector2 Position { get; set; } = Vector2.Zero;
        #endregion

        #region Constructor
        public Actor(Game game)
        {
            m_gameRef = (Game1)game;
        }
        #endregion

        #region XNA Methods
        /// <summary>
        /// Updates the actor.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="levelManager"></param>
        public void Update(GameTime gameTime, LevelManager levelManager)
        {
            foreach(IComponent c in m_componentList)
            {
                c.Update(gameTime, levelManager, this);
            }
        }

        /// <summary>
        /// Draws the actor.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (IComponent c in m_componentList)
            {
                c.Draw(gameTime, spriteBatch, this);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a component.
        /// </summary>
        /// <param name="component">A component.</param>
        public void AddComponent(IComponent component)
        {
            if (component is IGraphicsComponent)
            {
                if (Graphics != null)
                {
                    m_componentList.Remove(Graphics);
                }
                Graphics = (IGraphicsComponent)component;
            }
            else if (component is IInputComponent)
            {
                if (Input != null)
                {
                    m_componentList.Remove(Input);
                }
                Input = (IInputComponent)component;
            }
            if (component is IPhysicsComponent)
            {
                if (Physics != null)
                {
                    m_componentList.Remove(Physics);
                }
                Physics = (IPhysicsComponent)component;
            }
            m_componentList.Add(component);
        }
        #endregion
    }
}