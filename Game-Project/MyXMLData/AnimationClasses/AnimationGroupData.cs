using System.Collections.Generic;

namespace MyXMLData.AnimationClasses
{
    /// <summary>
    /// Holds data for animation groups.
    /// </summary>
    public class AnimationGroupData
    {
        public AnimationData[] AnimationData;
        public string[] AnimationNames;
        public string GroupName;
        public string TextureFilePath;

        public AnimationGroupData()
        {
        }

        public AnimationGroupData(List<string> animationNames, List<AnimationData> animationData,
            string groupName, string textureFilePath)
        {
            AnimationNames = animationNames.ToArray();
            AnimationData = animationData.ToArray();
            GroupName = groupName;
            TextureFilePath = textureFilePath;
        }
    }
}
