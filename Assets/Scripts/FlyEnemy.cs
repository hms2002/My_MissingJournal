using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip BatCry;
    public AudioClip BatAttack;

    private int CurEnemyHp = 25;         // 적 현재 Hp
    private int MaxEnemyHp = 25;         // 적 최대 HP
    private int EnemyAttack = 15;        // 적 공격력
    public float AttackRange;            // 적 공격 범위            
    public float AttackCoolTime;         // 적 공격 속도 (몇 초 마다 공격 하는지)
    public float AttackCurTime = 0;      // 적 공격 딜레이
    public float EnemySpeed;             // 적 속도
    public float FieldOfVision;          // 적 시야 범위
    private float timer = 0;             // 플레이어 미탐지 시간
    private bool Detect = false;         // 플레이어 탐지 여부

    public int NextMove;                 // 다음 이동

    public GameObject Bullet;            // 총알 오브젝트
    public Transform Pos;                // 사출 위치
    public GameObject player;             // 플레이어 트랜스폼

    public Transform Pos1;
    public float CircleSize1;

    FlyEnemy enemy;
    Rigidbody2D rigid;

    void Start()
    {
        enemy = GetComponent<FlyEnemy>();
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
            GameObject.Find("EnemyManager").GetComponent<EnemyManager>().DeleteEnemy(0);
        }

        AttackCurTime -= UnityEngine.Time.deltaTime;

        if ( AttackCurTime < 0 )
        {
            AttackCurTime = 0;
        }
        
        Collider2D[] collider2Ds1 = Physics2D.OverlapCircleAll(Pos1.position, CircleSize1);
        foreach (Collider2D collider in collider2Ds1)
        {
            if (AttackCurTime == 0 && collider.tag == "Player")
            {   
                CancelInvoke();
                NextMove = 0;
                Detect = true;
                player = GameObject.FindGameObjectWithTag("Player");
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle , Vector3.forward);
                Pos.rotation = rotation;

                audioSource.pitch = 2;
                audioSource.clip = BatAttack;
                audioSource.volume = 0.3f;
                if (audioSource.isPlaying == false)
                {
                    audioSource.Play();
                }

                GameObject Bulletcopy = Instantiate(Bullet, Pos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                AttackCurTime = AttackCoolTime;  
            }
            else
            if (Detect == true)
            {
                timer += UnityEngine.Time.deltaTime;            
            }
            AttackCurTime -= UnityEngine.Time.deltaTime;
        }

        if (timer >= 2)
        {
            Invoke("Think", 5);
            timer = 0;
            Detect = false;
        }
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(NextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + NextMove*0.4f, rigid.position.y - 3);
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

    public void TakeDamage(int Attack)
    {
        CurEnemyHp -= Attack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Pos1.position, CircleSize1);
    }
}
