using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundEnemy : MonoBehaviour
{
    Animator anim;

    private int CurEnemyHp = 25;      // 적 현재 Hp
    private int MaxEnemyHp = 25;      // 적 최대 HP
    private int EnemyAttack = 15;     // 적 공격력
    public float AttackRange;         // 적 공격 범위
    public float AttackCoolTime;      // 적 공격 속도 (몇 초 마다 공격 하는지)
    public float AttackCurTime;       // 적 공격 딜레이
    public float EnemySpeed = 4;      // 적 속도
    public float FieldOfVision;       // 적 시야 범위
    public float CurRushtime = 0;     // 돌진중인 시간
    private float MaxRushtime = 3;    // 최대 돌진 시간
    private bool Direction = false;    // 좌우 구별
    private bool RushStart = false;    // 돌진 여부

    public int NextMove;              // 다음 이동

    public Transform player;          // 플레이어 트랜스폼
    public Transform Pos1;
    public Transform Pos2;
    public Vector2 BoxSize1;
    public Vector2 BoxSize2;
    public Transform Axis;
    
    GroundEnemy enemy;

    Rigidbody2D rigid;

    PlayerMovment playerMovement;
    Rigidbody2D playerRigid;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovment>();
        playerRigid = playerMovement.GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        enemy = GetComponent<GroundEnemy>();
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("Think", 5);
    }

    void Update()
    {

        AttackCurTime -= UnityEngine.Time.deltaTime;

        if ( AttackCurTime < 0 )
        {
            AttackCurTime = 0;
        }

        Collider2D[] collider2Ds1 = Physics2D.OverlapBoxAll(Pos1.position, BoxSize1, 0);
        foreach (Collider2D collider in collider2Ds1)
        {
            if (AttackCurTime == 0 && collider.tag == "Player" && RushStart == false)
            {   
                EnemyRush();
            }
        }

        Collider2D[] collider2Ds2 = Physics2D.OverlapBoxAll(Pos2.position, BoxSize2, 0);
        foreach (Collider2D collider in collider2Ds2)
        {
            if (AttackCurTime == 0 && collider.tag == "Player")
            {   
                AttackPlayer();
            }
        }

        if (RushStart == true)
        {
            CurRushtime += UnityEngine.Time.deltaTime;
        }

        if (RushStart == true && CurRushtime > MaxRushtime)
        {
            AttackRest();
        }

        if (NextMove == -1)
        {
            Direction = true;
            Axis.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (NextMove == 1)
        {
            Direction = false;
            Axis.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(NextMove * EnemySpeed, rigid.velocity.y);

        if(rigid.velocity.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;

        Vector2 frontVec = new Vector2(rigid.position.x + NextMove*0.4f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("ForestGround"));
        if ( rayHit.collider == null )
        {
            NextMove *= -1;
            CancelInvoke();
            Invoke("Think", 5);
        }
    }

    void Think()
    {
        NextMove = Random.Range(-1, 2);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void EnemyRush()
    {
        CancelInvoke();

        anim.SetBool("OnAtk", true);

        EnemySpeed = 7;
        RushStart = true;

        if (Direction == true)
        {
            NextMove = -1;
//            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Direction == false)
        {
            NextMove = 1;
 //           GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    
    public void TakeDamage(int Attack)
    {
        Debug.Log("Hit");
        CurEnemyHp -= Attack;

        if (CurEnemyHp <= 0)  // HP 없으면 죽음
        {
            ItemDatabase.instance.DropItem((int)ItemList.RawMeat, transform.position);

            GameObject.Find("EnemyManager").GetComponent<EnemyManager>().DeleteEnemy(1);

            Destroy(this.gameObject);
        }
    }

    public void AttackPlayer()
    {
        playerMovement.Hit(transform.position.x < playerMovement.transform.position.x);
        PlayerHp.CurHp -= EnemyAttack;
        AttackRest();
    }

    void AttackRest()
    {
        Debug.Log("브레이크");

        anim.SetBool("OnAtk", false);

        NextMove = 0;
        EnemySpeed = 2;
        CurRushtime = 0;
        RushStart = false;
        AttackCurTime = AttackCoolTime;
        Invoke("Think", 5);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(Pos1.position, BoxSize1);

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireCube(Pos2.position, BoxSize2);
    //}
}
