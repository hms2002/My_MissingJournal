using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip caveWalkSound;
    public AudioClip forestWalkSound;
    public AudioClip beachWalkSound;

    public static PlayerMovment instance;

    Animator anim;              // 플레이어 애니메이션
    SpriteRenderer renderer;    // 플레이어 이미지 좌우반전

    public float MaxSpeed;          // 이동 속도
    private bool canMove;

    public float JumpHeight;        // 점프 높이
    private bool LongJump = false;  // 낮은 점프, 높은 점프
    private bool CanJump;
    public bool canJump
    {
        get { return CanJump; }
        set {
            Debug.Log("값 : " + value);
            CanJump = value; }
    }

    int Ground;                       // 바닥 레이어
    int CaveGround;
    int ForestGround;
    int BeachGround;

    private CapsuleCollider2D capsuleCollider2D;    // 오브젝트 충돌 범위 컴포넌트
    private bool IsGrounded;
    public bool isGrounded
    {
        get { return IsGrounded; }
        set {
            IsGrounded = value;
            if (audioSource != null)
            {
                if (value == false && audioSource.isPlaying == true)
                    audioSource.Stop(); 
                anim.SetBool("isGrounded", IsGrounded); }
            }
           
    }// 바닥 충돌 여부
    private Vector2 FootPosition;                   // 발 위치

    Rigidbody2D rigid;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Ground = ((1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("CaveGround")) | (1 << LayerMask.NameToLayer("ForestGround")) | (1 << LayerMask.NameToLayer("BeachGround")));

        CaveGround = (1 << LayerMask.NameToLayer("CaveGround"));
        ForestGround = (1 << LayerMask.NameToLayer("ForestGround"));
        BeachGround = (1 << LayerMask.NameToLayer("BeachGround"));

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


            if (Physics2D.OverlapCircle(FootPosition, 0.1f, CaveGround))
            {
                audioSource.pitch = 2;
                audioSource.clip = caveWalkSound;
                audioSource.volume = 0.3f;
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
            }
            else if (Physics2D.OverlapCircle(FootPosition, 0.1f, ForestGround))
            {
                audioSource.pitch = 2;
                audioSource.volume = 0.3f;
                audioSource.clip = forestWalkSound;
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
            }
            else if (Physics2D.OverlapCircle(FootPosition, 0.1f, BeachGround))
            {
                audioSource.pitch = 2;
                audioSource.volume = 0.3f;
                audioSource.clip = beachWalkSound;
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }
            }

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
            if (audioSource != null)
            {
                if(audioSource.isPlaying == true)
                    audioSource.Stop();                
            }

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

        if (inven.slots[inven.highlightSlotIdx].item == null)
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", true);
            anim.SetTrigger("isOnce");
            return;
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 1);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", true);
            anim.SetTrigger("isOnce");
        }
    }

    public void StopFelling()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null)
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
            return;
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
        else if(inven.slots[inven.highlightSlotIdx].item.name == "Pail")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 1);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Club")
        {

        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
        }
    }


    public void Picking()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null)
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", true);
            anim.SetTrigger("isOnce");
            return;
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Pick")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 1);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", true);
            anim.SetTrigger("isOnce");
        }

    }

    public void StopPicking()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null)
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
            return;
        }

        if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Pail")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 1);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
        }
        else if(inven.slots[inven.highlightSlotIdx].item.name == "Club")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 1);
        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
        }
    }

    public void HoldingPail()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
        anim.SetLayerWeight(3, 1);
        anim.SetLayerWeight(4, 0);
    }

    public void StopHoldingPail()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null)
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            return;
        }

        anim.SetBool("isHandling", false);

        if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Pail")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 1);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Club")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 1);
        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
    }

    public void HoldingClub()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
        anim.SetLayerWeight(3, 0);
        anim.SetLayerWeight(4, 1);
    }

    public void StopHoldingClub()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null)
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
            return;
        }

        anim.SetBool("isHandling", false);

        if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Pail")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 1);
            anim.SetLayerWeight(4, 0);
            anim.SetBool("isHandling", false);
        }
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Club")
        {
            anim.SetLayerWeight(0, 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 1);
        }
        else
        {
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(4, 0);
        }
    }
    public void PlayerFree()
    {
        canMove = true;
        canJump = true;
    }

    public void PlayerConfine()
    {
        canMove = false;
        canJump = false;
        anim.SetBool("isWalking", false);
    }
}