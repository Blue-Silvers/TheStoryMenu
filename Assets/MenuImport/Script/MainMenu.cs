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
    [SerializeField] private string SecondLevelToLoad;
    [SerializeField] private string ThirdLevelToLoad;
    [SerializeField] private string CreditsLoad;

    [Header("Windows")]
    [SerializeField] private GameObject settingsWindow, levelsWindow;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject ngButton, settingsButton, lvlButton;

    [Header("Background")]
    [SerializeField] private GameObject[] background;

    [Header("Pages")]
    [SerializeField] private GameObject[] page;
    int actualPage = 0;
    [SerializeField] int gamePage, mapPage, bestiairePage, florePage, logsPage, controlsPage, SettingsPage;

    [Header("Input Manager (don't touch)")]
    [SerializeField] private InputActionReference escapeM;
    private void Start()
    {
        Time.timeScale = 1;
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);

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
            CloseSettingsButton();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPage();
        }
    }
    public void Level1()
    {
        SceneManager.LoadScene(FirstLevelToLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
    }
    public void Level2()
    {
        SceneManager.LoadScene(SecondLevelToLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
    }
    public void Level3()
    {
        SceneManager.LoadScene(ThirdLevelToLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
    }

    public void SettingsButton()
    {
        settingsWindow.SetActive(true);
        levelsWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(settingsButton);
    }

    public void LevelSelectionButton()
    {
        levelsWindow.SetActive(true);
        settingsWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(lvlButton);
    }

    public void CloseSettingsButton()
    {
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
        eventSystem.SetSelectedGameObject(ngButton);
    }

    public void Credit()
    {
        SceneManager.LoadScene(CreditsLoad);
        settingsWindow.SetActive(false);
        levelsWindow.SetActive(false);
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
        CloseSettingsButton();
    }

    private void NextPage()
    {
        page[actualPage].SetActive(false);
        actualPage += 1;
        if (actualPage == page.Length)
        {
            actualPage = 0;
        }
        page[actualPage].SetActive(true);
    }
    private void PreviousPage()
    {
        page[actualPage].SetActive(false);
        actualPage -= 1;
        if (actualPage == -1)
        {
            actualPage = page.Length -1;
        }
        page[actualPage].SetActive(true);
    }
}
