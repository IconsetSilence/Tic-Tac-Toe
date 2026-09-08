using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    [RequireComponent(typeof(TextMesh))]
    public class TurnText : MonoBehaviour
    {
        [SerializeField] private MatchSystem _matchSystem;

        private TextMesh _text;

        private void Awake() => _text = GetComponent<TextMesh>();

        private void OnEnable()
        {
            _matchSystem.NextTurn += OnNextTurn;
            _matchSystem.GameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _matchSystem.NextTurn -= OnNextTurn;
            _matchSystem.GameOver -= OnGameOver;
        }

        private void OnNextTurn(Turn turn) => _text.text = turn.ToString();

        private void OnGameOver(MatchResult matchResult) => _text.text = matchResult.ToString();
    }
}