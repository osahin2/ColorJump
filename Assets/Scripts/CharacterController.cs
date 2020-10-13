using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private const float JUMP_AMOUNT = 8f;

    public Animator anim;
    public float speedX;
    public float speedZ;

    private Rigidbody rb;
    private float horizontal;
    private bool isGround = true;
    private bool secondJump = false;
    private Vector3 moveDir;
    private float moveX = 0f;
    private bool isMove;
    private RaycastHit raycastHit;

    private bool _endGameControl;
    public bool endGameControl {
        get
        {
            return _endGameControl;
        } 
            }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("horizontal", horizontal);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.A) && !isGround)
        {
            isMove = true;
            moveX = -1f;
        }

        if (Input.GetKeyDown(KeyCode.D) && !isGround)
        {
            isMove = true;
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, 0).normalized;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "JumpControl")
        {
            isGround = true;
            secondJump = false;
            anim.SetBool("isJump", false);
            anim.SetBool("secondJump", false);
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.TryGetComponent(out GroundController ground))
        {
            if (ground.IsRed)
            {
                gameObject.TryGetComponent<CharacterController>(out CharacterController character);
                character.enabled = false;
                _endGameControl = true;
            }
        }
    }

    private void Jump()
    {
        if (secondJump)
        {
            anim.SetBool("secondJump", true);
            rb.velocity = Vector3.up * JUMP_AMOUNT;
            secondJump = false;
        }

        if (isGround)
        {
            anim.SetBool("isJump", true);
            rb.velocity = Vector3.up * JUMP_AMOUNT;
            isGround = false;
            secondJump = true;
        }
    }
    private void Move()
    {
        Vector3 position = rb.position;
        position.z = position.z + speedZ * Time.deltaTime;
        rb.MovePosition(position);

        if (isMove)
        {
            Vector3 movePosition = transform.position + moveDir * speedX;
            Physics.Raycast(transform.position, moveDir, out raycastHit, speedX);
            if (raycastHit.collider != null)
            {
                movePosition = transform.position;
            }
            rb.MovePosition(movePosition);
            isMove = false;
        }
    }
}
