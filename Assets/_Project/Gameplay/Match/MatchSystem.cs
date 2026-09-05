using System;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class MatchSystem : MonoBehaviour
    {
        public event Action<Turn> NextTurn;

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
            CurrentTurn = GetTurnOpposite(CurrentTurn);
            NextTurn?.Invoke(CurrentTurn);
        }

        private Turn GetTurnOpposite(Turn turn) => turn == Turn.Player ? Turn.Enemy : Turn.Player;

        private Turn GetRandomTurn()
        {
            int num = UnityEngine.Random.Range(0, 2);
            return num == 1 ? Turn.Player : Turn.Enemy;
        }
    }

    public enum Turn
    {
        Player,
        Enemy
    }
}