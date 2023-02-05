using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool rotate = true;
    public Camera Cm;
    public static bool GameIsPause = false;
    public GameObject PauseMenuUI;
    // Update is called once per frame
    void Update()
    {
        if (GameIsPause)
        {
            PauseMenuUI.SetActive(true);
        }
        else
        {
            PauseMenuUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Restart");
    }

    public void LoadMenu()
    {
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(false);
        GameIsPause = false;
        SceneManager.LoadScene("Start Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void RotateCam()
    {
        if (rotate)
        {
            Cm.transform.Rotate(new Vector3(0, 0, 90));
            rotate = false;
        }
        else
        {
            rotate = true;
            Cm.transform.Rotate(new Vector3(0, 0, -90));
        }
    }
}
