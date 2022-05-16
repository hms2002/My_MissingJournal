using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartManageMent : MonoBehaviour
{
    public Image backGround;
    public GameObject TextCanvas;
    public Image MainStoryImage;
    public Text text;

    public List<Sprite> storyImages;

    string[] textFile =
    {// 1 2 3
"������ ��ſ� ���Ӱ� MT��!",
"MT�� ���� ���� (M)���ð� (T)���ϰ��� ���������� ",
"�ų��� ���ð� ������",
"���ֵ��� ����� ž���ϽǰԿ�!", // 3

"�������� 1�ð� �ɸ��ٰ� ����?",
"�� �׵��� ���̳� ������",
"���!...���! ����� �߶��մϴ�",// 6
"��...����!! ���ƾƾƾ�",


"��.. ���� ����..",
"�̰� ����?",// 9
"�谡 3�Ͽ� �ѹ� ������?",// 10
"�ϴ� ��������...",// 11

///////////////////// �ʹ� �� �� //////////////////////

"���� ��",
"�� ��� ���� �������� �����̾���... �������� ���������� �츮�� ���ε��� ���°� Ż���ϴ� �����̾���...",

"���� ��",
"�׸��� �� A+�� �޾Ҵ�... "
    };


    bool isOnce = true;
    private void Start()
    {
        isOnce = true;
        TextCanvas.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnce == true)
        {
            isOnce = false;
            StartCoroutine("WhatStory");
        }
    }

    IEnumerator WhatStory()
    {
        while(backGround.color.a <= 1)
        {
            Color a = backGround.color;
            a.a += 0.01f;
            backGround.color = a;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        while (MainStoryImage.color.a <= 1)
        {
            Color a = MainStoryImage.color;
            a.a += 0.01f;
            MainStoryImage.color = a;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1);

        TextCanvas.SetActive(true);

        text.text = "";

        for(int j = 0; j < textFile.Length; j++)
        {
            switch (j)
            {
                case 3:

                    TextCanvas.SetActive(false);

                    while (MainStoryImage.color.a <= 1)
                    {
                        Color a = MainStoryImage.color;
                        a.a -= 0.01f;
                        MainStoryImage.color = a;
                        yield return new WaitForSeconds(0.01f);
                    }

                    MainStoryImage.sprite = storyImages[1];

                    yield return new WaitForSeconds(0.5f);
                    
                    while (MainStoryImage.color.a <= 0)
                    {
                        Color a = MainStoryImage.color;
                        a.a += 0.01f;
                        MainStoryImage.color = a;
                        yield return new WaitForSeconds(0.01f);
                    }

                    yield return new WaitForSeconds(1);

                    TextCanvas.SetActive(true);

                    break;
                case 6:
                    for(int i = 0; i < 5; i++)
                    {
                        MainStoryImage.sprite = storyImages[2];
                        yield return new WaitForSeconds(0.2f);
                        MainStoryImage.sprite = storyImages[1];
                        yield return new WaitForSeconds(0.2f);
                    }
                    MainStoryImage.sprite = storyImages[2];
                    break;
                case 8:
                    MainStoryImage.sprite = storyImages[3];
                    break;
                case 9:
                    TextCanvas.SetActive(false);

                    while (MainStoryImage.color.a >= 0)
                    {
                        Color a = MainStoryImage.color;
                        a.a -= 0.01f;
                        MainStoryImage.color = a;
                        yield return new WaitForSeconds(0.01f);
                    }

                    MainStoryImage.sprite = storyImages[3];

                    yield return new WaitForSeconds(0.5f);

                    while (MainStoryImage.color.a <= 1)
                    {
                        Color a = MainStoryImage.color;
                        a.a += 0.01f;
                        MainStoryImage.color = a;
                        yield return new WaitForSeconds(0.01f);
                    }

                    yield return new WaitForSeconds(1);

                    TextCanvas.SetActive(true);
                    break;
                case 10:
                    MainStoryImage.sprite = storyImages[4];
                    TextCanvas.SetActive(false);
                    yield return new WaitForSeconds(1);
                    TextCanvas.SetActive(true);
                    break;

                case 12:
                    while (MainStoryImage.color.a >= 0)
                    {
                        Color a = MainStoryImage.color;
                        a.a -= 0.01f;
                        MainStoryImage.color = a;
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    SceneManager.LoadScene(1);

                    yield break;
            }


            for (int i = 0; i < textFile[j].Length; i++)//�ش� ����� ���̸�ŭ �ݺ��ϸ� �ؽ�Ʈ�� ���� ����
            {
                if (Input.GetKey(KeyCode.Space))
                    break;

                //sizeCtrl.Fix(i + 1);
                //Debug.Log(bossDoorScenario[scriptNum].Length + "  " + i);
                text.text = textFile[j].Substring(0, i + 1);
                yield return new WaitForSeconds(0.1f);
            }

            text.text = textFile[j];

            yield return new WaitForSeconds(1f);
        }
    }
}
