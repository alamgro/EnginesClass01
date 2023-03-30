using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace CustomNode
{
    public class JumpNode : Unit
    {
        //Node variables
        [DoNotSerialize]
        public ControlInput inputTrigger;
        [DoNotSerialize]
        public ControlOutput outputTrigger;
        [DoNotSerialize]
        public ValueInput rigidBody2DInput;
        [DoNotSerialize]
        public ValueInput jumpForceInput;
        [DoNotSerialize]
        public ValueInput layerMaskInput;

        private Rigidbody2D _rigidBody2D;
        private float _jumpForce;
        private ContactFilter2D _contactFilter;
        private LayerMask _layerMask;
        
        protected override void Definition() //Lo que contiene el Nodo
        {
            inputTrigger = ControlInput("", (flow) =>
            {
                _rigidBody2D = flow.GetValue<Rigidbody2D>(rigidBody2DInput);
                _jumpForce = flow.GetValue<float>(jumpForceInput);

                SetupContactFilter();

                Collider2D[] hits = new Collider2D[1];
                int hitCount = _rigidBody2D.GetContacts(_contactFilter, hits);

                if (hitCount > 0)
                {
                    _rigidBody2D.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);
                }

                return outputTrigger;
            });

            jumpForceInput = ValueInput("Move Speed", _jumpForce);
            rigidBody2DInput = ValueInput<Rigidbody2D>("Rigidbody2D");
            layerMaskInput = ValueInput("Layer Mask", _layerMask);
            outputTrigger = ControlOutput("");
        }

        private void SetupContactFilter()
        {
            _contactFilter = new ContactFilter2D();
            _contactFilter.useLayerMask = true;
            _contactFilter.layerMask = LayerMask.GetMask("Ground");
            _contactFilter.useDepth = false;
            _contactFilter.useNormalAngle = false;
        }
    }
}

