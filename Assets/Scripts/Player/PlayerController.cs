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
    public bool OnChar01;
    public bool OnChar02;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Look();
        DistinChar();
    }

    void FixedUpdate()
    {
        Move();
        MoveAnimation();
    }

    void DistinChar()
    {
        if (GameManager.Instance.Player.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            OnChar01 = true;
            OnChar02 = false;
        }
        else if (GameManager.Instance.Player.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            OnChar02 = true;
            OnChar01 = false;
        }
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

    public void OnMove(InputAction.CallbackContext context)
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

    void MoveAnimation()
    {
        if (OnChar01 && isWalking) animator01.SetBool("Walk", true);
        else if (OnChar01 && !isWalking) animator01.SetBool("Walk", false);

        if (OnChar02 && isWalking) animator02.SetBool("Walk", true);
        else if (OnChar02 && !isWalking) animator02.SetBool("Walk", false);
    }

    void Look()
    {
        if (OnChar01 && isWalking)
        {
            Quaternion plRotate = Quaternion.LookRotation(new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z), Vector3.up);
            transform.GetChild(0).rotation = Quaternion.RotateTowards(transform.GetChild(0).rotation, plRotate,Time.deltaTime * 100);            
        }
        else if (OnChar02 && isWalking)
        {
            Quaternion plRotate = Quaternion.LookRotation(new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z), Vector3.up);
            transform.GetChild(1).rotation = Quaternion.RotateTowards(transform.GetChild(1).rotation, plRotate, Time.deltaTime * 100);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (OnChar01 && context.phase == InputActionPhase.Started)
            {
                animator01.SetTrigger("Attack");
            }
            else if (OnChar02 && context.phase == InputActionPhase.Started)
            {
                animator02.SetTrigger("Attack");
            }
        }
    }
}
