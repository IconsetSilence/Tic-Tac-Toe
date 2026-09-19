using System.IO;
using UnityEngine;

namespace TicTacToe.Data
{
    public static class SaveSystem
    {
        public static void Save(object data, string path)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public static T Load<T>(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                T data = JsonUtility.FromJson<T>(json);

                return data;
            }

            return default;
        }

        public static void Reset<T>(string path)
        {
            T data = default;
            Save(data, path);
        }
    }
}