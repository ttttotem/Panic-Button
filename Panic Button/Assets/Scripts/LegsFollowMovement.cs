using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsFollowMovement : MonoBehaviour
{

    Vector2 movement;
    // Start is called before the first frame update
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.magnitude);

        if(movement != new Vector2(0, 0))
        {
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * movement;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward,rotatedVectorToTarget);
            rotation *= Quaternion.Euler(0, 0, 90);
            transform.rotation = rotation;
        }
        
    }
}