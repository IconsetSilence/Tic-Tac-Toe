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

        private void Start() => StartNewMatch();

        public void StartNewMatch()
        {
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

            await UniTask.WaitForSeconds(_newMatchInSeconds);

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
            if (_field.NoFreeMarks())
            {
                result = MatchResult.Draw;
                return true;
            }

            foreach (var line in _field.Lines)
            {
                if (line.CheckCrossLine())
                {
                    result = MatchResult.Victory;
                    return true;
                }
                else if (line.CheckNoughtLine())
                {
                    result = MatchResult.Defeat;
                    return true;
                }
            }

            result = MatchResult.None;
            return false;
        }
    }

    public enum Turn
    {
        Player,
        Enemy
    }

    public enum MatchResult
    {
        None,
        Victory,
        Defeat,
        Draw
    }
}