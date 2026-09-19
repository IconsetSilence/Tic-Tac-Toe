using System;
using TicTacToe.Data;
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
        private CountDataRepository _repository;

        private const int Point = 1;

        private void Awake()
        {
            _repository = new CountDataRepository();
            LoadData();
        }

        private void OnEnable() => _matchSystem.GameOver += OnGameOver;

        private void OnDisable() => _matchSystem.GameOver -= OnGameOver;

        private void Start() => UpdateCount?.Invoke(_data);

        [PropertySpace(16), Title("Data")]
        [Button("Save")]
        private void SaveData() => _repository.Save(_data);

        [Button("Load")]
        private void LoadData() => _data = _repository.Load();

        [Button("Reset")]
        private void ResetData() => _repository.Reset();

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