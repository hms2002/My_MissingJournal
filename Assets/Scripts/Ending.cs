using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text text3;
    public Transform cutscene;
    public Transform Anim;
    private float data = 0;

    
    
    void Update()
    {   
        data += UnityEngine.Time.deltaTime;
        
        if (data >= 3)
        {
            cutscene.position = new Vector3(0, 0.25f, 1);
        }

        if (data >= 6)
        {
            text1.text = "이 모든 것은 교수님의 시험이었다... ";
        }
        
        if (data >= 9)
        {
            text2.text = "교수님이 고의적으로 우리를 무인도로";
        }

        if (data >= 15)
        {
            text3.text = "보냈고 탈출하는 시험이었다...";
        }

        if (data >= 18)
        {
            cutscene.position = new Vector3(12, 5, 1);
            text1.text = "";
            text2.text = "";
            text3.text = "";
        }

        if (data >= 21)
        {
            Anim.position = new Vector3(0, 0.25f, 2);
        }

        if (data >= 24)
        {
            text2.text = "그리고 난 A+를 받았다... ";
        }
    }
}
