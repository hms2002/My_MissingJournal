using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(menuName = "ItemEft/Costomable/Club")]
public class ItemClubEffect : ItemEffects
{
    public int durability = 3;

    InventoryUI inventoryUI;
    Slider slider;

    public override bool ExecuteRole()
    {
        inventoryUI = InventoryUI.instance;

        Debug.Log("³»±¸µµ : " + durability);

        Transform slot = inventoryUI.transform.GetChild(inventoryUI.highlightSlotIdx);

        Transform sliderTransform = slot.GetChild(2);

        slider = sliderTransform.GetComponent<Slider>();

        slider.maxValue = 2;

        slider.value = slider.value - 1;

        if (slider.value <= 0)
        {
            slider.value = 1;
            sliderTransform.gameObject.SetActive(false);

            PlayerMovment.instance.StopHoldingClub();

            return true;
        }
        else
        {
            sliderTransform.gameObject.SetActive(true);
            return false;
        }
    }
}

