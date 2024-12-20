using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    [Header("First Selected Options")]
    [SerializeField] private GameObject resumeButton;



    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.instance.pause)
        {
            if(!isPaused)
            {
                isPaused = true;
                Pause();
            }
            else
            {
                isPaused = false;
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
        settingsMenu.SetActive(false); 
    }
    public void OnOpenMainMenu()
    {

        mainMenu.SetActive(true);   
        settingsMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(resumeButton);
    }
    public void OnOpenSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnResume()
    {
        Unpause();
    }

    private void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
