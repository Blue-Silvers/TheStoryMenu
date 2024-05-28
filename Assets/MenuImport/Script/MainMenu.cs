using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [Header("Level selection for button")]
    [SerializeField] private string FirstLevelToLoad;
    [SerializeField] private string CreditsLoad;

    [Header("Windows")]
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject ngButton, settingsButton, conrolButton;

    [Header("Background")]
    [SerializeField] private GameObject[] background;

    [Header("Pages")]
    [SerializeField] private GameObject[] page;
    int actualPage = 0;
    [SerializeField] int gamePage, mapPage, bestiairePage, florePage, logsPage, controlsPage, settingsPage;

    [Header("Input Manager (don't touch)")]
    [SerializeField] private InputActionReference escapeM;
    private void Start()
    {
        Time.timeScale = 1;

        int rdBackground = Random.Range(0, background.Length);
        background[rdBackground].SetActive(true);
        for (int i = 0; i < background.Length; i++)
        {
            if (background[i] != background[rdBackground])
            {
                background[i].SetActive(false);
            }
        }
        for (int i = 0; i < page.Length; i++)
        {
            if (page[i] != page[actualPage])
            {
                page[i].SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            GamePage();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            NextPage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            PreviousPage();
        }
    }
    public void GamePage()
    {
        SelectPage(gamePage);
        eventSystem.SetSelectedGameObject(ngButton);
    }
    public void MapPage()
    {
        SelectPage(mapPage);
    }
    public void BestiairePage()
    {
        SelectPage(bestiairePage);
    }
    public void FlorePage()
    {
        SelectPage(florePage);
    }
    public void LogsPage()
    {
        SelectPage(logsPage);
    }
    public void ControlsPage()
    {
        SelectPage(controlsPage);
    }
    public void SettingsButton()
    {
        SelectPage(settingsPage);
        eventSystem.SetSelectedGameObject(settingsButton);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(FirstLevelToLoad);
        print("ok");
    }

    public void Credit()
    {
        SceneManager.LoadScene(CreditsLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        escapeM.action.started += EscapeM;
    }
    private void EscapeM(InputAction.CallbackContext obj)
    {
        GamePage();
    }

    public void NextPage()
    {
        page[actualPage].SetActive(false);
        actualPage += 1;
        if (actualPage == page.Length)
        {
            actualPage = 0;
        }
        page[actualPage].SetActive(true);
    }
    public void PreviousPage()
    {
        page[actualPage].SetActive(false);
        actualPage -= 1;
        if (actualPage == -1)
        {
            actualPage = page.Length -1;
        }
        page[actualPage].SetActive(true);
    }

    private void SelectPage(int index)
    {
        page[actualPage].SetActive(false);
        actualPage = index;
        page[actualPage].SetActive(true);
    }
}
