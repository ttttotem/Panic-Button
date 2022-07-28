using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpinner : MonoBehaviour
{
    public float turnSpeed = 5;
    private void Update()
    {
        transform.Rotate(0, 0, 60 * Time.deltaTime * turnSpeed);
    }
}
