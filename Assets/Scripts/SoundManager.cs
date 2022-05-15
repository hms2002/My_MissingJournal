using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    public AudioClip BatCry;              // 박쥐 울음 소리
    public AudioClip BoarCrt;             // 멧돼지 울음 소리
    public AudioClip BeachWalk;           // 해변 걷는 소리
    public AudioClip CaveWalk;            // 동굴 걷는 소리
    public AudioClip PastureWalk;         // 파밍 지역 걷는 소리
    public AudioClip DaytimeBGM;          // 낮 BGM
    public AudioClip NightBGM;            // 밤 BGM
    public AudioClip DieBGM;              // 죽음 BGM
    public AudioClip DiscoverBGM;         // 단서 발견 시 BGM
    public AudioClip FirstBGM;            // 스토리 초반 컷신에 나오는 BGM
    public AudioClip Eat;                 // 먹는 소리
    public AudioClip Hit;                 // 몽둥이 휘둘러서 맞추는 소리
    public AudioClip Felling;             // 나무 캐는 소리
    public AudioClip Mining;              // 돌 캐는 소리
    public AudioClip Swing;               // 도구 휘두르는 소리

    public AudioSource BGM;
    public AudioSource audioSource;
}
