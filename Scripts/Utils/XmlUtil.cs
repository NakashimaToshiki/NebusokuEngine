using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace NebusokuEngine
{
    public static class XmlUtil
    {
        /// <summary>
        /// Xmlファイルを読み込んで、デシリアライズしたクラスを返す。
        /// </summary>
        /// <param name="configPath">読み込みファイル</param>
        static public T Read<T>(string configPath)
        {
            using (StreamReader sr = new StreamReader(configPath, new UTF8Encoding(false)))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(sr);
            }
        }

        /// <summary>
        /// クラスをシリアライズして、Xmlファイルを出力する。
        /// </summary>
        /// <param name="item">書き込み対象</param>
        /// <param name="configPath">書き込みファイル</param>
        static public void Write<T>(T item, string configPath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(configPath));
            using (StreamWriter sw = new StreamWriter(configPath, false, new UTF8Encoding(false)))
            {
                new XmlSerializer(typeof(T)).Serialize(sw, item);
            }
        }
    }
}