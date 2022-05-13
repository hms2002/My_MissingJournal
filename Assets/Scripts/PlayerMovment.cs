using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float MaxSpeed;          // 이동 속도
    public float JumpHeight;        // 점프 높이
    private bool LongJump = false;  // 낮은 점프, 높은 점프
    
    private LayerMask Ground;
    private CapsuleCollider2D capsuleCollider2D;
    private bool isGrounded;
    private Vector2 FootPosition;


    Rigidbody2D rigid;

    void Start()
    {
        
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(h * MaxSpeed, rigid.velocity.y);

        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            Jump();
        }

        if ( Input.GetKey(KeyCode.Space) )
        {
            LongJump = true;
        }
        else if ( Input.GetKeyUp(KeyCode.Space) )
        {
            LongJump = false;
        }
    }

    void FixedUpdate()
    {
    //    Bounds bounds = capsuleCollider2D.bounds;
     //   FootPosition = new Vector2(bounds.center.x, bounds.min.y);
    //    isGrounded = Physics2D.OverlapCircle(FootPosition, 0.1f, Ground);

        if ( LongJump && rigid.velocity.y > 0 )
        {
            rigid.gravityScale = 0.5f;
        }
        else
        {
            rigid.gravityScale = 1f;
        }
    }

    private void Jump()
    {   
        rigid.velocity = Vector2.up * JumpHeight;
        
    }
}
