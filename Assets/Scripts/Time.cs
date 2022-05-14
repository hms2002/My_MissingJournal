using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    public Text Clock;                // 시계
    public Text Today;                // 생존 일자

    private int hour = 9;            // 시
    private int minute = 0;          // 분
    private bool afternoon = false;  // 오전 오후 여부
    private bool twelve = false;     // 정오 자정 구분
    private int day = 1;             // 일
    private string state = "AM";     // 오전 오후
    private float timer = 0;         // 게임 시간을 계산을위한 타이머

    void Update()
    { 
        timer += UnityEngine.Time.deltaTime;


        if (timer >= 0.03333333)
        {
            minute += 1;
            timer = 0;
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
