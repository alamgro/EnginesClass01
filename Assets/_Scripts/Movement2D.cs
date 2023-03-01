using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting.FullSerializer;
using System;

public class Movement2D : MonoBehaviour
{
    [SerializeField] 
    private float _moveSpeed = 1f;
    [SerializeField]
    private float _jumpForce = 1f;
    [SerializeField]
    private PlayerInput _playerInput;
    [SerializeField]
    private UnityEvent OnMovementStarted;
    [SerializeField]
    private UnityEvent OnMovementStopped;
    [SerializeField]
    private UnityEvent OnJumped;
    
    //[SerializeField]
    //private UnityEvent<Vector2> MovingEvent;

    private Rigidbody2D _rigidbody;
    //private InputMapping _inputMapping;
    private IEnumerator _movementRoutine;
    private bool _isMoving = false;
    private Vector2 _direction;

    public void SetDirection(InputAction.CallbackContext ctx) => _direction = ctx.ReadValue<Vector2>();

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //MovingEvent.AddListener(Move);
        /*_inputMapping.Default.Axis2D.performed += context =>
        {
            Move(context.ReadValue<Vector2>());
        };*/

        _playerInput.actions["Axis2D"].performed += _=>
        {
            if (_direction.y > 0f)
            {
                Jump();
                OnJumped?.Invoke();
            }

            if (_direction.magnitude == 0f || _isMoving) return;
            
            _isMoving = true;
            OnMovementStarted?.Invoke();
        };
        _playerInput.actions["Axis2D"].canceled += _=>
        {
            _isMoving = false;
            OnMovementStopped?.Invoke();
        };
    }
    
    void Update()
    {
        //MovingEvent?.Invoke(Axis2D);
        //Move();
    }

    private void OnEnable()
    {
        //_inputMapping.Enable();
    }

    private void OnDisable()
    {
        //_inputMapping.Disable();
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
        _direction = Vector2.zero;
    }

    private IEnumerator MovementRoutine()
    {
        while (true)
        {
            Move();
            yield return null;
        }
    }

    #region MOVE OPTION #3
    public void Move() => transform.Translate(_moveSpeed * Time.deltaTime * _direction.x * Vector3.right);

    public void Jump() => _rigidbody.AddForce(_jumpForce * Vector2.up, ForceMode2D.Impulse);

    //private Vector2 Axis2D => _inputMapping.Default.Axis2D.ReadValue<Vector2>();
    #endregion

    #region MOVE OPTION #2
    //private void Move() => transform.Translate(_moveSpeed * Axis2D.x * Time.deltaTime * Vector3.one);

    //private Vector2 Axis2D => _inputMapping.Default.Axis2D.ReadValue<Vector2>();
    #endregion

    #region MOVE OPTION #1
    //private void Move() => transform.Translate(MoveSpeed * Axis2D.x * Time.deltaTime * Vector2.right);

    //private Vector2 Axis2D => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    #endregion
}
