using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyXMLData.AnimationClasses;

namespace SuperMetroid.Animations
{
    /// <summary>
    /// A single frame of animation; used by the <c>Animation</c> class.
    /// </summary>
    public class AnimationFrame
    {
        #region Fields
        private Color m_color;
        #endregion

        #region Properties
        public float Alpha { get; set; }
        public Color Color { get { return m_color * Alpha; } set { m_color = value; } }
        public string MessageInfo { get; set; }
        public int MessageType { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        #endregion

        #region Constructor
        public AnimationFrame(int xOffset, int yOffset, Color color, float rotation, float scale, 
            SpriteEffects spriteEffects)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            Color = color;
            Rotation = rotation;
            Scale = scale;
            SpriteEffects = spriteEffects;

            Alpha = 1f;
            MessageInfo = "";
            MessageType = 0;
        }

        // Used for cloning
        private AnimationFrame(AnimationFrame frame)
        {
            XOffset = frame.XOffset;
            YOffset = frame.YOffset;
            m_color = frame.m_color;
            Alpha = frame.Alpha;
            Rotation = frame.Rotation;
            Scale = frame.Scale;
            SpriteEffects = frame.SpriteEffects;
            MessageInfo = frame.MessageInfo;
            MessageType = frame.MessageType;
        }
        #endregion

        #region Custom Methods
        public object Clone()
        {
            return new AnimationFrame(this);
        }
        #endregion

        #region Data Methods
        public static AnimationFrame FromAnimationFrameData(AnimationFrameData data)
        {
            AnimationFrame animationFrame = new AnimationFrame(
                data.XOffset,
                data.YOffset,
                data.Color,
                data.Rotation,
                data.Scale,
                data.SpriteEffects);
            animationFrame.Alpha = data.Alpha;
            animationFrame.MessageInfo = data.MessageInfo;
            animationFrame.MessageType = data.MessageType;

            return animationFrame;
        }
        #endregion
    }
}
