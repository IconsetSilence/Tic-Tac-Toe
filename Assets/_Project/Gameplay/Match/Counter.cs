using System;
using System.IO;
using TicTacToe.Core;
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

        private void Awake() => LoadData();

        private void OnEnable() => _matchSystem.GameOver += OnGameOver;

        private void OnDisable() => _matchSystem.GameOver -= OnGameOver;

        private void Start() => UpdateCount?.Invoke(_data);

        [PropertySpace(16), Title("Data")]
        [Button("Save")]
        private void SaveData() => DataSaveSystem.Instance.Save(_data, DataPath);

        [Button("Load")]
        private void LoadData() => _data = DataSaveSystem.Instance.Load<CountData>(DataPath);

        [Button("Reset")]
        private void ResetData()
        {
            DataSaveSystem.Instance.Restart(ref _data, DataPath);
            UpdateCount?.Invoke(_data);
        }

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