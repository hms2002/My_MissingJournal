using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Speed;
    public int Attack = 10;

    public float distance;

    public LayerMask isLayer;
    public Transform Pos;

    void Start()
    {
        Invoke("DestroyBullet", 2);
    }
    
    void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if ( raycast.collider != null )
        {
            if ( raycast.collider.tag == "Player" )
            {
                PlayerHp.CurHp -= Attack;
            }
            DestroyBullet();
        }

        transform.Translate(transform.right * -1f * Speed * UnityEngine.Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
