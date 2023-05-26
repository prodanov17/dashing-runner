using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public KeyHolder kh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().Play("gateOpen");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
