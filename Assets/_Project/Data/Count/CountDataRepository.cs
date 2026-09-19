using System.IO;
using UnityEngine;

namespace TicTacToe.Data
{
    public class CountDataRepository
    {
        private static readonly string SavePath = Path.Combine(Application.persistentDataPath, "count_save.json");

        public void Save(object data) => SaveSystem.Save(data, SavePath);

        public CountData Load() => SaveSystem.Load<CountData>(SavePath);

        public void Reset() => SaveSystem.Reset<CountData>(SavePath);
    }
}