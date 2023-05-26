using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

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
        DontDestroyOnLoad(gameObject);


        if(pause != null && movement != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause.SetActive(!pause.activeSelf);

            }

            if (pause.activeSelf)
            {
                movement.enabled = false;
                movement.GetComponent<Animator>().SetFloat("Speed", 0f);
            }
            else if (!pause.activeSelf) movement.enabled = true;
        }
        
    }
}
