using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftLine : MonoBehaviour
{
    CraftMenu craftMenu;

    public TextMeshProUGUI src1Text;
    public TextMeshProUGUI src2Text;

    public Button createBtn;

    public int needSrc1;
    public int needSrc2;
    public ItemList item;

    private void Start()
    {
        craftMenu = transform.parent.GetComponent<CraftMenu>();
    }

    public void UpdateLine(int woodNum, int stoneNum)   // �÷��̾ ������ �ִ� ����� ���� �ľ�
    {
        src1Text.text = woodNum.ToString() + " / " + needSrc1.ToString();
        src2Text.text = stoneNum.ToString() + " / " + needSrc2.ToString();

        if(woodNum < needSrc1 || stoneNum < needSrc2)
        {
            createBtn.interactable = false;
        }
        else
        {
            createBtn.interactable = true;
        }
    }

    public void CraftOnMenu()
    {
        craftMenu.Craft(needSrc1, needSrc2, item);
    }
}
