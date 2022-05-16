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
"오늘은 즐거운 게임과 MT날!",
"MT의 뜻은 역시 (M)마시고 (T)토하고지 ㅋㅋㅋㅋㅋ ",
"신나게 마시고 토하자",
"제주도행 비행기 탑승하실게요!", // 3

"도착까지 1시간 걸린다고 했지?",
"음 그동안 잠이나 취하자",
"비상!...비상! 비행기 추락합니다",// 6
"뭐...뭐야!! 으아아아악",


"으.. 여긴 어디야..",
"이건 뭐지?",// 9
"배가 3일에 한번 지나가?",// 10
"일단 나가보자...",// 11

///////////////////// 초반 신 끝 //////////////////////

"엔딩 신",
"이 모든 것은 교수님의 시험이었다... 교수님이 고의적으로 우리를 무인도로 보냈고 탈출하는 시험이었다...",

"도장 신",
"그리고 난 A+를 받았다... "
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


            for (int i = 0; i < textFile[j].Length; i++)//해당 대사의 길이만큼 반복하며 텍스트를 점점 띄우기
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
