using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class SerializationHelper
    {
        private static readonly Dictionary<int, XmlSerializer> SerializerDict = new Dictionary<int, XmlSerializer>();

        public static XmlSerializer GetSerializer(Type t)
        {
            var typeHash = t.GetHashCode();

            if (!SerializerDict.ContainsKey(typeHash))
                SerializerDict.Add(typeHash, new XmlSerializer(t));

            return SerializerDict[typeHash];
        }

        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        public static bool Save(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return true;
        }

        public static string Serialize(object obj)
        {
            string returnStr;
            var serializer = GetSerializer(obj.GetType());
            var ms = new MemoryStream();
            XmlTextWriter xtw = null;
            StreamReader sr = null;
            try
            {
                xtw = new XmlTextWriter(ms, Encoding.UTF8) {Formatting = Formatting.Indented};
                serializer.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                sr = new StreamReader(ms);
                returnStr = sr.ReadToEnd();
            }
            finally
            {
                if (xtw != null)
                    xtw.Close();
                if (sr != null)
                    sr.Close();
                ms.Close();
            }
            return returnStr;

        }

        public static object DeSerialize(Type type, string s)
        {
            var b = Encoding.UTF8.GetBytes(s);
            var serializer = GetSerializer(type);
            return serializer.Deserialize(new MemoryStream(b));
        }
    }
}
