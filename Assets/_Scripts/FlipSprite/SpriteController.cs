
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FlipSprite
{
    [Serializable]
    public class SpriteController : ISpriteController
    {
        [SerializeReference]
        private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SprRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
        public bool IsFlippedOnX { get => SprRenderer.flipX; set => SprRenderer.flipX = value; }

        public void FlipX(bool flip) => _spriteRenderer.flipX = flip;
    }
}
