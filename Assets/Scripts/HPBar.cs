using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GameObject prfHp;
    public GameObject Canvas;
    
    RectTransform Hpgauge;

    public float Height = 1f;

    void Start()
    {
        Hpgauge = Instantiate(prfHp, Canvas.transform).GetComponent<RectTransform>();
    }

    
    void Update()
    {
        Vector3 HpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + Height, 0));
        Hpgauge.position = HpbarPos;
    }
}
