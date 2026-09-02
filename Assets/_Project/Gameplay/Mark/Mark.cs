using System;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Mark : MonoBehaviour
    {
        [Title("Figures")]
        [LabelText("Cross"), SerializeField] private GameObject _crossFigure;
        [LabelText("Nought"), SerializeField] private GameObject _noughtFigure;

        private MarkState _state = MarkState.Empty;

        [Title("Definition")]
        [LabelText("Current State"), ReadOnly, ShowInInspector] public MarkState State
        {
            get => _state;
            set
            {
                if (_crossFigure == null || _noughtFigure == null)
                    throw new NullReferenceException("Set figure`s game objects");

                _state = value;
                SetStateRender();
            }
        }

        public bool Free => _state == MarkState.Empty;
        
        [PropertySpace(8), Button]
        public void DrawCross() => State = MarkState.Cross;

        [Button]
        public void DrawNought() => State = MarkState.Nought;

        [Button]
        public void Clear() => State = MarkState.Empty;

        private void SetStateRender()
        {
            _crossFigure.SetActive(_state == MarkState.Cross);
            _noughtFigure.SetActive(_state == MarkState.Nought);
        }
    }

    public enum MarkState
    {
        Empty,
        Cross,
        Nought
    }
}