using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftMenu : MonoBehaviour
{
    public CraftLine[] lines;

    Inventory inven;
    Vector3 toolBoxPos;

    private void Awake()
    {
        inven = Inventory.instance;
        lines = GetComponentsInChildren<CraftLine>();

        toolBoxPos = ToolBox.instance.transform.position;
    }

    public void ReCalcMaterials()
    {
        int woodCnt = 0, stoneCnt = 0;

        foreach(Slot slot in inven.slots)
        {
            if (slot.isEmpty)
                continue;
            if(slot.item.itemType == ItemType.Material)
            {
                if(slot.item.name == "Branch")
                {
                    woodCnt += slot.stack;
                }
                else
                {
                    stoneCnt += slot.stack;
                }
            }
        }


        for(int i = 0; i < lines.Length; i++)
        {
            lines[i].UpdateLine(woodCnt, stoneCnt);
        }
    }

    public void Craft(int woodCnt, int stoneCnt, ItemList _item)
    {
        // �÷��̾� ���� ��� �ѱ�
        Inventory.instance.onCreate = true;
        // �ڿ������� ��� ����
        for(int i = inven.slots.Length - 1; i >= 0; i--)
        {
            Slot slot = inven.slots[i];

            if (slot.isEmpty) continue;

            if (slot.item.itemType == ItemType.Material)
            {
                if (slot.item.name == "Branch")
                {
                    while(slot.item != null && woodCnt != 0)
                    {
                        slot.UseSlot();
                        woodCnt--;
                    }
                }
                else
                {
                    while (slot.item != null && stoneCnt != 0)
                    {
                        slot.UseSlot();
                        stoneCnt--;
                    }
                }
            }
        }
        // ��� ��� �� �÷��̾� ���۸�� ����
        Inventory.instance.onCreate = false;
        
        ItemDatabase.instance.DropItem((int)_item, toolBoxPos);

        ReCalcMaterials();
    }
}
