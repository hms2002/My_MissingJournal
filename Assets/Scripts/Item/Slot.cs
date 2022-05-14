using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    Image slotImg;
    Sprite defaultSprite;
    Text stackCountText;

    public int stack;
    public bool isEmpty;

    public int maxStack
    {
        get
        {
            switch (item.itemType)
            {
                case ItemType.Material:
                    return 5;
                case ItemType.Equipment:
                    return 1;
                case ItemType.Food:
                    return 10;
            }
            return -1;
        }
        set
        {
        }
    }

    private void Awake()
    {
        slotImg = transform.GetChild(0).GetComponent<Image>();
        stackCountText = transform.GetChild(1).GetComponent<Text>();
        defaultSprite = slotImg.sprite;
    }

    public bool UseSlot()
    {
        if(item == null)
            return false;
        else
        {
            Debug.Log(item.name);
            Debug.Log(item.itemType);
            Debug.Log(item.effects);

            // 아이템 사용에 성공하면 개수 하나 줄이기
            // 내구도를 사용한다면?
            // 아이템 개수를 줄이는 것은 Material만 해당한다.
            switch (item.itemType)
            {
                case ItemType.Material:
                    // 아이템이 재료일 때 사용하기
                    if (item.Use() == true) // 사용 후 개수 줄이기
                    {
                        stack--;
                        Inventory.instance.onChangeItem.Invoke();
                    }
                    else return false;

                    break;
                case ItemType.Equipment:
                    // 아이템이 장착 무기일 때 사용하기
                    if (item.Use() == true) // 무기가 부서지면
                    {
                        stack--;// 스택 빼기
                        Inventory.instance.onChangeItem.Invoke();
                    }
                    else return false;
                    break;
                case ItemType.Food:                    
                    // 먹기에 성공하면
                    if (item.Use() == true) 
                    {
                        stack--;// 개수를 하나 뺀다
                        Inventory.instance.onChangeItem.Invoke();
                    }
                    else return false;
                    break;
            }
        }
        if (stack < 1)
        {
            Debug.Log("삭-제");
            RemoveSlot();
            Inventory.instance.onChangeItem.Invoke();
        }
        return true;
    }
    public void RemoveSlot()
    {
        item = null;
        isEmpty = true;
    }

    public void UpdataSlotUI()
    {
        if (item != null)
        {
            slotImg.sprite = item.itemImages;
        }
        else
        {
            slotImg.sprite = defaultSprite;
        }

        if (stack >= 1)
            stackCountText.text = stack.ToString();
        else
            stackCountText.text = "";
    }  
}
