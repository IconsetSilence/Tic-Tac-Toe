using TriInspector;
using UnityEngine;

namespace TicTacToe.Gameplay
{
    [HideMonoScript]
    [RequireComponent(typeof(SpriteRenderer))]
    public class MarkView : MonoBehaviour
    {
        [SerializeField] private Mark _mark;

        [Title("Sprites")]
        [LabelText("Cross"), SerializeField] private Sprite _crossSprite;
        [LabelText("Nought"), SerializeField] private Sprite _noughtSprite;

        [Title("Sprite Size")]
        [LabelText("Width"), SerializeField] private float _spriteWidth = 1.75f;
        [LabelText("Height"), SerializeField] private float _spriteHeight = 1.75f;

        private SpriteRenderer _renderer;

        private void Awake() => _renderer = GetComponent<SpriteRenderer>();

        private void OnEnable() => _mark.ChangeState += OnChangeState;

        private void OnDisable() => _mark.ChangeState -= OnChangeState;

        private void OnChangeState(MarkState state)
        {
            _renderer.sprite = state switch
            {
                MarkState.Cross => _crossSprite,
                MarkState.Nought => _noughtSprite,
                _ => default
            };
            _renderer.size = new Vector2(_spriteWidth, _spriteHeight);
        }
    }
}