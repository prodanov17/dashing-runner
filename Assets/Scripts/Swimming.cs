using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimming : MonoBehaviour
{
    public Transform pos;
    public PlayerMovement movement;
    public Rigidbody2D rb;

    public bool isSwimming = false;
    //private bool goingUp = false;

    public float swimSpeed = 3f;

    void FixedUpdate()
    {
        if (isSwimming)
        {
            Swim();
            if(Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical") == -1)
            {
                //rb.velocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Water")
        {
            movement.enabled = false;
            rb.gravityScale = -0.02f;

            isSwimming = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            movement.enabled = true;
            rb.gravityScale = 1f;
            isSwimming = false;
        }
    }

    void Swim()
    {
        Vector3 swim = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        pos.transform.position += swim * Time.fixedDeltaTime * swimSpeed;
    }

}
