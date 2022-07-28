using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    public GameObject flashlight;
    bool alternator = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            alternator = !alternator;
            flashlight.SetActive(alternator);
        }
    }
}
