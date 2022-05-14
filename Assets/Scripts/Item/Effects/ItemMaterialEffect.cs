using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = "ItemEft/Costomable/Material")]
public class ItemMaterialEffect : ItemEffects
{


    public override bool ExecuteRole()
    {
        if (Inventory.instance.onCreate)
        {
            Debug.Log("이게 되네;;");
            return true;
        }
        else
        {
            Debug.Log("당연히 안 되지");
            return false;
        }
    }
}
