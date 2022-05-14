using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider HpBar;        // HP바 UI
    public Text HpText;         // HP Text
    public static int CurHp;    // 현재 HP
    private int MaxHp = 100;    // 최대 HP

    GroundEnemy gde = new GroundEnemy();

    void Start()
    {
        CurHp = MaxHp;
    }

    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.I) )
        {
            gde.AttackPlayer();
            HpBar.value = CurHp;
        }

        if ( CurHp < 0 )              // HP 음수 방지
        {
            CurHp = 0;
        }   

        HpText.text = CurHp.ToString() + " / " + MaxHp.ToString();
    }
}
