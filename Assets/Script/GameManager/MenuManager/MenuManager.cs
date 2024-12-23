using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    private Stack<GameObject> menuStack = new Stack<GameObject>();


    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;


    [Header("Settings Menu")]
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject audioMenu;


    [Header("First Selected Options")]
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject controlsButton;
    [SerializeField] private GameObject lookUpKeybindingButton;
    private Dictionary<GameObject, GameObject> _firstSelectedOptions = new Dictionary<GameObject, GameObject>();


    private bool _isPaused;

    private void InitializeFirstSelectedOptions()
    {
        _firstSelectedOptions.Add(mainMenu, resumeButton);
        _firstSelectedOptions.Add(settingsMenu, controlsButton);
        _firstSelectedOptions.Add(controlsMenu, lookUpKeybindingButton);
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeFirstSelectedOptions();

        pauseOverlay.SetActive(false);
        controlsMenu.SetActive(false);
        audioMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.instance.pause)
        {
            if(!_isPaused)
            {
                _isPaused = true;
                Pause();
            }
            else
            {
                _isPaused = false;
                Unpause();
            }
        }
    }

    private void Pause() {
        Time.timeScale = 0f;
        pauseOverlay.SetActive(true);

        InputManager.instance.canGetAction = false;
        OnOpenMainMenu();
    }

    private void Unpause()
    {
        Time.timeScale = 1f;
        CloseAllMenus();
        pauseOverlay.SetActive(false);

        InputManager.instance.canGetAction = true;

    }
    public void OnOpenMainMenu()
    {
        PushToStack(mainMenu);
    }
    public void OnOpenSettingsMenu()
    {
        PushToStack(settingsMenu);

    }

    #region Settings Menu
    
    public void OnOpenControlsMenu()
    {
        PushToStack(controlsMenu);
    }

    public void OnOpenAudioMenu()
    {
        PushToStack(audioMenu);
    }
    #endregion

    public void OnBack()
    {
        menuStack.Pop().SetActive(false);
        menuStack.Peek().SetActive(true);

        EventSystem.current.SetSelectedGameObject(_firstSelectedOptions[menuStack.Peek()]);
    }

    public void OnResume()
    {
        _isPaused = false;
        Unpause();
    }

    private void CloseAllMenus()
    {
        while(menuStack.Count > 0)
        {
            menuStack.Pop().SetActive(false);
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    private void PushToStack(GameObject menu)
    {
        if(menuStack.Count > 0)
        {
            menuStack.Peek().SetActive(false);
        }

        menu.SetActive(true);
        menuStack.Push(menu);

        if (_firstSelectedOptions.ContainsKey(menu))
        {
            EventSystem.current.SetSelectedGameObject(_firstSelectedOptions[menu]);
        }
    }
}

