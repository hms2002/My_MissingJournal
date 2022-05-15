using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public Button btn;
    public Image coolTimeImg;
    public Slider fellSlider;

    Inventory inven;
    
    public float waitTime = 5;
    public float fellingTime;
    float curTime;

    bool onFalling;
    bool isWaitingTime;

    private void Start()
    {
        waitTime = 5f;

        fellSlider.gameObject.SetActive(false);

        inven = Inventory.instance;

        isWaitingTime = false;
        curTime = 0;
    }
    float x;
    private void Update()
    {
        // ���� ĳ�� ��Ÿ��
        if(onFalling)
        {
            x = Input.GetAxisRaw("Horizontal");

            // ĳ�� ���߿� �Է¹����� ����
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || x != 0 || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6))
            {
                PlayerMovment.instance.StopFelling();

                onFalling = false;

                // ĳ�� ���൵ �����̴� ����
                fellSlider.gameObject.SetActive(false);

                // ��Ÿ�� �̹��� �ѱ�
                coolTimeImg.transform.parent.gameObject.SetActive(true);
                coolTimeImg.fillAmount = 1;

                // ��ư ���̸� ��ȣ�ۿ� �����ϵ��� �ٲٰ�, ��ư ���������� ��� �Ѽ� ��ȣ�ۿ� �����ϰ� �ٲٰ� �ٽ� ����
                if (btn.gameObject.activeSelf == true)
                    btn.interactable = true;
                else
                {
                    btn.gameObject.SetActive(true);
                    btn.interactable = true;
                    btn.gameObject.SetActive(false);
                }

                return;
            }


            curTime += UnityEngine.Time.deltaTime;
            fellSlider.value = curTime;
            
            // ���� ĳ�� ������ ��
            if(curTime >= fellingTime)
            {
                PlayerMovment.instance.StopFelling();
                onFalling = false;
                curTime = 0;

                isWaitingTime = true;

                fellSlider.gameObject.SetActive(false);

                coolTimeImg.transform.parent.gameObject.SetActive(true);

                DropItem();
            }
        }
        // ��� �ð� ��Ÿ��
        else if(isWaitingTime == true)
        {
            curTime += UnityEngine.Time.deltaTime;
            
            coolTimeImg.fillAmount = curTime / waitTime;
            
            if (curTime > waitTime)
            {
                isWaitingTime = false;

                curTime = 0;

                btn.interactable = true;
            }
        }
    }

    public void Felling()
    {
        if (isWaitingTime == true) return;

        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null) return;
        if (inven.slots[inven.highlightSlotIdx].item.name == "FullPail")
        {
            inven.slots[inven.highlightSlotIdx].RemoveSlot();
            inven.AddItem(ItemDatabase.instance.itemDB[(int)ItemList.Pail], inven.highlightSlotIdx);
        }


        btn.interactable = false;

        coolTimeImg.fillAmount = 0;
        coolTimeImg.transform.parent.gameObject.SetActive(false);

        onFalling = true;
        fellSlider.gameObject.SetActive(true);

        if (inven.slots[inven.highlightSlotIdx].item.name == "Axe")
            fellingTime = 10;
        else
            fellingTime = 15;

        fellSlider.maxValue = fellingTime;
        
        PlayerMovment.instance.Felling();
    }

    public void DropItem()
    {
        ItemDatabase.instance.DropItem((int)ItemList.Branch, transform.position);

        switch(Random.Range(0, 2))
        {
            case 0:
                break;
            case 1:
                ItemDatabase.instance.DropItem((int)ItemList.Fruits, transform.position);
                break;
        }
    }
}