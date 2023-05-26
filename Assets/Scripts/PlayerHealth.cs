using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public ParticleSystem smoke;
    public CameraFollow cameraFollow;
    public GameObject deathScreen;
    public Animator transition;

    public int startingCrystals;
    public int hearts;
    public Image[] heartImage;

    private Vector3 lastPos;

    private void Start()
    {
        hearts = 3;
        lastPos = GetComponent<Transform>().transform.position;
    }

    public void TakeDamage()
    {
        smoke.Play();
        AudioManager.instance.Play("Death");
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        transition.Play("Death");
        cameraFollow.enabled = false;

        hearts -= 1;
        heartImage[hearts].enabled = false;
        if (hearts <= 0)
        {
            Debug.Log("Death");
            Die();

        }
        else StartCoroutine(Respawn());


    }

    public void GainHealth()
    {
        AudioManager.instance.Play("health");
        heartImage[hearts].enabled = true;
        hearts += 1;
    }



    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.5f);
        cameraFollow.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Transform>().transform.position = lastPos;
        transition.Play("Respawn");
    }

    public void Die()
    {
        deathScreen.SetActive(true);
        cameraFollow.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        //Destroy(gameObject);
        //Debug.Log("Die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            lastPos = gameObject.GetComponent<Transform>().transform.position;
        }

        if (collision.CompareTag("Heart"))
        {
            if(hearts < 3)
            {
                GainHealth();
                Destroy(collision.gameObject);
            }

        }
    }
}
