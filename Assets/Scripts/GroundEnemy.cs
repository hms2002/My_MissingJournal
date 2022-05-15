using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private int CurEnemyHp = 25;      // 적 현재 Hp
    private int MaxEnemyHp = 25;      // 적 최대 HP
    private int EnemyAttack = 15;     // 적 공격력
    public float AttackRange;         // 적 공격 범위
    public float AttackCoolTime;      // 적 공격 속도 (몇 초 마다 공격 하는지)
    public float AttackCurTime;       // 적 공격 딜레이
    public float EnemySpeed;          // 적 속도
    public float FieldOfVision;       // 적 시야 범위
    public float CurRushtime = 0;          // 플레이어 미탐지 시간
    public float MaxRushtime = 0;
    private bool Direction = false;

    public int NextMove;              // 다음 이동

    public Transform player;          // 플레이어 트랜스폼
    public Transform Pos;
    public Vector2 BoxSize;
    public Transform Axis;

    GroundEnemy enemy;

    Rigidbody2D rigid;

    void Start()
    {
        enemy = GetComponent<GroundEnemy>();
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("Think", 5);
    }

    void Update()
    {
        if ( CurEnemyHp <= 0 )
        {
            Destroy(this.gameObject);
        }

        AttackCurTime -= UnityEngine.Time.deltaTime;

        if ( AttackCurTime < 0 )
        {
            AttackCurTime = 0;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if ( AttackCurTime == 0 )
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, BoxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.tag == "Player")
                {
                    Debug.Log("사람 발견");    
                    Direction = true;
                    RushEnemy();
                }
            }
        }

        if (CurRushtime >= MaxRushtime)
        {
            Invoke("Think", 5);
            CurRushtime = 0;
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

        Vector2 frontVec = new Vector2(rigid.position.x + NextMove*0.4f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
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

    public void TakeDamage(int Attack)
    {
        Debug.Log("Hit");
        CurEnemyHp -= Attack;
    }

    public void RushEnemy()
    {
        CancelInvoke();
        EnemySpeed = 6;
        CurRushtime += UnityEngine.Time.deltaTime;

        if (player.position.x - transform.position.x < 0)
        {
            Direction = false;
            NextMove = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            Direction = true;
            NextMove = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }        
    }
    
    public void AttackPlayer()
    {


        PlayerHp.CurHp -= EnemyAttack;
        // 적 공격 애니메이션
        AttackCurTime = AttackCoolTime;
        EnemySpeed = 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, BoxSize);
    }
}
