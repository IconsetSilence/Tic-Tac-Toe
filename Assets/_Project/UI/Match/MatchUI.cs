using TriInspector;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.UI
{
    [HideMonoScript]
    [RequireComponent(typeof(UIDocument))]
    public class MatchUI : MonoBehaviour
    {
        private VisualElement _root;

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;


        }
    }
}