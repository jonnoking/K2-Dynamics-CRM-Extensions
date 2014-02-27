using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace K2.PSUK.ServiceObjectSchema
{
    public static class Serialization
    {
        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Method to convert a custom Object to XML string
        /// </summary>
        /// <param name="pObject">Object that is to be serialized to XML</param>
        /// <returns>XML string</returns>
        public static void SerializeObject(Object pObject, string path)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                File.Delete(path);
                Stream fStream = File.OpenWrite(path);
                XmlTextWriter xmlTextWriter = new XmlTextWriter(fStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, pObject);

                xmlTextWriter.Close();
                fStream.Close();

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Method to reconstruct an Object from XML string
        /// </summary>
        /// <param name="pXmlizedString"></param>
        /// <returns></returns>
        public static Object DeserializeObject(String path, Type type)
        {
            FileStream file = new FileStream(path, FileMode.Open);

            XmlSerializer xs = new XmlSerializer(type);

            Object desFile = xs.Deserialize(file);
            file.Close();

            return desFile;
        }
    }
}
