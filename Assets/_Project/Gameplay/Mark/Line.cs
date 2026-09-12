using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [Serializable]
    public struct Line
    {
        [SerializeField] private Mark[] _marks;
        private const int MarkCountInLine = 3;

        public readonly IEnumerable<Mark> Marks => _marks;

        public readonly bool CheckCrossLine() => CheckLineCompliance(Figure.Cross);

        public readonly bool CheckNoughtLine() => CheckLineCompliance(Figure.Nought);

        private readonly bool CheckLineCompliance(Figure figure)
        {
            var markWithFigureCompliance = _marks.Where(mark => mark.State == Mark.ConvertFigureToMarkState(figure));
            return markWithFigureCompliance.Count() == MarkCountInLine;
        }
    }
}