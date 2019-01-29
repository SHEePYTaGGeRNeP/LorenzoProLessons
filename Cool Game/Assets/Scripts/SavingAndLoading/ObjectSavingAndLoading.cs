using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace Assets.Scripts.SavingAndLoading
{
    using UnityEngine;

    public static class SaveLoadObject
    {
        public const string DEFAULT_SAVE_FILE_NAME = "SavedData.dat";
        private static string _applicationPersistentDataPath;
        public static string ApplicationPersistentDataPath
        {
            get
            {
                if (String.IsNullOrEmpty(_applicationPersistentDataPath))
                    _applicationPersistentDataPath = Utils.PathForDocumentsFile();
                return _applicationPersistentDataPath;
            }
        }

        public static string CheckFilePath(string filePath)
        {
            if (!filePath.ToUpper().EndsWith(".DAT"))
                filePath += ".dat";
            if (filePath.StartsWith("//"))
                filePath = filePath.Remove(0, 1);
            filePath = ApplicationPersistentDataPath + "/" + filePath;
            return filePath;
        }

        /// <summary>
        /// Make sure the object contains [Serializeable]
        /// </summary>
        /// <param name="o">Make sure the object contains [Serializeable]</param>
        /// <param name="filePath">Should not include the ApplicationPersistentDatapath</param>
        public static void SaveObject<T>(T o, string filePath = "/" + DEFAULT_SAVE_FILE_NAME)
        {
            filePath = CheckFilePath(filePath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(filePath);

            bf.Serialize(fs, o);
            fs.Close();
            LogHelper.Log(typeof(SaveLoadObject), "Saved file " + filePath);
        }

        /// <summary>
        /// Make sure the object contains [Serializeable] and has an empty constructor
        /// </summary>
        /// <param name="filePath">Should not include the ApplicationPersistentDatapath</param>
        /// <returns></returns>
        public static T LoadObjectOfType<T>(string filePath = "/" + DEFAULT_SAVE_FILE_NAME)
            where T : new()
        {
            filePath = CheckFilePath(filePath);
            if (!File.Exists(filePath))
            {
                LogHelper.LogError(typeof(SaveLoadObject), "LoadObject", "File does not exist: " + filePath);
                return new T();
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(filePath, FileMode.Open);
            T objectOfType = (T)bf.Deserialize(fs);
            fs.Close();
            return objectOfType;
        }
    }

    // How to use SaveLoadObject class
    public class SaveLoadObjectTestClass
    {
        [Serializable]
        private class Hello
        {
            public string Value { private get; set; }
            public int Value2 { private get; set; }
            public override string ToString()
            {
                return this.Value + " " + this.Value2;
            }
        }

        public static void Test()
        {
            Hello h = new Hello { Value = "hello world", Value2 = 200 };
            SaveLoadObject.SaveObject(h, "classTest");
            Thread.Sleep(300);
            Hello readH = SaveLoadObject.LoadObjectOfType<Hello>("classTest");
            LogHelper.Log(typeof(SaveLoadObjectTestClass), "class = " + readH);
            File.Delete(SaveLoadObject.CheckFilePath("classTest.dat"));
        }
    }
}
