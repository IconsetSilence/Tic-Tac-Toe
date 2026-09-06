using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Field : MonoBehaviour
    {
        [SerializeField] private MatchSystem _matchSystem;
        [Title("Lines")]
        [LabelText("Horizontal"), SerializeField] private Line[] _horizontalLines;
        [LabelText("Vertical"), SerializeField] private Line[] _verticalLines;
        [LabelText("Diagonal"), SerializeField] private Line[] _diagonalLines;

        private Mark[] _marks;
        private const int MaxMarkCount = 9;

        public MatchSystem MatchSystem => _matchSystem;

        public IEnumerable<Mark> Marks => _marks;

        public IEnumerable<Line> Lines { get; private set; }

        private void Awake()
        {
            _marks = GetComponentsInChildren<Mark>(false);
            Lines = _horizontalLines.Concat(_verticalLines).Concat(_diagonalLines);
        }

        [PropertySpace(8), Button]
        public void SetFigureDifinitionsOnClick(Figure figure)
        {
            var freeMarks = _marks.Where(mark => mark.Free);

            foreach (var mark in freeMarks)
                mark.FigureDefinitionOnClick = figure;
        }

        public bool NoFreeMarks()
        {
            var noFreeMarks = _marks.Where(mark => mark.State != MarkState.Empty);
            return noFreeMarks.Count() == MaxMarkCount;
        }
    }
}