using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    const int MAX_SLOT_SIZE = 6;

    Inventory inven;
    Slot[] slots;
    Image[] images = new Image[MAX_SLOT_SIZE];
    Transform slotMenu;

    int highlightSlotIdx;
    Color defaultColor;

    private void Awake()
    {
        // �̱��� ó��
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;


        // ������ ���� ����
        highlightSlotIdx = 0;

        inven = Inventory.instance;
        inven.onChangeItem += RedrawSlotUI;

        slotMenu = inven.soltMenu;

        slots = slotMenu.GetComponentsInChildren<Slot>();
        for (int i = 0; i < MAX_SLOT_SIZE; i++)
            images[i] = slots[i].GetComponent<Image>();
    }
    private void Start()
    {

        defaultColor = images[0].color;

        HighlightSlot(0);
    }
    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdataSlotUI();
        }
    }

    public void HighlightSlot(int slotNum)
    {
        images[highlightSlotIdx].color = defaultColor;

        // 물 든 양동이 예외처리
        if(inven.slots[highlightSlotIdx].item != null && inven.slots[highlightSlotIdx].item.name == "FullPail")
        {
            inven.slots[highlightSlotIdx].RemoveSlot();
            inven.AddItem(ItemDatabase.instance.itemDB[(int)ItemList.Pail], highlightSlotIdx);
        }
        else if (inven.slots[highlightSlotIdx].item != null && inven.slots[highlightSlotIdx].item.name == "Pail")
        {
            PlayerMovment.instance.StopHoldingPail();
        }
        else if (inven.slots[highlightSlotIdx].item != null && inven.slots[highlightSlotIdx].item.name == "Club")
        {
            PlayerMovment.instance.StopHoldingClub();
        }

        images[slotNum].color = Color.white;

        highlightSlotIdx = slotNum;

        if (inven.slots[highlightSlotIdx].item != null && inven.slots[highlightSlotIdx].item.name == "Pail")
        {
            PlayerMovment.instance.HoldingPail();
        }
        else if (inven.slots[highlightSlotIdx].item != null && inven.slots[highlightSlotIdx].item.name == "Club")
        {
            PlayerMovment.instance.HoldingClub();
        }
    }
}
