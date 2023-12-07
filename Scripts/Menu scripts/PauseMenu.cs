using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public CanvasGroup hud;
    public static bool isPaused;
    public TextMeshProUGUI pauseText;

    public int sceneToLoad;

    public GameObject continueButton;
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        pauseText.text = "Pausa";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        hud.interactable = false;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        hud.interactable = true;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Win()
    {
        pauseText.text = "Vitoria!";
        pauseMenu.SetActive(true);
        continueButton.SetActive(false);
        hud.interactable = false;
        Time.timeScale = 0f;
    }

    public void Derrota()
    {
        pauseText.text = "Derrota!";
        pauseMenu.SetActive(true);
        continueButton.SetActive(false);
        hud.interactable = false;
        Time.timeScale = 0f;
    }
}
