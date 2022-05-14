using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : MonoBehaviour
{
    public static ToolBox instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
