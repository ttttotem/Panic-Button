using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PoliceMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    public Rigidbody2D target;

    Vector2 movement;
    Vector2 mousePos;

    bool Wait = false;
    bool CatchingBreath = false;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath;
    Seeker seeker;

    public float jumpToBiteDistance = 2f;
    public float jumpMoveSpeed = 40f;

    public float nextWaypointDistance = 3f; //pathfinding
    public float catchBreathDuration = 2f;
    public float distanceToPlayerBuffer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (target == null || path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 lookDir = (Vector2) path.vectorPath[currentWaypoint] - rb.position;
        Vector2 targetAngleDir = target.position - rb.position;
        float angle = Mathf.Atan2(targetAngleDir.y, targetAngleDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        if(targetAngleDir.magnitude > distanceToPlayerBuffer && Wait == false)
        {
            rb.MovePosition(rb.position + (lookDir.normalized * moveSpeed * Time.fixedDeltaTime));
                
        } else
        {
            Wait = true;
            if(CatchingBreath == false)
            {
                StartCoroutine(CatchBreath());
            }
        }
        if(targetAngleDir.magnitude < jumpToBiteDistance)
        {
            rb.MovePosition(rb.position + (targetAngleDir * jumpMoveSpeed * Time.fixedDeltaTime));
        }
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
            
    }

    IEnumerator CatchBreath()
    {
        CatchingBreath = true;
        yield return new WaitForSeconds(catchBreathDuration);
        Wait = false;
        CatchingBreath = false;
    }
}
