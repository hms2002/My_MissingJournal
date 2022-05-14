using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHungergauge : MonoBehaviour
{
    public Image Hunger;              // 허기 게이지 UI
    public Text Gauge;                // 허기 게이지 Text

    public int CurHunger;             // 현재 허기 게이지
    private int MaxHunger = 100;      // 최대 허기 게이지

    void Start()
    {
        CurHunger = MaxHunger;
        Hunger.fillAmount = CurHunger;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CurHunger -= 10;
        }

        if ( CurHunger < 0 )              // 허기 음수 방지
        {
            CurHunger = 0;
        } 

        Hunger.fillAmount = (float) CurHunger / (float) MaxHunger;
        Gauge.text = CurHunger.ToString();
    }
}
