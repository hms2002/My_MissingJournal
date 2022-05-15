using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ocean : MonoBehaviour
{
    public Button btn;

    Inventory inven;

    public void FloatingWater()
    {
        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null) return;
        if (inven.slots[inven.highlightSlotIdx].item.name == "Pail")
        {
            inven.slots[inven.highlightSlotIdx].RemoveSlot();
            inven.AddItem(ItemDatabase.instance.itemDB[(int)ItemList.FullPail], inven.highlightSlotIdx);
        }
    }
}
