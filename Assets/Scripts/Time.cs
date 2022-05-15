using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    public Text Clock;                // 시계
    public Text Today;                // 생존 일자

    private int hour = 8;            // 시
    private int minute = 0;          // 분
    private bool afternoon = false;  // 오전 오후 여부
    private bool twelve = false;     // 정오 자정 구분
    private int day = 1;             // 일
    private string state = "AM";     // 오전 오후
    private float timer1 = 0;         // 게임 시간을 계산을위한 타이머
    private float timer2 = 0;         // 게임 시간을 계산을위한 타이머

    void Update()
    { 
        timer1 += UnityEngine.Time.deltaTime;
        timer2 += UnityEngine.Time.deltaTime;

        if (timer1 >= 0.333)
        {
            minute += 1;
            timer1 = 0;
        }

        if (timer2 >= 40)
        {
            PlayerHungergauge.CurHunger -= 10;
            PlayerThirstgauge.CurThirst -= 10;
            timer2 = 0;
        }

        if ( minute == 60 )
        {
            hour += 1;
            minute = 0;
        }

        if ( hour == 12 && afternoon == false )
        {
            afternoon = true;
            state = "PM";
        }
        else if ( hour == 12 && twelve == true && afternoon == true )
        {
            hour = 0;
            day += 1;
            afternoon = false;
            twelve = false;
            state = "AM";
        }
        else if ( hour == 13 )
        {
            hour = 1;
            twelve = true;
        }

        Today.text = day.ToString() + "일차";

        Clock.text = state + " " + hour.ToString("D2") + " : " + minute.ToString("D2");

    }
}
