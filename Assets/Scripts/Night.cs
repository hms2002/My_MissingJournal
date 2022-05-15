using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : MonoBehaviour
{
    public SpriteRenderer sr;

    public Color day;
    public Color night;

    public float oneDay;
    public float currentTime = 60;

    [Range(0.01f, 0.2f)]
    public float transitionTime;

    bool isSwap = false;

    private void Awake()
    {
        float spritex = sr.sprite.bounds.size.x;
        float spritey = sr.sprite.bounds.size.y;

        float screenY = Camera.main.orthographicSize * 2;
        float screenX = screenY / Screen.height * Screen.width;

        transform.localScale = new Vector2(Mathf.Ceil(screenX / spritex), Mathf.Ceil(screenY / spritey));

        sr.color = day;
    }

    void Update()
    {
        currentTime += UnityEngine.Time.deltaTime;
        if (currentTime >= oneDay)
            currentTime = 0;

        if (!isSwap)
        {
            if (Mathf.FloorToInt(oneDay * 0.4f) == Mathf.FloorToInt(currentTime))
            {
                isSwap = true;
                StartCoroutine(SwapColor(sr.color, night));
            }   
            else if (Mathf.FloorToInt(oneDay * 0.9f) == Mathf.FloorToInt(currentTime))
            {
                isSwap = true;
                StartCoroutine(SwapColor(sr.color, day));
            }
        }

        
    }

    IEnumerator SwapColor(Color Start, Color end)
    {
        float t = 0;
        while (t < 1)
        {
            t += UnityEngine.Time.deltaTime * (1/(transitionTime * oneDay));
            sr.color = Color.Lerp(Start, end, t);
            yield return null;
        }
        isSwap = false;
    }
}
