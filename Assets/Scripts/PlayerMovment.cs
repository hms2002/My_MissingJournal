using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    Animator anim;              // 플레이어 애니메이션
    SpriteRenderer renderer;    // 플레이어 이미지 좌우반전

    public float MaxSpeed;          // 이동 속도
    public float JumpHeight;        // 점프 높이
    private bool LongJump = false;  // 낮은 점프, 높은 점프
    
    public LayerMask Ground;                       // 바닥 레이어
    private CapsuleCollider2D capsuleCollider2D;    // 오브젝트 충돌 범위 컴포넌트
    private bool isGrounded;                        // 바닥 충돌 여부
    private Vector2 FootPosition;                   // 발 위치

    Rigidbody2D rigid;

    void Awake()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        if(h != 0)
        {
            /* anim.SetBool("isWalking", true); */

            if(h > 0)
            {
                renderer.flipX = true;
            }
            else
            {
                renderer.flipX = false;
            }
        }
       else
        {
            /* anim.SetBool("isWalking", false); */
            
        }

        rigid.velocity = new Vector2(h * MaxSpeed, rigid.velocity.y);

        if ( Input.GetKeyDown(KeyCode.D) )
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

        if ( Input.GetKeyDown(KeyCode.A) )
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }


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
        Bounds bounds = capsuleCollider2D.bounds;
        FootPosition = new Vector2(bounds.center.x, bounds.min.y);
        isGrounded = Physics2D.OverlapCircle(FootPosition, 0.1f, Ground);

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
        if ( isGrounded == true )
        {
            rigid.velocity = Vector2.up * JumpHeight;
        }
    }
}
