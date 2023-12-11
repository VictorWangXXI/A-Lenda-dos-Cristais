using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public GameObject characters;
    public GameObject options;
    public GameObject credits;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCharacters()
    {
        mainMenu.SetActive(false);
        characters.SetActive(true);
    }
    public void CloseCharacters()
    {
        mainMenu.SetActive(true);
        characters.SetActive(false);
    }

    public void ShowOptions()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
    }
    public void CloseOptions()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
    public void CloseCredits()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

}
