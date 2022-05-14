using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string name;
    public Sprite itemImages;
    public List<ItemEffects> effects = new List<ItemEffects>();
    public bool alreadyTouch = false;

    public bool Use()
    {
        bool isUsed = false;

        foreach(ItemEffects efts in effects)
        {
            isUsed = efts.ExecuteRole();
        }

        return isUsed;
    }
}