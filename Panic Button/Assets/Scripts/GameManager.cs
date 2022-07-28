using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator transition;
    public Text text;
    public string[] transitionText;
    public string[] transitionSoundEffectName;
    public AudioManager am;


    public GameObject deadText;
    bool dead = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            if(dead == true)
            {
                //Reload current scene
                dead = false;
                Time.timeScale = 1f;
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
            }
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        text.enabled = true;
        if(levelIndex-1< transitionText.Length)
        {
            text.text = transitionText[levelIndex - 1];
        } else
        {
            text.text = "";
        }        

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        if (levelIndex - 1 < transitionSoundEffectName.Length)
        {
            am.Play(transitionSoundEffectName[levelIndex - 1]);
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelIndex);
        transition.SetTrigger("End");
        text.enabled = false;
    }

    public void PlayerDied()
    {
        Time.timeScale = 0f;
        deadText.SetActive(true);
        dead = true;
    }
}
