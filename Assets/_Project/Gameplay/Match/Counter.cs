using System;
using System.IO;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Counter : MonoBehaviour
    {
        [SerializeField] private MatchSystem _matchSystem;
        [Title("Counts")]
        [LabelText("Enemy"), SerializeField] private TextMesh _enemyCount;
        [LabelText("Player"), SerializeField] private TextMesh _playerCount;

        private CounterData _data;
        private const int Point = 1;
        private const string CounterSave = "counter_save.json";

        [Title("Points")]
        [LabelText("Player"), ShowInInspector] public int PlayerPoint => LoadData().PlayerPoints;

        [LabelText("Enemy"), ShowInInspector] public int EnemyPoint => LoadData().EnemyPoints;

        private void Awake() => _data = LoadData();

        private void Start() => SetText();

        private void OnEnable() => _matchSystem.GameOver += OnGameOver;

        private void OnDisable() => _matchSystem.GameOver -= OnGameOver;

        [PropertySpace(8), EnableInEditMode, Button]
        public void ResetData()
        {
            _data = new CounterData();
            SaveData(_data);
        }

        private void OnGameOver(MatchResult matchResult)
        {
            SetPoints(matchResult);
            SetText();
            SaveData(_data);
        }

        private void SetPoints(MatchResult matchResult)
        {
            switch (matchResult)
            {
                case MatchResult.Victory: _data.PlayerPoints += Point; break;
                case MatchResult.Defeat: _data.EnemyPoints += Point; break;
            }
        }

        private void SetText()
        {
            _enemyCount.text = _data.EnemyPoints.ToString();
            _playerCount.text = _data.PlayerPoints.ToString();
        }

        private static void SaveData(CounterData data)
        {
            string json = JsonUtility.ToJson(data, true);
            string path = Path.Combine(Application.persistentDataPath, CounterSave);

            File.WriteAllText(path, json);
        }

        private static CounterData LoadData()
        {
            string path = Path.Combine(Application.persistentDataPath, CounterSave);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                CounterData data = JsonUtility.FromJson<CounterData>(json);
                return data;
            }

            return new CounterData();
        }
    }

    [Serializable]
    public class CounterData
    {
        public int PlayerPoints, EnemyPoints;
    }
}