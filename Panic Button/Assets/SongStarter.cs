using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongStarter : MonoBehaviour
{
    public AudioManager am;
    public string song;
    // Start is called before the first frame update
    void Start()
    {
        am.Play(song);
    }
}
