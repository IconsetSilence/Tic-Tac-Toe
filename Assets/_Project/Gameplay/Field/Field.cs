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
        [SerializeField] private GameObject _marksContainer;
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
            _marks = _marksContainer.GetComponentsInChildren<Mark>();
            Lines = _horizontalLines.Concat(_verticalLines).Concat(_diagonalLines);
        }

        private void OnEnable() => _matchSystem.NewMatch += Clear;

        private void OnDisable() => _matchSystem.NewMatch -= Clear;

        public bool NoFreeMarks()
        {
            var noFreeMarks = _marks.Where(mark => mark.State != MarkState.Empty);
            return noFreeMarks.Count() == MaxMarkCount;
        }

        [PropertySpace(16)]
        [EnableInPlayMode, Button]
        public void Clear()
        {
            var freeMarks = _marks.Where(mark => mark.Free == false);

            foreach (var mark in freeMarks)
                mark.State = MarkState.Empty;
        }
    }
}