using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject startCrossFade;
    [SerializeField] private GameObject endCrossFade;
    //~~~~~~~~~~~~~SceneManage~~~~~~~~~~~~~~~~~~
    [SerializeField] private string[] sceneName;
    private int sceneIndex = 0; 
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private GameState currentState;

    // Instantiate ScriptableObject Data

    // Start is called before the first frame update
    void Awake()
    {
        currentState = GameState.Playing;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSence()
    {
        sceneIndex++;
        PlayerPrefs.SetInt("SceneIndex", sceneIndex);
        SceneManager.LoadScene(sceneName[sceneIndex]);
    }
    public void StartCrossFade(bool active)
    {
        startCrossFade.SetActive(active);
    }
    public void EndCrossFade(bool active)
    {
        endCrossFade.SetActive(active);
    }
}
public enum GameState
{
    MainMenu,
    Playing,
    Paused
}
