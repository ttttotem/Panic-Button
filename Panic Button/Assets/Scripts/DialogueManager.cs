using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    bool dialogueOpen = false;

    private Queue<string> sentences;
    bool alternator = true;
    string firstName;
    string secondName;

    bool couldShoot = false;

    GameObject player;
    TalkedToTracker tracker;

    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.Find("TalkedToTracker").GetComponent<TalkedToTracker>();
        player = GameObject.FindGameObjectWithTag("Player");
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if(dialogueOpen == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("space") || Input.GetButtonDown("Fire2"))
        {
            DisplayNextSentence();
        }
    }



    public void StartDialogue(Dialogue dialogue)
    {
        if(dialogueOpen == true)
        {
            return;
        }

        //Disable player
        player.GetComponent<PlayerMovement>().enabled = false;
        couldShoot = player.GetComponent<Shoot>().enabled;
        player.GetComponent<Shoot>().enabled = false;

        alternator = true;

        firstName = dialogue.name;
        secondName = dialogue.secondname;
        animator.SetBool("Active", true);
        StartCoroutine(LockinTimer());
        sentences.Clear();
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    //Prevents race case with Start Dialogue and Update
    IEnumerator LockinTimer()
    {
        yield return new WaitForSeconds(0.1f);
        dialogueOpen = true;
    }

    Coroutine prev;

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        alternator = !alternator;
        if (alternator == true && secondName != "")
        {
            nameText.text = secondName;
        } else if(alternator == false && secondName != "")
        {
            nameText.text = firstName;
        }

        string sentence = sentences.Dequeue();
        if (prev != null)
        {
            StopCoroutine(prev);
        }
        prev = StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void EndDialogue()
    {
        StartCoroutine(LockoutTimer());
        animator.SetBool("Active", false);
        //Check if this dialogue will cause an event
        tracker.spokeTo(firstName);
        tracker.CheckSpeachEvents();
        //Enable player
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Shoot>().enabled = couldShoot;
    }

    IEnumerator LockoutTimer()
    {
        yield return new WaitForSeconds(0.1f);
        dialogueOpen = false;
    }
}
