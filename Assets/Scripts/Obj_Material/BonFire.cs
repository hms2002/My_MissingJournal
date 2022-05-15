using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BonFire : MonoBehaviour
{
    Inventory inven;

    public Image fillImg;

    public TextMeshProUGUI curTimeText;

    float maxTime;
    float curTime;

    private void Start()
    {
        inven = Inventory.instance;
        maxTime = 360;
        curTime = 360;
    }

    private void Update()
    {
        curTime -= UnityEngine.Time.deltaTime;

        fillImg.fillAmount = curTime / maxTime;

        curTimeText.text = ((int)curTime).ToString();
        
        if(curTime <= 0)
        {
            ItemDatabase.instance.DropItem((int)ItemList.Branch, transform.position);
            Destroy(gameObject);
        }
    }

    public void Cook()
    {
        for(int i = 0; i < inven.slots.Length; i++)
        {
            if (inven.slots[i].item == null) continue;
            if(inven.slots[i].item.name == "RawMeat")
            {
                while(inven.slots[i].item == null)
                {
                    inven.slots[i].UseSlot();
                    ItemDatabase.instance.DropItem((int)ItemList.Meat, transform.position);
                }
            }
        }
    }

    public void AddFire()
    {
        int woodCnt = 3;
        int stoneCnt = 1;
        // 플레이어 제작 모드 켜기
        Inventory.instance.onCreate = true;
        // 뒤에서부터 재료 제거
        for (int i = inven.slots.Length - 1; i >= 0; i--)
        {
            Slot slot = inven.slots[i];

            if (slot.isEmpty) continue;

            if (slot.item.itemType == ItemType.Material)
            {
                if (slot.item.name == "Branch")
                {
                    while (slot.item != null && woodCnt != 0)
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
        // 재료 사용 후 플레이어 제작모드 끄기
        Inventory.instance.onCreate = false;

        curTime += 360;
    }
}