using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment,
    Food
}

public enum ItemList
{
    Branch,         // 나뭇가지
    Fruits,         // 과일
    Stone,          // 돌 조각
    Club,           // 몽둥이
    Pick,           // 곡괭이
    Axe,            // 도끼
    Pail,           // 양동이
    FullPail,       // 가득 찬 양동이
    WaterPurifier,  // 정수기
    Bonfire,        // 모닥불
    RawMeat,        // 생고기
    Meat,           // 구운고기
    Hint
}

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
       
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;

    public void DropItem(int objType, Vector3 objPos)
    {
        GameObject go = Instantiate(fieldItemPrefab, objPos, Quaternion.identity);
        go.GetComponent<FieldItems>().SetItem(itemDB[objType]);
        go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20, 20), Random.Range(10, 30)));
    }
}