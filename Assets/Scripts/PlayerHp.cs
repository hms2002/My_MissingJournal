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

    public static PlayerHp instance;

    float autoHealCoolTime;
    float autoHealCurTime;
    int autoHealValue;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        autoHealCoolTime = 1f;
        autoHealValue = 10;

    }
    void Start()
    {
        CurHp = MaxHp;
    }

    void Update()
    {
        autoHealCurTime -= UnityEngine.Time.deltaTime;

        if(PlayerMovment.instance.isCaveGround)
        {
            if(PlayerHungergauge.CurHunger >= 60 && PlayerThirstgauge.CurThirst >= 60)
            {
                if(autoHealCurTime <= 0 && CurHp < MaxHp)
                {
                    autoHealCurTime = autoHealCoolTime;
                    Heal(3);

                    PlayerThirstgauge.CurThirst -= 1;
                    PlayerHungergauge.CurHunger -= 1;
                }
            }
        }

        if ( Input.GetKeyDown(KeyCode.I) )    // 테스트 코드
        {
            gde.AttackPlayer();
        }

        if ( CurHp <= 0 )              // HP 음수 방지
        {
            CurHp = 0;
            PlayerMovment.instance.PlayerDie();
        }   

        HpBar.value = CurHp;
        HpText.text = CurHp.ToString() + " / " + MaxHp.ToString();
    }

    public void Heal(int value)
    {
        CurHp += value;

        if (CurHp > 100) CurHp = MaxHp;
    }
}
