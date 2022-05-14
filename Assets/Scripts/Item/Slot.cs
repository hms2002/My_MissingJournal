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

    public void UpdataSlotUI()
    {
        if (item != null)
        {
            slotImg.sprite = item.itemImages;
            stackCountText.text = stack.ToString();
        }
        else
        {
            slotImg.sprite = defaultSprite;
            stackCountText.text = "";
        }
    }  
}
