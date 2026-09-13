using System;
using TriInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.UI
{
    [HideMonoScript]
    [RequireComponent(typeof(UIDocument))]
    public class MatchUI : MonoBehaviour
    {
        private VisualElement _root;
        private Label _playerCountLabel, _enemyCountLabel;
        private const int MinPoints = 0, MaxPoints = 99;
        private const string PlayerCountName = "player-count", EnemyCountName = "enemy-count";

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _playerCountLabel = _root.Q<Label>(PlayerCountName);
            _enemyCountLabel = _root.Q<Label>(EnemyCountName);
        }

        [EnableInPlayMode, Button]
        public void SetPlayerPoint(int value) => SetPoints(_playerCountLabel, value);

        [EnableInPlayMode, Button]
        public void SetEnemyPoint(int value) => SetPoints(_enemyCountLabel, value);

        private static void SetPoints(Label countLabel, int value)
        {
            if (value < MinPoints || value > MaxPoints)
                throw new ArgumentOutOfRangeException("Points\' min is 0 & max is 99");

            countLabel.text = value.ToString();
        }
    }
}