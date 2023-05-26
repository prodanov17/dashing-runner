using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject options;
    public GameObject levels;

    public void PlayGame()
    {
        levels.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Options()
    {
        options.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
