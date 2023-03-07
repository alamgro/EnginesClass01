using UnityEngine;
using UnityEngine.InputSystem;

namespace FlipSprite
{
    public interface ISpriteController
    {
        public SpriteRenderer SprRenderer { get; set; }
        public bool IsFlippedOnX { get; set; }

    }
}
