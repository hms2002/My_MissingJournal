using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    Animator anim;

    private int Attack = 25;
    private float CurTime;
    public float CoolTime = 0.5f;

    public Transform Pos;
    public Transform Axis;
    public Vector2 BoxSize;

    Rigidbody2D rigid;

    Inventory inven;

    Item item;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        inven = Inventory.instance;

        item = Inventory.instance.slots[Inventory.instance.highlightSlotIdx].item;
    }
    
    void Update()
    {
        if (inven.slots[inven.highlightSlotIdx].item == null)
            return;
        else if (inven.slots[Inventory.instance.highlightSlotIdx].item.name != "Club")
        {
            return;
        }
            

        if (Input.GetKeyDown(KeyCode.A))
        {
            Axis.localEulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Axis.localEulerAngles = new Vector3(0, 0, 0);
        }

        if ( CurTime <= 0 )
        {
            if ( Input.GetKey(KeyCode.Mouse0) )
            {

                anim.SetTrigger("Attack");

                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, BoxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if ( collider.tag == "GroundEnemy" )
                    {
                        inven.slots[inven.highlightSlotIdx].UseSlot();
                        collider.GetComponent<GroundEnemy>().TakeDamage(Attack);
                    }
                    else if ( collider.tag == "FlyEnemy" )
                    {
                        inven.slots[inven.highlightSlotIdx].UseSlot();
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
