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

            // ������ ��뿡 �����ϸ� ���� �ϳ� ���̱�
            // �������� ����Ѵٸ�?
            // ������ ������ ���̴� ���� Material�� �ش��Ѵ�.
            switch (item.itemType)
            {
                case ItemType.Material:
                    // �������� ����� �� ����ϱ�
                    if (item.Use() == true) // ��� �� ���� ���̱�
                    {
                        stack--;
                        Inventory.instance.onChangeItem.Invoke();
                    }
                    else return false;

                    break;
                case ItemType.Equipment:
                    // �������� ���� ������ �� ����ϱ�
                    if (item.Use() == true) // ���Ⱑ �μ�����
                    {
                        stack--;// ���� ����
                        Inventory.instance.onChangeItem.Invoke();
                    }
                    else return false;
                    break;
                case ItemType.Food:                    
                    // �Ա⿡ �����ϸ�
                    if (item.Use() == true) 
                    {
                        stack--;// ������ �ϳ� ����
                        Inventory.instance.onChangeItem.Invoke();
                    }
                    else return false;
                    break;
            }
        }
        if (stack < 1)
        {
            Debug.Log("��-��");
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
