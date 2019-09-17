using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SuperMetroid.GameStates
{
    public interface IStateManager
    {
        event EventHandler StateChanged;

        GameState CurrentState { get; }

        void PushState(GameState state, PlayerIndex? index);
        void ChangeState(GameState state, PlayerIndex? index);
        void PopState();
        bool ContainsState(GameState state);
    }

    /// <summary>
    /// Manager for <c>GameState</c> objects.
    /// </summary>
    public class GameStateManager : GameComponent, IStateManager
    {
        #region Fields
        private readonly Stack<GameState> gameStates = new Stack<GameState>();
        private const int DRAW_ORDER_INC = 50;
        private const int START_DRAW_ORDER = 5000;
        private int drawOrder;
        #endregion

        #region Properties
        public event EventHandler StateChanged;
        public GameState CurrentState { get { return gameStates.Peek(); } }
        #endregion

        #region Constructor
        public GameStateManager(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IStateManager), this);
        }
        #endregion

        #region Custom Methods
        public void PushState(GameState state, PlayerIndex? index)
        {
            drawOrder += DRAW_ORDER_INC;
            AddState(state, index);
            OnStateChanged();
        }

        private void AddState(GameState state, PlayerIndex? index)
        {
            gameStates.Push(state);
            state.PlayerIndexInControl = index;
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        public void PopState()
        {
            if (gameStates.Count != 0)
            {
                RemoveState();
                drawOrder -= DRAW_ORDER_INC;
                OnStateChanged();
            }
        }

        private void RemoveState()
        {
            GameState state = gameStates.Peek();
            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            gameStates.Pop();
        }

        public void ChangeState(GameState state, PlayerIndex? index)
        {
            while (gameStates.Count > 0)
            {
                RemoveState();
            }

            drawOrder = START_DRAW_ORDER;
            state.DrawOrder = drawOrder;
            drawOrder += DRAW_ORDER_INC;

            AddState(state, index);
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return gameStates.Contains(state);
        }

        protected internal virtual void OnStateChanged()
        {
            StateChanged?.Invoke(this, null);
        }
        #endregion
    }
}