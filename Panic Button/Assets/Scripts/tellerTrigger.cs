using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class tellerTrigger : MonoBehaviour
{
    public Text text;
    public string instruction = "TALK TO THE GUARD";
    public GameObject teller;
    public GameObject vault;
    public GameObject player;
    public GameObject guard;
    public CinemachineVirtualCamera cam1;

    PlayerMovement movement;
    PoliceMovement policeMovement;

    public Dialogue dialogue;

    private void Start()
    {
       
        movement = player.GetComponent<PlayerMovement>();
        policeMovement = guard.GetComponent<PoliceMovement>();
        StartCoroutine(GuardCutScene());
    }

    IEnumerator GuardCutScene()
    {
        movement.enabled = false;
        
        cam1.Follow = teller.transform;
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitForSeconds(2f);
        cam1.Follow = guard.transform;
        yield return new WaitForSeconds(2f);
        policeMovement.enabled = true;
        yield return new WaitForSeconds(2f);
        text.text = instruction;
        StartCoroutine(ClearText());
        cam1.Follow = player.transform;
        movement.enabled = true;
        
    }

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(3f);
        text.text = "";
    }
}
