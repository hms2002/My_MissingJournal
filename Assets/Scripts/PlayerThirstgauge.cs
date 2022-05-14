using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThirstgauge : MonoBehaviour
{
    public Image Thirst;              // 갈증 게이지 UI
    public Text Gauge;                // 갈증 게이지 Text
    
    public int CurThirst;             // 현재 갈증 게이지
    private int MaxThirst = 100;      // 최대 갈증 게이지

    void Start()
    {
        CurThirst = MaxThirst;
        Thirst.fillAmount = CurThirst;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CurThirst -= 10;
        }

        if ( CurThirst < 0 )              // 갈증 음수 방지
        {
            CurThirst = 0;
        } 

        Thirst.fillAmount = (float) CurThirst / (float) MaxThirst;
        Gauge.text = CurThirst.ToString();
    }
}