using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Doors door;

    private int triggerCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerCount += 1;
        if (triggerCount == 1)
        {
            AudioManager.instance.Stop("door");
            AudioManager.instance.Play("door");
            door.closed = false;
            door.open = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerCount -= 1;
        if(triggerCount == 0)
        {
            AudioManager.instance.Stop("door");
            AudioManager.instance.Play("door");
            door.open = false;
            door.closed = true;
        }

    }
}
