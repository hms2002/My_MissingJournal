using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    string name;
    Image itemSprite;

    void Init(string tempName, Image tempImg)
    {
        name = tempName;
        itemSprite = tempImg;
    }
}
