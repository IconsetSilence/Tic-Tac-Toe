using System;
using TicTacToe.Data;
using TicTacToe.Gameplay;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace TicTacToe.UI
{
    [HideMonoScript]
    [RequireComponent(typeof(UIDocument))]
    public class MatchUI : MonoBehaviour
    {
        [SerializeField] private MatchSystem _matchSystem;
        [SerializeField] private Counter _counter;
        [Scene, SerializeField] private string _mainMenuScene;
        [Title("Text Color In Turn")]
        [LabelText("Player"), SerializeField] private Color _playerTurnColor = Color.red;
        [LabelText("Enemy"), SerializeField] private Color _enemyTurnColor = Color.blue;
        [LabelText("Default"), SerializeField] private Color _defaultTurnColor = Color.white;
        private VisualElement _root;
        private Label _playerCountLabel, _enemyCountLabel, _turnAnyoneLabel;
        private Button _quitButton;
        private const int MinPoints = 0, MaxPoints = 99;
        private const string PlayerCountName = "player-count", EnemyCountName = "enemy-count", TurnAnyoneName = "turn-anyone", QuitButtonName = "quit-button";

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;

            _playerCountLabel = _root.Q<Label>(PlayerCountName);
            _enemyCountLabel = _root.Q<Label>(EnemyCountName);
            _turnAnyoneLabel = _root.Q<Label>(TurnAnyoneName);
            _quitButton = _root.Q<Button>(QuitButtonName);

            _matchSystem.NextTurn += SetTurn;
            _counter.UpdateCount += OnUpdateCount;
            _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClick);
        }

        private void OnDisable()
        {
            _matchSystem.NextTurn -= SetTurn;
            _counter.UpdateCount -= OnUpdateCount;
            _quitButton.UnregisterCallback<ClickEvent>(OnQuitButtonClick);
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

        private void OnQuitButtonClick(ClickEvent clickEvent) => SceneManager.LoadScene(_mainMenuScene);

        private void OnUpdateCount(CountData count)
        {
            SetPlayerPoint(count.PlayerPoints);
            SetEnemyPoint(count.EnemyPoints);
        }

        private static void SetPoints(Label countLabel, int value)
        {
            if (value < MinPoints || value > MaxPoints)
                throw new ArgumentOutOfRangeException("Points\' min is 0 & max is 99");

            countLabel.text = value.ToString();
        }
    }
}