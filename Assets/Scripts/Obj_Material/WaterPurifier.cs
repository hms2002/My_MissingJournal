using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPurifier : MonoBehaviour
{
    public Button btn;
    public Image countUI;
    public Slider fellSlider;

    Inventory inven;

    float waitTime = 60;
    float curTime;
    int drinkCount;

    bool onPurifying;
    bool isPurified;

    private void Start()
    {
        inven = Inventory.instance;

        drinkCount = 0;

        onPurifying = false;
        isPurified = false;

        countUI.fillAmount = 0;

        fellSlider.maxValue = waitTime;
        fellSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(onPurifying)
        {
            curTime += UnityEngine.Time.deltaTime;
            fellSlider.value = curTime;

            if(curTime >= waitTime)
            {
                onPurifying = false;
                drinkCount = 2;
                countUI.fillAmount = 1;

                fellSlider.gameObject.SetActive(false);
                countUI.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    public void Purifying()
    {
        if(!isPurified)
        {
            if (inven.slots[inven.highlightSlotIdx].item != null && inven.slots[inven.highlightSlotIdx].item.name == "FullPail")
            {
                inven.slots[inven.highlightSlotIdx].RemoveSlot();
                inven.AddItem(ItemDatabase.instance.itemDB[(int)ItemList.Pail], inven.highlightSlotIdx);
            }
            else return;

            onPurifying = true;

            curTime = 0;

            fellSlider.gameObject.SetActive(true);
            countUI.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            // 목마름 채우는 코드 짜야함
            Debug.Log("물이 시원하다!");
            switch(drinkCount)
            {
                case 1:
                    drinkCount--;
                    isPurified = false;
                    countUI.fillAmount = 0;
                    break;
                case 2:
                    drinkCount--;
                    countUI.fillAmount = 0.5f;
                    break;
            }
        }
    }
}
