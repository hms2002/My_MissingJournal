using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animation Anim;

    private int Attack = 25;
    private float CurTime;
    public float CoolTime = 0.5f;

    public Transform Pos;
    public Vector2 BoxSize;

    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if ( CurTime <= 0 )
        {
            if ( Input.GetKey(KeyCode.Z) )
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, BoxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if ( collider.tag == "GroundEnemy" )
                    {
                        collider.GetComponent<GroundEnemy>().TakeDamage(Attack);
                    }
                    else if ( collider.tag == "FlyEnemy" )
                    {
                        collider.GetComponent<FlyEnemy>().TakeDamage(Attack);
                    }
                }
                // 공격 애니메이션
                CurTime = CoolTime;
            }
        }
        else
        {
            CurTime -= UnityEngine.Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, BoxSize);
    }
}
