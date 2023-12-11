using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour
{
    public GameObject[] panels;
    public Camera mainCamera;
    public float cameraTransitionSpeed = 5f;
    public float panelTransitionInterval = 3f;
    public string nextSceneName = "Fase1"; // Nome da cena a ser carregada após o painel 8

    private int currentPanelIndex = 0;
    private Coroutine autoAdvanceCoroutine;

    IEnumerator tra;

    void Start()
    {
        ShowPanel(currentPanelIndex);
        autoAdvanceCoroutine = StartCoroutine(AutoAdvancePanels());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPanelIndex++;
            ShowPanel(currentPanelIndex);
        }
    }

    void ShowPanel(int index)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }

        panels[index].SetActive(true);

        if (index == 6)
        {
            tra = TransitionCamera(new Vector3(0, 0, -10), new Vector3(0, -44.7f, -10));
            StartCoroutine(tra);
        }
        else
        {
            mainCamera.transform.position = new Vector3(0, 0, -10);
        }

        if (index == 7)
        {
            StopCoroutine(tra);
            StartCoroutine(AtrasoFinal());
        }
    }

    IEnumerator TransitionCamera(Vector3 startPosition, Vector3 targetPosition)
    {
        while (mainCamera.transform.position != targetPosition)
        {
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, targetPosition, Time.deltaTime * cameraTransitionSpeed);
            yield return null;
        }
    }

    IEnumerator AutoAdvancePanels()
    {
        while (true)
        {
            yield return new WaitForSeconds(panelTransitionInterval);

            currentPanelIndex++;
            if (currentPanelIndex >= panels.Length)
            {
                currentPanelIndex = 0;
            }

            ShowPanel(currentPanelIndex);
        }
    }

    private void OnDisable()
    {
        if (autoAdvanceCoroutine != null)
        {
            StopCoroutine(autoAdvanceCoroutine);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("Fase1");
    }

    IEnumerator AtrasoFinal()
    {
        yield return new WaitForSeconds(panelTransitionInterval);
        LoadNextScene();
    }
}
