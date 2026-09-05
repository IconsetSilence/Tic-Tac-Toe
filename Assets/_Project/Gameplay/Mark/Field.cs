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

        private Mark[] _marks;

        public MatchSystem MatchSystem => _matchSystem;

        public IEnumerable<Mark> Marks => _marks;

        private void Awake() => _marks = GetComponentsInChildren<Mark>(false);

        [PropertySpace(8), Button]
        public void SetFigureDifinitionsOnClick(Figure figure)
        {
            var freeMarks = _marks.Where(mark => mark.Free).Select(mark => mark);

            foreach (var mark in freeMarks)
                mark.FigureDefinitionOnClick = figure;
        }
    }
}