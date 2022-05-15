using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerThirstgauge : MonoBehaviour
{
    public static PlayerThirstgauge instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public Image Thirst;              // 갈증 게이지 UI
    public Text Gauge;                // 갈증 게이지 Text
    
    private float MaxThirst = 100;      // 최대 갈증 게이지
    public static float CurThirst;             // 현재 갈증 게이지

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

        Thirst.fillAmount = CurThirst / MaxThirst;
        Gauge.text = CurThirst.ToString();
    }

    public void Drink(int value)
    {
        CurThirst += value;
        if (CurThirst > MaxThirst) CurThirst = MaxThirst;
    }
}
