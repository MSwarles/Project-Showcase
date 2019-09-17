using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SuperMetroid.Components
{
    public enum MouseButtons { Center, Left, Right }

    /// <summary>
    /// Used to get the current and previous states of user input devices.
    /// </summary>
    public class Xin : GameComponent
    {
        #region Keyboard Properties
        public static KeyboardState KeyboardState { get; private set; } = Keyboard.GetState();
        public static KeyboardState PreviousKeyboardState { get; private set; } = Keyboard.GetState();
        #endregion

        #region Mouse Properties
        public static MouseState MouseState { get; private set; } = Mouse.GetState();
        public static MouseState PreviousMouseState { get; private set; } = Mouse.GetState();
        #endregion

        #region GamePad Properties
        public static GamePadState[] GamePadState { get; private set; } = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
        public static GamePadState[] PreviousGamePadState { get; private set; } = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
        #endregion

        #region Constructor
        public Xin(Game game): base(game)
        {
        }
        #endregion

        #region XNA Methods
        public override void Update(GameTime gameTime)
        {
            PreviousKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            PreviousMouseState = MouseState;
            MouseState = Mouse.GetState();

            PreviousGamePadState = GamePadState;
            GamePadState = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                GamePadState[(int)index] = GamePad.GetState(index);
            }

            base.Update(gameTime);
        }
        #endregion

        #region General Methods
        public static void FlushInput()
        {
            MouseState = PreviousMouseState;
            KeyboardState = PreviousKeyboardState;
            GamePadState = PreviousGamePadState;
        }
        #endregion

        #region Keyboard Methods
        public static bool KeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return KeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key);
        }
        #endregion

        #region Mouse Methods
        public static bool CheckMouseReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return (MouseState.LeftButton == ButtonState.Released) && (PreviousMouseState.LeftButton == ButtonState.Pressed);
                case MouseButtons.Right:
                    return (MouseState.RightButton == ButtonState.Released) && (PreviousMouseState.RightButton == ButtonState.Pressed);
                case MouseButtons.Center:
                    return (MouseState.MiddleButton == ButtonState.Released) && (PreviousMouseState.MiddleButton == ButtonState.Pressed);
            }

            return false;
        }
        #endregion

        #region GamePad Methods
        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return GamePadState[(int)index].IsButtonUp(button) &&
                PreviousGamePadState[(int)index].IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return GamePadState[(int)index].IsButtonDown(button) &&
                PreviousGamePadState[(int)index].IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button, PlayerIndex index)
        {
            return GamePadState[(int)index].IsButtonDown(button);
        }
        #endregion
    }
}