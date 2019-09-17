using System.Collections.Generic;

namespace MyXMLData.AnimationClasses
{
    /// <summary>
    /// Holds data for animations.
    /// </summary>
    public class AnimationData
    {
        public AnimationFrameData[] AnimationFrames;
        public string Name;
        public int FrameWidth;
        public int FrameHeight;
        public int FramesPerSecond;
        public bool IsLoop;

        public AnimationData()
        {
        }

        public AnimationData(List<AnimationFrameData> animationFrames, int frameWidth, 
            int frameHeight, int framesPerSecond, bool loop)
        {
            AnimationFrames = animationFrames.ToArray();
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FramesPerSecond = framesPerSecond;
            IsLoop = loop;
        }
    }
}
