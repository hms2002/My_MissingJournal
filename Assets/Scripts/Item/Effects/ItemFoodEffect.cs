using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "ItemEft/Costomable/Food")]
public class ItemFoodEffect : ItemEffects
{
    public float eatAmount;
    public float drinkAmount;

    public override bool ExecuteRole()
    {
        if (Inventory.instance.onCreate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
