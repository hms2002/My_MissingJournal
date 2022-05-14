using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    Slot[] slots;
    Transform slotMenu;

    private void Awake()
    {
        inven = Inventory.instance;
        Debug.Log("z");
        inven.onChangeItem += RedrawSlotUI;

        slotMenu = inven.soltMenu;
    }
    private void Start()
    {
        slots = slotMenu.GetComponentsInChildren<Slot>();
    }
    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdataSlotUI();
        }
    }
}
