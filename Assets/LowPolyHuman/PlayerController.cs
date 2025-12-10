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
    private float _rotationSpeed = 25f;
    public CinemachineCamera CinemachineCamera;

    public Vector2 MoveInput = Vector2.zero;
    private Vector3 _moveVelocity = Vector3.zero;
    private Animator _anim;

    private Rigidbody _rigid;

    private bool _isAttacking = false;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimatorStateInfo attackDelay = _anim.GetCurrentAnimatorStateInfo(0);
        Debug.Log("currentAnimation :" + attackDelay);
    }
    private void FixedUpdate()
    {
        if (_moveVelocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveVelocity);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }

        if (_moveVelocity != Vector3.zero)
        {
            _anim.SetBool("isWalk", true);
        }
        else
        {
            _anim.SetBool("isWalk", false);
        }

        Vector3 forwardVector = CinemachineCamera.transform.forward;
        forwardVector.y = 0;
        Vector3 rightVector = CinemachineCamera.transform.right;
        _moveVelocity = forwardVector.normalized * MoveInput.y + rightVector.normalized * MoveInput.x;
        _rigid.MovePosition(transform.position +_moveVelocity * _speed * Time.fixedDeltaTime);
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
            Attack2();
            //StartCoroutine(Attack());
        }
    }

    private void Attack2()
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
    private IEnumerator Attack()
    {
        Debug.Log("공격!");
        _isAttacking = true;
        _anim.SetTrigger("Attack");
        AnimatorStateInfo attackDelay = _anim.GetCurrentAnimatorStateInfo(1);
        while (attackDelay.normalizedTime >= 1.0f)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        Debug.Log("공격 완료!");
        _isAttacking = false;
    }

}
