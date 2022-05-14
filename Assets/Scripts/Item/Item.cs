using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Material,
    Equipment
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string name;
    public Sprite itemImages;
    public bool alreadyTouch = false;

    public bool Use()
    {
        return false;
    }
}