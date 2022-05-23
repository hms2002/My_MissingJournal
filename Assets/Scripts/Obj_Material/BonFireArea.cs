using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonFireArea : MonoBehaviour
{
    const int needWoodCnt = 3;
    const int needStoneCnt = 1;


    public GameObject cookBtn;
    public GameObject addMaterialBtn;

    Inventory inven;

    private void Start()
    {
        cookBtn.SetActive(false);
        addMaterialBtn.SetActive(false);
        inven = Inventory.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cookBtn.SetActive(true);

            addMaterialBtn.SetActive(true);

            int woodCnt = 0, stoneCnt = 0;

            foreach (Slot slot in inven.slots)
            {
                if (slot.isEmpty)
                    continue;
                if (slot.item.itemType == ItemType.Material)
                {
                    if (slot.item.name == "Branch")
                    {
                        woodCnt += slot.stack;
                    }
                    else if (slot.item.name == "Stone")
                    {
                        stoneCnt += slot.stack;
                    }
                }
            }

            if (woodCnt >= needWoodCnt && stoneCnt >= needStoneCnt)
            {
                Debug.Log("Hello");
                addMaterialBtn.GetComponent<Button>().interactable = true;
            }   
            else
            {
                addMaterialBtn.GetComponent<Button>().interactable = false;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cookBtn.SetActive(false);


            addMaterialBtn.SetActive(false);
        }
    }
}
