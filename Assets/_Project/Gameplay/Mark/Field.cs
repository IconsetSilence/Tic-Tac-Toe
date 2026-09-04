using System.Linq;
using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    public class Field : MonoBehaviour
    {
        private Mark[] _marks;

        private void Awake() => _marks = GetComponentsInChildren<Mark>(false);

        [Button]
        public void SetFigureDifinitionsOnClick(MarkState state = MarkState.Cross)
        {
            if (state == MarkState.Empty)
                throw new UnityException("In Definition, the player cannot select an empty mark");

            var freeMarks = _marks.Where(mark => mark.Free).Select(mark => mark);

            foreach (var mark in freeMarks)
                mark.FigureDefinitionOnClick = state;
        }
    }
}