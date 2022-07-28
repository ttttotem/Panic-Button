using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBox : MonoBehaviour
{
    bool playerInTrigger = false;
    public int dialogueNumber = 0;

    private void Update()
    {
        if (!playerInTrigger)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("space") || Input.GetButtonDown("Fire2"))
        {
            GetComponentInParent<DialogueTrigger>().TriggerDialogue(dialogueNumber);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
