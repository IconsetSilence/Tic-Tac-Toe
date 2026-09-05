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

        private void OnEnable() => _matchSystem.NextTurn += OnNextTurn;

        private void OnDisable() => _matchSystem.NextTurn -= OnNextTurn;

        private void OnNextTurn(Turn turn) => _text.text = turn.ToString();
    }
}