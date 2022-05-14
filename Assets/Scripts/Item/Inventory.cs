using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;


    const int SLOT_SIZE = 6;

    public Transform soltMenu;

    public Slot[] slots;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private void Start()
    {
        slots = soltMenu.GetComponentsInChildren<Slot>();

        for (int i = 0; i < SLOT_SIZE; i++)
        {
            slots[i].isEmpty = true;
        }
    }

    public bool AddItem(Item _item)
    {
        for(int i = 0; i < SLOT_SIZE; i++)
        {
            // 슬롯 안에 아이템 있는지 확인
            if (slots[i].isEmpty == true)
            {
                slots[i].isEmpty = false;
                slots[i].item = _item;
                slots[i].stack++;

                if (onChangeItem != null)
                {
                    onChangeItem.Invoke();
                }
                return true;
            }
            else if(slots[i].item.name == _item.name)
            {
                if(slots[i].stack < slots[i].maxStack)
                {
                    slots[i].isEmpty = false;
                    slots[i].stack++;

                    if(onChangeItem != null)
                        onChangeItem.Invoke();
                    return true;
                }
            }
        }

        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("FieldItem"))
        {
            FieldItems fieldItems = collision.gameObject.GetComponent<FieldItems>();
            if (fieldItems.item.alreadyTouch == false)
            {
                fieldItems.item.alreadyTouch = true;

                if (AddItem(fieldItems.GetItem()))
                    fieldItems.DestroyItem();
            }
        }
    }
}