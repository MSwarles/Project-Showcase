using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyXMLData.AnimationClasses
{
    /// <summary>
    /// Holds data for animation frames.
    /// </summary>
    public class AnimationFrameData
    {
        public int XOffset;
        public int YOffset;
        public Color Color;
        public float Rotation;
        public float Alpha;
        public float Scale;
        public SpriteEffects SpriteEffects;
        public int MessageType;
        public string MessageInfo;

        public AnimationFrameData()
        {
        }

        public AnimationFrameData(int xOffset, int yOffset, Color color, float alpha, float rotation, 
            float scale, SpriteEffects spriteEffects, int messageType, string messageInfo)
        {
            XOffset = xOffset;
            YOffset = yOffset;
            Color = color;
            Alpha = alpha;
            Rotation = rotation;
            Scale = scale;
            SpriteEffects = spriteEffects;
            MessageType = messageType;
            MessageInfo = messageInfo;
        }
    }
}