using System;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Mark : MonoBehaviour
    {
        public event Action<MarkState> ChangeState;
        private MarkState _state;

        [ShowInInspector]
        public MarkState State
        {
            get => _state;
            set
            {
                _state = value;
                ChangeState?.Invoke(value);
            }
        }

        [ShowInInspector]
        public bool Free => State == MarkState.Empty;

        public static MarkState ConvertFigureToMarkState(Figure figure) => figure switch
        {
            Figure.Cross => MarkState.Cross,
            Figure.Nought => MarkState.Nought,
            _ => throw new ArgumentException()
        };

        public static Figure ConvertMarkStateToFigure(MarkState state) => state switch
        {
            MarkState.Cross => Figure.Cross,
            MarkState.Nought => Figure.Nought,
            _ => throw new ArgumentException("\"Figure\" doesn`t have an empty state")
        };
    }

    public enum MarkState { Empty, Cross, Nought }

    public enum Figure { Cross, Nought }
}