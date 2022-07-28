using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeistTrigger : MonoBehaviour
{

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm.LoadNextScene();
    }

   
}
