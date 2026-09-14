using System;
using System.IO;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Counter : MonoBehaviour
    {
        public event Action<CountData> UpdateCount;

        [SerializeField] private MatchSystem _matchSystem;
        [SerializeField] private CountData _data;

        private const int Point = 1;
        private static string DataPath => Path.Combine(Application.persistentDataPath, "count_save.json");

        private void Awake() => _data = LoadData();

        private void OnEnable() => _matchSystem.GameOver += OnGameOver;

        private void OnDisable() => _matchSystem.GameOver -= OnGameOver;

        private void Start() => UpdateCount?.Invoke(_data);

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

            return default;
        }

        [Button(Name = "Reset")]
        public void ResetData() => _data = new CountData();

        [PropertySpace(16), Title("Points")]
        [Button]
        public void AddPlayerPoint() => _data.PlayerPoints += Point;

        [Button]
        public void AddEnemyPoint() => _data.EnemyPoints += Point;

        private void OnGameOver(MatchResult result)
        {
            switch (result)
            {
                case MatchResult.Victory: AddPlayerPoint(); break;
                case MatchResult.Defeat: AddEnemyPoint(); break;
            }

            UpdateCount?.Invoke(_data);
            SaveData();
        }
    }
}