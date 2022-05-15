using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    public static SoundManager instance;

    public AudioClip DaytimeBGM;
    public AudioClip NightBGM;

    public AudioClip BoarCrt;             // 멧돼지 울음 소리
    public AudioClip DieBGM;              // 죽음 BGM
    public AudioClip DiscoverBGM;         // 단서 발견 시 BGM
    public AudioClip FirstBGM;            // 스토리 초반 컷신에 나오는 BGM
    public AudioClip Eat;                 // 먹는 소리
    public AudioClip Hit;                 // 몽둥이 휘둘러서 맞추는 소리
    public AudioClip Felling;             // 나무 캐는 소리
    public AudioClip Mining;              // 돌 캐는 소리
    public AudioClip Swing;               // 도구 휘두르는 소리

    public AudioSource audioSource;

    void Start()
    {
        audioSource.loop = true;

        audioSource.clip = DaytimeBGM;
        audioSource.Play();
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MainBGM(int type)
    {
        /* if (type == 0)
        {
            audioSource.clip = DaytimeBGM;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = NightBGM;
            audioSource.Play();
        } */
    }
}
