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
        PlayerHungergauge.CurHunger += eatAmount;
        PlayerThirstgauge.CurThirst += drinkAmount;

        return true;
    }
}
