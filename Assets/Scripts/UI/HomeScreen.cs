using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreen : MonoBehaviour
{
    public GameObject homePane;
    public GameObject optionsPane;
    public string scene;
    public enum Screen
    {
        HOME,
        OPTIONS
    };
    public Screen state;

    private void Start()
    {
        OpenHome();
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenOptions()
    {
        state = Screen.OPTIONS;
        homePane.SetActive(false);
        optionsPane.SetActive(true);
    }

    public void OpenHome()
    {
        state = Screen.HOME;
        homePane.SetActive(true);
        optionsPane.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
