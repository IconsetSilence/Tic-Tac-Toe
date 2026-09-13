using System;
using TicTacToe.Gameplay;
using TriInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.UI
{
    [HideMonoScript]
    [RequireComponent(typeof(UIDocument))]
    public class MatchUI : MonoBehaviour
    {
        [Title("Text Color In Turn")]
        [LabelText("Player"), SerializeField] private Color _playerTurnColor = Color.red;
        [LabelText("Enemy"), SerializeField] private Color _enemyTurnColor = Color.blue;
        [LabelText("Default"), SerializeField] private Color _defaultTurnColor = Color.white;
        private VisualElement _root;
        private Label _playerCountLabel, _enemyCountLabel, _turnAnyoneLabel;
        private const int MinPoints = 0, MaxPoints = 99;
        private const string PlayerCountName = "player-count", EnemyCountName = "enemy-count", TurnAnyoneName = "turn-anyone";

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _playerCountLabel = _root.Q<Label>(PlayerCountName);
            _enemyCountLabel = _root.Q<Label>(EnemyCountName);
            _turnAnyoneLabel = _root.Q<Label>(TurnAnyoneName);
        }

        [PropertySpace(16)]
        [EnableInPlayMode, Button]
        public void SetPlayerPoint(int value) => SetPoints(_playerCountLabel, value);

        [EnableInPlayMode, Button]
        public void SetEnemyPoint(int value) => SetPoints(_enemyCountLabel, value);

        [EnableInPlayMode, Button]
        public void SetTurn(Turn turn)
        {
            var textColorResult = new StyleColor(turn switch
            {
                Turn.Player => _playerTurnColor,
                Turn.Enemy => _enemyTurnColor,
                _ => _defaultTurnColor
            });

            _turnAnyoneLabel.text = turn.ToString();
            _turnAnyoneLabel.style.color = textColorResult;
            _turnAnyoneLabel.style.unityTextOutlineColor = textColorResult;
        }

        private static void SetPoints(Label countLabel, int value)
        {
            if (value < MinPoints || value > MaxPoints)
                throw new ArgumentOutOfRangeException("Points\' min is 0 & max is 99");

            countLabel.text = value.ToString();
        }
    }
}