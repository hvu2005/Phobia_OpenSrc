using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameManagerData _data;
    private GameObject canvas;
    private GameObject startCrossFade;
    private GameObject endCrossFade;
    //~~~~~~~~~~~~~SceneManage~~~~~~~~~~~~~~~~~~
    private string[] sceneName;
    private int sceneIndex = 0; 
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private GameState currentState;

    // Instantiate ScriptableObject Data
    private void InstantiateData()
    {
        canvas = Instantiate(_data.canvas);
        startCrossFade = Instantiate(_data.startCrossFade,canvas.transform);
        endCrossFade = Instantiate(_data.endCrossFade,canvas.transform);

        DontDestroyOnLoad(canvas);
        //DontDestroyOnLoad(startCrossFade);
        //DontDestroyOnLoad(endCrossFade);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Playing;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InstantiateData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.MainMenu:
                break;
        }
    }
    public void LoadSence()
    {
        sceneIndex++;
        SceneManager.LoadScene(_data.sceneName[sceneIndex]);
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
