using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace CustomNode
{
    public enum TypeMovement { HORIZONTAL, VERTICAL, FREE };
    public class Movement : Unit
    {
        //Variables para nodo
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
        private TypeMovement typeMovement;
        private float moveSpeed;
        private Rigidbody2D rigidBody2D;
        private Vector2 direction;

        protected override void Definition() //Lo que contiene el Nodo
        {
            inputTrigger = ControlInput("", (flow) =>
            {
                typeMovement = flow.GetValue<TypeMovement>(typeMovementInput);
                moveSpeed = flow.GetValue<float>(moveSpeedInput);
                rigidBody2D = flow.GetValue<Rigidbody2D>(rigidBody2DInput);
                direction = flow.GetValue<Vector2>(directionInput);

                switch (typeMovement)
                {
                    case TypeMovement.HORIZONTAL:
                        rigidBody2D.MovePosition(rigidBody2D.position + direction.normalized * Vector2.right * moveSpeed * Time.fixedDeltaTime);
                        break;
                    case TypeMovement.VERTICAL:
                        rigidBody2D.MovePosition(rigidBody2D.position + direction.normalized * Vector2.up * moveSpeed * Time.fixedDeltaTime);
                        break;
                    case TypeMovement.FREE:
                        rigidBody2D.MovePosition(rigidBody2D.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
                        break;

                    default:
                        rigidBody2D.MovePosition(rigidBody2D.position + direction.normalized * Vector2.right * moveSpeed * Time.fixedDeltaTime);
                        break;
                }
                return outputTrigger;
            });

            typeMovementInput = ValueInput("Type Movement", TypeMovement.HORIZONTAL);
            moveSpeedInput = ValueInput("Move Speed", moveSpeed);
            rigidBody2DInput = ValueInput<Rigidbody2D>("RigidBody2D");
            directionInput = ValueInput("Direction", Vector2.zero);

            outputTrigger = ControlOutput("");
        }
    }
}

