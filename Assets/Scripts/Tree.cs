using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public float coolTime;
    float curTime;

    bool onFalling;
    bool canFall;

    private void Start()
    {
        curTime = 0;
    }

    private void Update()
    {
        
    }

    void Felling()
    {
        if (canFall == false) return;

    }
}