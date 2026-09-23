using System.IO;
using UnityEngine;

namespace TicTacToe.Core
{
    public sealed class DataSaveSystem : Singleton<DataSaveSystem>
    {
        public void Save(object data, string path)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
        }

        public T Load<T>(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                T data = JsonUtility.FromJson<T>(json);

                return data;
            }

            return default;
        }

        public void Restart<T>(ref T data, string path)
        {
            data = default;
            Save(data, path);
        }

        public void Delete(string path) => File.Delete(path);
    }
}