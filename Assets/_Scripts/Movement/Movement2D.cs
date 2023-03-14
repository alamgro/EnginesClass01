using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

namespace Movement
{
    public class Movement2D : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 1f;
        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField]
        private UnityEvent OnMovementStarted;
        [SerializeField]
        private UnityEvent OnMovementStopped;

        private IEnumerator _movementRoutine;
        private bool _isMoving = false;
        private Vector2 _direction;


        void Start()
        {
            _playerInput.actions["Axis2D"].performed += _ =>
            {
                if (_direction.magnitude == 0f || _isMoving) return;

                _isMoving = true;
                OnMovementStarted?.Invoke();
            };
            _playerInput.actions["Axis2D"].canceled += _ =>
            {
                _isMoving = false;
                OnMovementStopped?.Invoke();
            };
        }

        public void StartMoving()
        {
            _movementRoutine = MovementRoutine();
            StartCoroutine(_movementRoutine);
        }

        public void StopMoving()
        {
            StopCoroutine(_movementRoutine);
            _movementRoutine = null;
        }

        private IEnumerator MovementRoutine()
        {
            while (true)
            {
                Move();
                yield return null;
            }
        }

        public void SetDirection(InputAction.CallbackContext ctx) => _direction = ctx.ReadValue<Vector2>();
        public void Move() => transform.Translate(_moveSpeed * Time.deltaTime * _direction.x * Vector3.right);
    }
}