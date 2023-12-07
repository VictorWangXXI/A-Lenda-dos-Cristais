using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controleCinematica : MonoBehaviour
{

    public GameObject pauseButton;
    public GameObject continueButton;

    public void Pause()
    {
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        continueButton.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;

    }

    public void Skip()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Fase1");
    }
}
