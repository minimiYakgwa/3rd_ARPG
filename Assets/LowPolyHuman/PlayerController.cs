using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _walkSpeed = 5f;
    [SerializeField]
    private float _rotationSpeed = 25f;
    [SerializeField]
    private float _SprintSpeed = 10f;

    public CinemachineCamera CinemachineCamera;

    public Vector2 MoveInput = Vector2.zero;
    private Vector3 _moveVelocity = Vector3.zero;
    private Animator _anim;

    private Rigidbody _rigid;

    private bool _isAttacking = false;
    private bool _isSprinting = false;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        ShowMovingAnim();
    }

    private void ShowMovingAnim()
    {
        if (_isAttacking) return;
        if (_moveVelocity != Vector3.zero)
        {
            if (_isSprinting)
            {
                _anim.SetBool("isSprint", true);
            }
            else
            {
                _anim.SetBool("isWalk", true);
            }  
        }
        else
        {
            _anim.SetBool("isWalk", false);
        }
    }

    private void Move()
    {
        if (_isAttacking) return;

        if (_moveVelocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveVelocity);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }

        if (_moveVelocity != Vector3.zero)
        {
            _speed = _walkSpeed;
            _isSprinting = false;

        }

        Vector3 forwardVector = CinemachineCamera.transform.forward;
        forwardVector.y = 0;
        Vector3 rightVector = CinemachineCamera.transform.right;
        _moveVelocity = forwardVector.normalized * MoveInput.y + rightVector.normalized * MoveInput.x;
        _rigid.MovePosition(transform.position + _moveVelocity * _speed * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        _moveVelocity = new Vector3(MoveInput.x, 0, MoveInput.y).normalized;

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && !_isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("공격!");
        _isAttacking = true;
        _anim.SetTrigger("Attack");
    }

    public void EndAttack()
    {
        Debug.Log("공격 완료!");
        _isAttacking = false;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isSprinting = true;
            _speed = _SprintSpeed;
        }
    }
}
