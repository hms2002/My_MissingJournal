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
    Branch,         // ��������
    Fruits,         // ����
    Stone,          // �� ����
    Club,           // ������
    Pick,           // ���
    Axe,            // ����
    Pail,           // �絿��
    FullPail,       // ���� �� �絿��
    WaterPurifier,  // ������
    Bonfire         // ��ں�
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
        GameObject go = Instantiate(fieldItemPrefab, new Vector3(1, 2, 0), Quaternion.identity);
        go.GetComponent<FieldItems>().SetItem(itemDB[(int)ItemList.Branch]);
        go = Instantiate(fieldItemPrefab, new Vector3(1, 2, 0), Quaternion.identity);
        go.GetComponent<FieldItems>().SetItem(itemDB[(int)ItemList.Branch]);
        go = Instantiate(fieldItemPrefab, new Vector3(1, 2, 0), Quaternion.identity);
        go.GetComponent<FieldItems>().SetItem(itemDB[(int)ItemList.Branch]);
        go = Instantiate(fieldItemPrefab, new Vector3(1, 2, 0), Quaternion.identity);
        go.GetComponent<FieldItems>().SetItem(itemDB[(int)ItemList.Stone]);
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