using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlipSprite : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    public void FlipX(InputAction.CallbackContext ctx) => _spriteRenderer.flipX = !(ctx.ReadValue<Vector2>().x > 0f) && (ctx.ReadValue<Vector2>().x < 0f || _spriteRenderer.flipX);
}
