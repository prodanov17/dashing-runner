using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public CameraShake cameraShake;
    public AudioManager audioManager;
    public ParticleSystem drop;

    public LayerMask Activator;
    public float distance;

    private bool falling = false;


    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).position, Vector2.down, distance, Activator);
        Debug.DrawRay(transform.GetChild(0).position, Vector2.down * distance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                DropSpike();
            }
        }

        //Landed
        if (falling)
        {
            if (GetComponent<Rigidbody2D>().velocity.y > -.1f && GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                StartCoroutine(cameraShake.Shake(.15f, .2f));
                
                falling = false;
            }
        }
    }

    void DropSpike()
    {
        drop.Play();
        StartCoroutine(cameraShake.Shake(.15f, .2f));
        falling = true;
        GetComponent<Rigidbody2D>().gravityScale = 2f;
        Destroy(this.gameObject, 2f);
    }

}
