using System.IO;
using System.Xml;

using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace SuperMetroid
{
    /// <summary>
    /// Used to serialize data to XML or deserialize XML to data.
    /// </summary>
    static class XnaSerializer
    {
        /// <summary>
        /// Convert data to XML.
        /// </summary>
        /// <param name="fileName">File to be written to.</param>
        /// <param name="data">Data to be converted to XML.</param>
        public static void Serialize<T>(string fileName, T data)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(fileName, settings))
            {
                IntermediateSerializer.Serialize<T>(writer, data, null);
            }
        }
        /// <summary>
        /// Convert XML to data.
        /// </summary>
        /// <param name="filename">File to be read from.</param>
        /// <returns>Data converted from XML.</returns>
        public static T Deserialize<T>(string filename)
        {
            T data;
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                using (XmlReader reader = XmlReader.Create(stream))
                {
                    data = IntermediateSerializer.Deserialize<T>(reader, null);
                }
            }

            return data;
        }
    }
}