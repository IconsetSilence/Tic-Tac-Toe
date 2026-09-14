using System;
using System.IO;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Counter : MonoBehaviour
    {
        [SerializeField] private CountData _data;

        private const int Point = 1;
        private static string DataPath => Path.Combine(Application.persistentDataPath, "count_save.json");

        [PropertySpace(16), Title("Data")]
        [Button(Name = "Save")]
        public void SaveData()
        {
            string json = JsonUtility.ToJson(_data, true);
            File.WriteAllText(DataPath, json);
        }

        [Button(Name = "Load")]
        public CountData LoadData()
        {
            if (File.Exists(DataPath))
            {
                string json = File.ReadAllText(DataPath);
                var data = JsonUtility.FromJson<CountData>(json);

                if (Application.isEditor)
                    _data = data;

                return data;
            }

            throw new FileNotFoundException("No this save file");
        }

        [Button(Name = "Reset")]
        public void ResetData() => _data = new CountData();

        [PropertySpace(16), Title("Points")]
        [Button]
        public void AddPlayerPoint() => _data.PlayerPoints += Point;

        [Button]
        public void AddEnemyPoint() => _data.EnemyPoints += Point;
    }

    [Serializable]
    public struct CountData
    {
        public int PlayerPoints, EnemyPoints;
    }
}