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

        private MatchSystem _matchSystem;
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

        [ShowInInspector] public Figure FigureDefinitionOnClick { get; set; } = Figure.Cross;

        public bool Free => _state == MarkState.Empty;

        private void Awake() => _matchSystem = GetComponentInParent<Field>().MatchSystem;

        [PropertySpace(8), Button]
        public void DrawCross() => State = MarkState.Cross;

        [Button]
        public void DrawNought() => State = MarkState.Nought;

        [Button]
        public void Clear() => State = MarkState.Empty;

        public static Figure ConvertMarkStateToFigure(MarkState markState)
        {
            return markState switch
            {
                MarkState.Cross => Figure.Cross,
                MarkState.Nought => Figure.Nought,
                _ => throw new ArgumentException($"Cannot {MarkState.Empty} be converted to type {nameof(Figure)}")
            };
        }

        public static MarkState ConvertFigureToMarkState(Figure figure)
        {
            return figure switch
            {
                Figure.Cross => MarkState.Cross,
                Figure.Nought => MarkState.Nought,
                _ => default
            };
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Free && _matchSystem.CurrentTurn == Turn.Player)
            {
                State = ConvertFigureToMarkState(FigureDefinitionOnClick);
                _matchSystem.EndTurn();
            }
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

    public enum Figure
    {
        Cross,
        Nought
    }
}