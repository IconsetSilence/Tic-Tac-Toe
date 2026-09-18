using Cysharp.Threading.Tasks;
using System;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class MatchSystem : MonoBehaviour
    {
        public event Action<MatchResult> GameOver;
        public event Action<Turn> NextTurn;
        public event Action NewMatch;

        [SerializeField] private Field _field;
        [Min(0), SerializeField] private float _newMatchInSeconds = 5f;
        [ReadOnly, SerializeField] private MatchResult _result;

        [ReadOnly, ShowInInspector] public Turn CurrentTurn { get; private set; }

        public MatchResult Result => _result;

        public bool MatchActive => _result == MatchResult.None;

        private void Start() => StartNewMatch();

        public void StartNewMatch()
        {
            _result = MatchResult.None;
            NewMatch?.Invoke();

            var firstTurn = GetRandomTurn();

            CurrentTurn = firstTurn;
            NextTurn?.Invoke(firstTurn);
        }

        public void EndTurn()
        {
            if (TryGetMatchResult(out _result))
                EndMatch().Forget();
            else
            {
                CurrentTurn = GetTurnOpposite(CurrentTurn);
                NextTurn?.Invoke(CurrentTurn);
            }
        }

        private async UniTask EndMatch()
        {
            GameOver?.Invoke(_result);

            await UniTask.WaitForSeconds(_newMatchInSeconds, cancellationToken: destroyCancellationToken);

            StartNewMatch();
        }

        private Turn GetTurnOpposite(Turn turn) => turn == Turn.Player ? Turn.Enemy : Turn.Player;

        private Turn GetRandomTurn()
        {
            int num = UnityEngine.Random.Range(0, 2);
            return num == 1 ? Turn.Player : Turn.Enemy;
        }

        private bool TryGetMatchResult(out MatchResult result)
        {            
            foreach (var line in _field.Lines)
            {
                if (line.CheckLineCompliance(Figure.Cross))
                {
                    result = MatchResult.Victory;
                    return true;
                }
                else if (line.CheckLineCompliance(Figure.Nought))
                {
                    result = MatchResult.Defeat;
                    return true;
                }
            }

            if (_field.NoFreeMarks())
            {
                result = MatchResult.Draw;
                return true;
            }

            result = MatchResult.None;
            return false;
        }
    }

    public enum MatchResult
    {
        None,
        Victory,
        Defeat,
        Draw
    }
}