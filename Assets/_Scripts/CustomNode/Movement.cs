using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace CustomNode
{
    public enum TypeMovement { HORIZONTAL, VERTICAL, FREE };
    public class Movement : Unit
    {
        //Node variables
        [DoNotSerialize]
        public ControlInput inputTrigger;

        [DoNotSerialize]
        public ControlOutput outputTrigger;

        [DoNotSerialize]
        public ValueInput typeMovementInput;

        [DoNotSerialize]
        public ValueInput rigidBody2DInput;

        [DoNotSerialize]
        public ValueInput moveSpeedInput;

        [DoNotSerialize]
        public ValueInput directionInput;

        //Variables del tipo 
        private TypeMovement _typeMovement;
        private float _moveSpeed;
        private Rigidbody2D _rigidBody2D;
        private Vector2 _direction;

        protected override void Definition() //Lo que contiene el Nodo
        {
            inputTrigger = ControlInput("", (flow) =>
            {
                _typeMovement = flow.GetValue<TypeMovement>(typeMovementInput);
                _moveSpeed = flow.GetValue<float>(moveSpeedInput);
                _rigidBody2D = flow.GetValue<Rigidbody2D>(rigidBody2DInput);
                _direction = flow.GetValue<Vector2>(directionInput);

                switch (_typeMovement)
                {
                    case TypeMovement.HORIZONTAL:
                        _rigidBody2D.MovePosition(_rigidBody2D.position + _moveSpeed * Time.fixedDeltaTime * _direction.normalized * Vector2.right);
                        break;
                    case TypeMovement.VERTICAL:
                        _rigidBody2D.MovePosition(_rigidBody2D.position + _moveSpeed * Time.fixedDeltaTime * _direction.normalized * Vector2.up);
                        break;
                    case TypeMovement.FREE:
                        _rigidBody2D.MovePosition(_rigidBody2D.position + _moveSpeed * Time.fixedDeltaTime * _direction.normalized);
                        break;

                    default:
                        _rigidBody2D.MovePosition(_rigidBody2D.position + _moveSpeed * Time.fixedDeltaTime * _direction.normalized * Vector2.right);
                        break;
                }
                return outputTrigger;
            });

            typeMovementInput = ValueInput("Type Movement", TypeMovement.HORIZONTAL);
            moveSpeedInput = ValueInput("Move Speed", _moveSpeed);
            rigidBody2DInput = ValueInput<Rigidbody2D>("RigidBody2D");
            directionInput = ValueInput("Direction", Vector2.zero);

            outputTrigger = ControlOutput("");
        }
    }
}

