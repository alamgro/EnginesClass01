using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace CustomNode
{
    public class FlipSpriteNode : Unit
    {
        [DoNotSerialize]
        public ControlInput inputTrigger;
        [DoNotSerialize]
        public ControlOutput outputTrigger;
        [DoNotSerialize]
        public ValueInput spriteRendererInput;
        [DoNotSerialize]
        public ValueInput directionInput;

        //Variables del tipo 
        private Vector2 _direction;
        private SpriteRenderer _spriteRenderer;

        protected override void Definition() //Lo que contiene el Nodo
        {
            inputTrigger = ControlInput("", (flow) =>
            {
                _direction = flow.GetValue<Vector2>(directionInput);
                _spriteRenderer = flow.GetValue<SpriteRenderer>(spriteRendererInput);

                _spriteRenderer.flipX = _direction.x < 0;

                return outputTrigger;
            });

            directionInput = ValueInput("Direction", Vector2.zero);
            spriteRendererInput = ValueInput<SpriteRenderer>("Sprite Renderer");

            outputTrigger = ControlOutput("");
        }
    }
}

