using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer sr;
    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;
    public GameObject cutscene;
    private float data = 0;
    
    void Start()
    {
        sr = cutscene.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (data == 0)
        {
            StartCoroutine("FadeIn");
        }

        data += UnityEngine.Time.deltaTime;

        if (data >= 5)
        {
            text1.text = "이 모든 것은 교수님의 시험이었다... ";
        }
        
        if (data >= 10)
        {
            text2.text = "교수님이 고의적으로 우리를 무인도로";
        }

        if (data >= 15)
        {
            text3.text = "보냈고 탈출하는 시험이었다...";
        }
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i < 10; ++i)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOUT()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
