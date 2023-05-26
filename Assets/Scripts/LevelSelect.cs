using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject menu;

    private void Update()
    {
        if (LevelManager.levelManager.level > 0) transform.GetChild(2).GetComponent<Button>().interactable = true;
        else transform.GetChild(2).GetComponent<Button>().interactable = false;

        if (LevelManager.levelManager.level > 1) transform.GetChild(3).GetComponent<Button>().interactable = true;
        else transform.GetChild(3).GetComponent<Button>().interactable = false;

        if (LevelManager.levelManager.level > 2) transform.GetChild(4).GetComponent<Button>().interactable = true;
        else transform.GetChild(4).GetComponent<Button>().interactable = false;
    }

    public void LoadLevel01()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadLevel02()
    {
        SceneManager.LoadScene("Level02");
    }

    public void LoadLevel03()
    {
        SceneManager.LoadScene("Level03");
    }

    public void Back()
    {
        menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
