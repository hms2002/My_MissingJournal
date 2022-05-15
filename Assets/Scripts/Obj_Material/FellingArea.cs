using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FellingArea : MonoBehaviour
{
    public GameObject fellBtn;
    private void Start()
    {
        fellBtn.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            fellBtn.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fellBtn.SetActive(false);
        }
    }
}
