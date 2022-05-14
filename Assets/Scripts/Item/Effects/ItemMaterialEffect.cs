using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "ItemEft/Costomable/Material")]
public class ItemMaterialEffect : ItemEffects
{


    public override bool ExecuteRole()
    {
        if (Inventory.instance.onCreate)
            return true;
        else
            return false;
    }
}
