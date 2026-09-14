using Cysharp.Threading.Tasks;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Field _field;
        [SerializeField] private MatchSystem _matchSystem;

        [Title("Turn Time")]
        [Min(0), SerializeField] private float _minTurnTime = 3f;
        [Min(0), SerializeField] private float _maxTurnTime = 7.5f;

        private void OnEnable() => _matchSystem.NextTurn += OnNextTurn;

        private void OnDisable() => _matchSystem.NextTurn -= OnNextTurn;

        private void OnNextTurn(Turn turn)
        {
            if (turn == Turn.Enemy)
                MakeTurn().Forget();
        }

        private async UniTask MakeTurn()
        {
            var freeMarks = _field.Marks.Where(mark => mark.Free);
            var randomMarkIndex = Random.Range(0, freeMarks.Count());
            var randomMark = freeMarks.ElementAt(randomMarkIndex);
            var turnTime = Random.Range(_minTurnTime, _maxTurnTime);

            await UniTask.WaitForSeconds(turnTime, cancellationToken: destroyCancellationToken);

            if (_matchSystem.MatchActive)
            {
                DrawSome(randomMark);
                _matchSystem.EndTurn();
            }
        }

        private void DrawSome(Mark mark) => mark.State = MarkState.Nought;
    }
}