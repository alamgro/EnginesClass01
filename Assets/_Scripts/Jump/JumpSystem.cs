using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Jump
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpSystem : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField, Range(0f, 10f)]
        private float _jumpForce = 1f;
        [SerializeReference]
        private IGroundingChecker _groundingChecker;
        [Header("Events")]
        [SerializeField]
        private UnityEvent OnJumped;
        [SerializeField]
        private UnityEvent<float> OnFell;
        [SerializeField]
        private UnityEvent OnJumpStopped;

        // Routine while it stays on air
        private IEnumerator _jumpRoutine;
        private IAirController _airController;

        private async void GroundTouchedFirstTime(Action<string> onCompleted)
        {
            var message = "Completed";

            Debug.Log("Loading...");
            await Task.Delay(200);

            try
            {
                onCompleted?.Invoke(message);
            }
            catch (Exception e)
            {
                message = "[Error]: Action not completed";
                throw;
            }
        }

        private void Start()
        {
            GroundTouchedFirstTime(message => 
            {
                Debug.Log(message);
            });
        }

        public JumpSystem()
        {
            Inject(new AirController());
            Inject(new GroundingChecker());
        }
        public void Inject(IAirController airController) => _airController = airController;
        public void Inject(IGroundingChecker groundingChecker) => _groundingChecker = groundingChecker;

        private void Awake()
        {
            //_groundingChecker.GroundLayer = LayerMask.NameToLayer("Ground");
            _jumpRoutine = JumpRoutine();

            _playerInput.actions["Jump"].performed += _ =>
            {
                _airController.IsGrounding = _groundingChecker.IsGroundingByRaycast(transform.position);
                if (_airController.IsJumping && !_airController.IsGrounding) return;

                OnJumped?.Invoke();
            };
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _groundingChecker.RayColor;
            Gizmos.DrawRay(transform.position + _groundingChecker.RayToGroundPosition, Vector2.down * _groundingChecker.RayToGroundDistance);
        }

        public void StartJumping()
        {
            AddVerticalImpulse();
            _airController.IsJumping = true;
            StartCoroutine(JumpRoutine());
        }
        public void StopJumping()
        {
            StopCoroutine(JumpRoutine());
            _airController.IsJumping = false;
            _airController.IsFalling = false;
        }

        public void CheckGrounding(float velY)
        {
            _airController.IsGrounding = _groundingChecker.IsGroundingByRaycast(transform.position);
            if (_airController.IsGrounding && velY == 0f)
            {
                OnJumpStopped?.Invoke();
            }
        }

        public void CheckFalling(float velY) => _airController.IsFalling = !_airController.IsGrounding && velY < 0f;

        private IEnumerator JumpRoutine()
        {
            while (true)
            {
                OnFell?.Invoke(_rigidbody.velocity.y);
                yield return null;
            }
        }

        private void AddVerticalImpulse() => _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        //private bool IsGroundingByRaycast => Physics2D.Raycast(transform.position + _rayToGroundPosition, Vector2.down, _rayToGroundDistance, _groundLayer);

    }
}