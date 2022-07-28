using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFader : MonoBehaviour
{
    AudioManager am;
    public string fadeOut;
    public float fadeTime;

    private void Start()
    {
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        StartCoroutine(am.Fade(fadeOut, fadeTime));
    }
}
