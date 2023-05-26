using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public void Play()
    {
        transform.GetChild(0).GetComponent<TrailRenderer>().enabled = true;
        transform.GetChild(1).GetComponent<TrailRenderer>().enabled = true;
        transform.GetChild(2).GetComponent<TrailRenderer>().enabled = true;
    }

    public void Stop()
    {
        transform.GetChild(0).GetComponent<TrailRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<TrailRenderer>().enabled = false;
        transform.GetChild(2).GetComponent<TrailRenderer>().enabled = false;
    }
}
