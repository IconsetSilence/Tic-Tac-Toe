using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    [RequireComponent(typeof(Camera))]
    public class MouseClickHandler : MonoBehaviour
    {
        [SerializeField] private MatchSystem _matchSystem;
        [SerializeField] private bool _canClickOnMark = false;
        private Camera _camera;

        private void Awake() => _camera = GetComponent<Camera>();

        private void Update()
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                CheckMark();
        }

        private void OnEnable() => _matchSystem.NextTurn += OnNextTurn;

        private void OnDisable() => _matchSystem.NextTurn -= OnNextTurn;

        private void OnNextTurn(Turn turn)
        {
            if (turn == Turn.Player)
                _canClickOnMark = true;
        }

        private void CheckMark()
        {
            if (_canClickOnMark)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Ray ray= _camera.ScreenPointToRay(mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider != null && hit.collider.TryGetComponent(out Mark mark))
                {
                    mark.State = MarkState.Cross;
                    _canClickOnMark = false;
                    _matchSystem.EndTurn();
                }
            }
        }
    }
}