using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Movement2D : MonoBehaviour
{
    [SerializeField] 
    private float _moveSpeed = 1f;
    //[SerializeField]
    //private UnityEvent<Vector2> MovingEvent;

    //private InputMapping _inputMapping;

    private void Awake()
    {
        //_inputMapping = new InputMapping();
    }

    void Start()
    {
        //MovingEvent.AddListener(Move);
        /*_inputMapping.Default.Axis2D.performed += context =>
        {
            Move(context.ReadValue<Vector2>());
        };*/
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

    #region MOVE OPTION #3
    public void Move(InputAction.CallbackContext ctx) => transform.Translate(_moveSpeed * ctx.ReadValue<Vector2>().x * Time.fixedTime * Vector3.right);

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
