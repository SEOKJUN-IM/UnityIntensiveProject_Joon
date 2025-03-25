using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;    
    private Vector2 curMovementInput;    

    private Rigidbody _rigidbody;
    public Animator animator01;
    public Animator animator02;
    public bool isWalking = false;
    public bool isAttacking = false;    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }    

    void FixedUpdate()
    {
        Move();
        DirectlyMoveAnimation();

        Rotation();
    }    

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
        if (_rigidbody.velocity.x != 0f || _rigidbody.velocity.z != 0f) isWalking = true;
        else if (_rigidbody.velocity.x == 0f && _rigidbody.velocity.z == 0f) isWalking = false;
    }

    // 사용자 입력 받는 OnMove(테스트 위해 일단 GameScene에서만 작동)
    public void OnMove(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.inGameScene)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                curMovementInput = context.ReadValue<Vector2>();
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                curMovementInput = Vector2.zero;
            }
        }        
    }

    // 사용자 입력으로 직접 움직일 때
    void DirectlyMoveAnimation()
    {
        if (GameManager.Instance.onChar01 && isWalking) animator01.SetBool("Walk", true);
        else if (GameManager.Instance.onChar01 && !isWalking) animator01.SetBool("Walk", false);

        if (GameManager.Instance.onChar02 && isWalking) animator02.SetBool("Walk", true);
        else if (GameManager.Instance.onChar02 && !isWalking) animator02.SetBool("Walk", false);
    }

    void Rotation()
    {
        if (isWalking)
        {
            Quaternion plRotate = Quaternion.LookRotation(new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, plRotate,Time.deltaTime * 100);
        }       
    }

    // 사용자 입력 받는 OnAttack(테스트 위해 일단 GameScene에서만 작동)
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.inGameScene && !EventSystem.current.IsPointerOverGameObject())
        {
            if (GameManager.Instance.onChar01 && context.phase == InputActionPhase.Started)
            {
                animator01.SetTrigger("Attack");
            }
            else if (GameManager.Instance.onChar02 && context.phase == InputActionPhase.Started)
            {
                animator02.SetTrigger("Attack");
            }
        }
    }

    // MoveState : IState에서 호출
    public void OnMoveAnimation()
    {
        if (GameManager.Instance.onChar01) animator01.SetBool("Walk", true);
        else if (GameManager.Instance.onChar02) animator02.SetBool("Walk", true);
    }

    // MoveState : IState에서 호출
    public void OffMoveAnimation()
    {
        if (GameManager.Instance.onChar01) animator01.SetBool("Walk", false);
        else if (GameManager.Instance.onChar02) animator02.SetBool("Walk", false);
    }

    // AttackState : IState에서 호출
    public void OnAttackAnimation()
    {
        if (GameManager.Instance.onChar01 && isAttacking) animator01.SetTrigger("Attack");
        else if (GameManager.Instance.onChar02 && isAttacking) animator02.SetTrigger("Attack");
    }

    // AttackState : IState에서 호출
    public void OffAttackAnimation()
    {
        if (GameManager.Instance.onChar01 && !isAttacking) animator01.ResetTrigger("Attack");
        else if (GameManager.Instance.onChar02 && !isAttacking) animator02.ResetTrigger("Attack");
    }

    // AttackState : IState에서 호출
    public void OnGetHitAnimation()
    {
        if (GameManager.Instance.onChar01) animator01.SetTrigger("GetHit");
        else if (GameManager.Instance.onChar02) animator02.SetTrigger("GetHit");
    }

    // DeadState : IState에서 호출
    public void OnDeadAnimation()
    {
        if (GameManager.Instance.onChar01) animator01.SetBool("Dead", true);
        else if (GameManager.Instance.onChar02) animator02.SetBool("Dead", true);
    }

    // DeadState : IState에서 호출
    public void OffDeadAnimation()
    {
        if (GameManager.Instance.onChar01) animator01.SetBool("Dead", false);
        else if (GameManager.Instance.onChar02) animator02.SetBool("Dead", false);
    }
}
