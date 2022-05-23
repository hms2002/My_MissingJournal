using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private float CurFE = 0;
    private float MaxFE = 20;
    private bool CreFE = false;
    private float CurGE = 0;
    private float MaxGE = 20;
    private bool CreGE = false;
    float PosX;
    float PosY;
    public GameObject FEEnemy;
    public GameObject GEEnemy;
    private BoxCollider2D Area;
    
    void Start()
    {
        Area = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        CurFE -= UnityEngine.Time.deltaTime;
        CurGE -= UnityEngine.Time.deltaTime;

        if ( CurFE < 0 )
        {
            CurFE = 0;
        }
        
        if ( CurGE < 0 )
        {
            CurGE = 0;
        }

        if (CreFE == false && CurFE <= 0)
        {
            Vector3 SpawnPos = GetRandomPosition(0);   

            GameObject instance = Instantiate(FEEnemy, SpawnPos, Quaternion.identity);
            CreFE = true;
        }

        if (CreGE == false && CurGE <= 0)
        {
            Vector3 SpawnPos = GetRandomPosition(1);   

            GameObject instance = Instantiate(GEEnemy, SpawnPos, Quaternion.identity);
            CreGE = true;
        }


    }

    private Vector2 GetRandomPosition(int type)
    {
        Vector2 BasePosition = transform.position;
        Vector2 Size = Area.size;

        if (type == 0)
        {
            PosX = BasePosition.x + Random.Range(-Size.x / 2f, Size.x / 2f);
            PosY = BasePosition.y + 2;
        }
        else if (type == 1)
        {
            PosX = BasePosition.x + Random.Range(-Size.x / 2f, Size.x / 2f);
            PosY = BasePosition.y - 1;            
        }


        Vector2 SpawnPos = new Vector2(PosX, PosY);

        return SpawnPos;
    }

    public void DeleteEnemy(int Type)
    {
        if (Type == 0)
        {
            CurFE = MaxFE;
            CreFE = false;
        }
        else if (Type == 1)
        {
            CurGE = MaxGE;
            CreGE = false;
        }
    }
}
