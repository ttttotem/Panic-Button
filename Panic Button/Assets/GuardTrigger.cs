using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject guard;
    public GameObject guardTalkBox;
    public GameObject teller;
    public Text text;
    public string instruction = "KILL THE GUARD";
    public string nextInstruction = "EXAMINE THE VAULT";
    public int dialogueNumber = 2;
    public GameObject door;
    public GameObject vault;
    bool killed = false;
    bool killable = false;
    Health health;
    DialogueTrigger dialogueTrigger;

    public float killDistance = 3f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && killed == false)
        {
            killable = true;
            guardTalkBox.SetActive(false);
            health = guard.GetComponent<Health>();
            dialogueTrigger = teller.GetComponent<DialogueTrigger>();
            text.text = instruction;
            StartCoroutine(ClearText());
        }
    }

    public void Update()
    {
        if (((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("space") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire1")) && killed == false && killable == true))
        {
            if((player.transform.position - guard.transform.position).magnitude < killDistance)
            {
                StopAllCoroutines();
                StartCoroutine(KillGuard());
            }
        }
    }

    IEnumerator KillGuard()
    {
        killed = true;
        if (health)
        {
            health.TakeDamage(1000f);
        }
        yield return new WaitForSeconds(2f);
        dialogueTrigger.TriggerDialogue(dialogueNumber);
        text.text = nextInstruction;
        StartCoroutine(ClearText());
        OpenDoor();
    }

    public void OpenDoor()
    {
        door.SetActive(false);
        vault.SetActive(true);
    }

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(3f);
        text.text = "";
    }
}
