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

    InventoryUI invenUI;

    public List<Item> items = new List<Item>();

    // ���� ����ΰ� Ȯ��
    public bool onCreate = true;


    private void Start()
    {
        instance.onCreate = false;
        invenUI = InventoryUI.instance;

        slots = soltMenu.GetComponentsInChildren<Slot>();

        for (int i = 0; i < SLOT_SIZE; i++)
        {
            slots[i].isEmpty = true;
        }
    }

    public int highlightSlotIdx = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            highlightSlotIdx = 0;
            invenUI.HighlightSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            highlightSlotIdx = 1;
            invenUI.HighlightSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            highlightSlotIdx = 2;
            invenUI.HighlightSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            highlightSlotIdx = 3;
            invenUI.HighlightSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            highlightSlotIdx = 4;
            invenUI.HighlightSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            highlightSlotIdx = 5;
            invenUI.HighlightSlot(5);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            bool success = slots[highlightSlotIdx].UseSlot();
            Debug.Log(success);
        }
    }

    public bool AddItem(Item _item)
    {
        for(int i = 0; i < SLOT_SIZE; i++)
        {
            // ���� �ȿ� ������ �ִ��� Ȯ��
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