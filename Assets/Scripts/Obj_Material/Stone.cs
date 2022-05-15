using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour
{
    AudioSource audioSource;

    public Button btn;
    public Image coolTimeImg;
    public Slider fellSlider;
    Animator anim;

    Inventory inven;

    float waitTime = 5;
    float pickingTime;
    float curTime;

    bool onPicking;
    bool isWaitingTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();

        anim = GetComponent<Animator>();

        waitTime = 5f;

        fellSlider.gameObject.SetActive(false);

        inven = Inventory.instance;

        isWaitingTime = false;
        curTime = 0;
    }
    float x;
    private void Update()
    {
        // ���� ĳ�� ��Ÿ��
        if (onPicking)
        {
            if (audioSource.isPlaying == false)
                audioSource.Play();

            x = Input.GetAxisRaw("Horizontal");

            // ĳ�� ���߿� �Է¹����� ����
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || x != 0 || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6))
            {
                audioSource.Stop();

                PlayerMovment.instance.StopPicking();

                onPicking = false;

                // ĳ�� ���൵ �����̴� ����
                fellSlider.gameObject.SetActive(false);

                // ��Ÿ�� �̹��� �ѱ�
                coolTimeImg.transform.parent.gameObject.SetActive(true);
                coolTimeImg.fillAmount = 1;

                // ��ư ���̸� ��ȣ�ۿ� �����ϵ��� �ٲٰ�, ��ư ���������� ��� �Ѽ� ��ȣ�ۿ� �����ϰ� �ٲٰ� �ٽ� ����
                if (btn.gameObject.activeSelf == true)
                    btn.interactable = true;
                else
                {
                    btn.gameObject.SetActive(true);
                    btn.interactable = true;
                    btn.gameObject.SetActive(false);
                }

                return;
            }


            curTime += UnityEngine.Time.deltaTime;
            fellSlider.value = curTime;


            // �� ĳ�� ������ ��
            if (curTime >= pickingTime)
            {
                audioSource.Stop();
                anim.SetInteger("Step", 2);
                PlayerMovment.instance.StopPicking();
                onPicking = false;
                curTime = 0;

                isWaitingTime = true;

                fellSlider.gameObject.SetActive(false);

                coolTimeImg.transform.parent.gameObject.SetActive(true);

                DropItem();
            }
            else if(curTime >= pickingTime/2)
            {
                anim.SetInteger("Step", 1);
            }
        }
        // ��� �ð� ��Ÿ��
        else if (isWaitingTime == true)
        {
            curTime += UnityEngine.Time.deltaTime;

            coolTimeImg.fillAmount = curTime / waitTime;

            if (curTime > waitTime)
            {

                anim.SetInteger("Step", 0);

                isWaitingTime = false;

                curTime = 0;

                btn.interactable = true;
            }
        }
    }

    public void Picking()
    {
        Debug.Log("This is");
        if (isWaitingTime == true) return;

        Inventory inven = Inventory.instance;

        if (inven.slots[inven.highlightSlotIdx].item == null) ;
        else if (inven.slots[inven.highlightSlotIdx].item.name == "FullPail")
        {
            inven.slots[inven.highlightSlotIdx].RemoveSlot();
            inven.AddItem(ItemDatabase.instance.itemDB[(int)ItemList.Pail], inven.highlightSlotIdx);
        }


        btn.interactable = false;

        coolTimeImg.fillAmount = 0;
        coolTimeImg.transform.parent.gameObject.SetActive(false);

        onPicking = true;
        fellSlider.gameObject.SetActive(true);

        if (inven.slots[inven.highlightSlotIdx].item == null) ;
        else if (inven.slots[inven.highlightSlotIdx].item.name == "Pick")
        {
            audioSource.pitch = 2;
            pickingTime = 10;
        }
        else
        {
            audioSource.pitch = 1;
            pickingTime = 15;
        }

        fellSlider.maxValue = pickingTime;
        
        PlayerMovment.instance.Picking();
    }

    public void DropItem()
    {
        ItemDatabase.instance.DropItem((int)ItemList.Stone, transform.position);
    }
}