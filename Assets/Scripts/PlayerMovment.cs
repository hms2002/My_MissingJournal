using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public static PlayerMovment instance;

    Animator anim;              // 플레이어 애니메이션
    SpriteRenderer renderer;    // 플레이어 이미지 좌우반전

    public float MaxSpeed;          // 이동 속도
    private bool canMove;

    public float JumpHeight;        // 점프 높이
    private bool LongJump = false;  // 낮은 점프, 높은 점프
    private bool canJump;

    public LayerMask Ground;                       // 바닥 레이어
    private CapsuleCollider2D capsuleCollider2D;    // 오브젝트 충돌 범위 컴포넌트
    private bool isGrounded;                        // 바닥 충돌 여부
    private Vector2 FootPosition;                   // 발 위치

    Rigidbody2D rigid;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        canMove = true;
        canJump = true;
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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

    private void Move()
    {
        if (!canMove) return;

        float h = Input.GetAxis("Horizontal");

        if (h != 0)
        {
            anim.SetBool("isWalking", true);

            if (h > 0)
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
            anim.SetBool("isWalking", false);

        }

        rigid.velocity = new Vector2(h * MaxSpeed, rigid.velocity.y);
    }

    private void Jump()
    {
        if (!canJump) return;


        if (Input.GetKey(KeyCode.Space))
        {
            LongJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            LongJump = false;
        }

        if ( isGrounded == true )
        {
            rigid.velocity = Vector2.up * JumpHeight;
        }
    }

    public void Felling()
    {
        Inventory inven = Inventory.instance;
        if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetBool("isHandling", true);
            anim.SetTrigger("isOnce");
        }
    }

    public void StopFelling()
    {
        Inventory inven = Inventory.instance;
        if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
        }
        else if(inven.slots[inven.highlightSlotIdx].item.name == "Pail")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 1);
            anim.SetBool("isHandling", false);
            anim.SetTrigger("isOnce");
        }
    }


    public void Picking()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item.name == "Pick")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 1);
            anim.SetLayerWeight(3, 0);
        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetBool("isHandling", true);
            anim.SetTrigger("isOnce");
        }

    }

    public void StopPicking()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Pail")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 1);
            anim.SetBool("isHandling", false);
            anim.SetTrigger("isOnce");
        }
    }

    public void HoldingPail()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
        anim.SetLayerWeight(3, 1);
    }

    public void StopHoldingPail()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
        anim.SetLayerWeight(3, 0);
    }

    public void PlayerFree()
    {
        canMove = true;
        canMove = true;
    }

    public void PlayerConfine()
    {
        canMove = false;
        canJump = false;
        anim.SetBool("isWalking", false);
    }
}