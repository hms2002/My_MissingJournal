using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class OpenDieUI : MonoBehaviour
{
    public static OpenDieUI instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    public Image background;
    public TextMeshProUGUI text;

    public void start()
    {
        StartCoroutine("FadeDieUI");
    }

    IEnumerator FadeDieUI()
    {
        Debug.Log("D");
        while(background.color.a <= 1)
        {
            Debug.Log("Dd");
            Color a = background.color;
            a.a += 0.01f;
            background.color = a;
            yield return new WaitForSeconds(0.01f);
        }

        while (text.color.a <= 1)
        {
            Debug.Log("Dfasd");
            Color a = text.color;
            a.a += 0.01f;
            text.color = a;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(1);
    }
}
