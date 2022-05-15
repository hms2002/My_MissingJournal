using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBoxArea : MonoBehaviour
{
    public Button fellBtn;
    private void Start()
    {
        fellBtn.interactable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fellBtn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fellBtn.interactable = false;
        }
    }
}
