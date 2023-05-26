using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private Vector3 startingPos;

    [HideInInspector]
    public bool open = false;
    [HideInInspector]
    public bool closed = false;
    public float startSpeed;
    public float doorSpeed = 2f;

    private void Start()
    {
        startSpeed = doorSpeed;
        startingPos = transform.position;
    }
    public void OpenDoor()
    {
        if (open)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0f, 2f, 0), doorSpeed * Time.fixedDeltaTime);
            if (transform.position == startingPos + new Vector3(0f, 2f, 0))
            {
                doorSpeed = 0f;
                open = false;
                AudioManager.instance.Stop("door");
            }
            else doorSpeed = startSpeed;
        }
    }
    public void CloseDoor()
    {
        if (closed)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, doorSpeed * Time.fixedDeltaTime);
            if (transform.position == startingPos)
            {
                doorSpeed = 0f;
                closed = false;
                AudioManager.instance.Stop("door");
            }
            else doorSpeed = startSpeed;
        }
    }

    private void FixedUpdate()
    {
        OpenDoor();
        CloseDoor();
    }

}
