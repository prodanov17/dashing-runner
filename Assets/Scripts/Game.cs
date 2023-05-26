using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public static Game gameManager;

    [HideInInspector]
    public int level = 1;

    public GameObject pause;
    public PlayerMovement movement;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Update()
    {
        if (pause != null && movement != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause.SetActive(!pause.activeSelf);

            }

            if (pause.activeSelf)
            {
                //Time.timeScale = 0f;
                movement.enabled = false;
                movement.GetComponent<Animator>().SetFloat("Speed", 0f);
            }
            else if (!pause.activeSelf) movement.enabled = true;
        }

    }
}
