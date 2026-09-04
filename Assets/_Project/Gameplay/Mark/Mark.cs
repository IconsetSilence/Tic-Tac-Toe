using System;
using TriInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Mark : MonoBehaviour, IPointerClickHandler
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
                Render();
            }
        }

        [ShowInInspector] public MarkState FigureDefinitionOnClick { get; set; } = MarkState.Cross;

        public bool Free => _state == MarkState.Empty;

        [PropertySpace(8), Button]
        public void DrawCross() => State = MarkState.Cross;

        [Button]
        public void DrawNought() => State = MarkState.Nought;

        [Button]
        public void Clear() => State = MarkState.Empty;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (FigureDefinitionOnClick == MarkState.Empty)
                throw new UnityException("In Definition, the player cannot select an empty mark");
            else if (Free)
                State = FigureDefinitionOnClick;
        }

        private void Render()
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