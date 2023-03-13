using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlipSprite
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlipSprite : MonoBehaviour
    {
        [SerializeReference]
        private ISpriteController _spriteController;

        public FlipSprite()
        {
            Inject(new SpriteController());
        }

        private void Awake()
        {
            _spriteController.SprRenderer = GetComponent<SpriteRenderer>();
        }

        public void Inject(ISpriteController spriteController) => _spriteController = spriteController;
        public void FlipSpriteOnMoveX(InputAction.CallbackContext ctx) => _spriteController.IsFlippedOnX = (
            !(ctx.ReadValue<Vector2>().x > 0f) && (ctx.ReadValue<Vector2>().x < 0f || _spriteController.SprRenderer.flipX));

        //public void FlipX(InputAction.CallbackContext ctx) => _spriteRenderer.flipX =
        //    !(ctx.ReadValue<Vector2>().x > 0f) && (ctx.ReadValue<Vector2>().x  < 0f || _spriteRenderer.flipX);
    }
}

