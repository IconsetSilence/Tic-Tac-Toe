using System;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class MatchSystem : MonoBehaviour
    {
        public event Action GameOver;
        public event Action<Turn> NextTurn;

        [SerializeField] private Field _field;
        [ReadOnly, ShowInInspector] public Turn CurrentTurn { get; private set; }

        private void Start() => Begin();

        public void Begin()
        {
            var firstTurn = GetRandomTurn();

            CurrentTurn = firstTurn;
            NextTurn?.Invoke(firstTurn);
        }

        public void EndTurn()
        {
            if (IsGameOver())
                GameOver?.Invoke();
            else
            {
                CurrentTurn = GetTurnOpposite(CurrentTurn);
                NextTurn?.Invoke(CurrentTurn);
            }
        }

        private Turn GetTurnOpposite(Turn turn) => turn == Turn.Player ? Turn.Enemy : Turn.Player;

        private Turn GetRandomTurn()
        {
            int num = UnityEngine.Random.Range(0, 2);
            return num == 1 ? Turn.Player : Turn.Enemy;
        }

        private bool IsGameOver()
        {
            if (_field.NoFreeMarks())
                return true;

            foreach (var line in _field.Lines)
                if (line.CheckCrossLine() || line.CheckNoughtLine())
                    return true;

            return false;
        }
    }

    public enum Turn
    {
        Player,
        Enemy
    }
}