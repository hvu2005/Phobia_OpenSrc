using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameManagerData _data;

    private GameObject startCrossFade;
    private GameObject endCrossFade;
    //~~~~~~~~~~~~~SceneManage~~~~~~~~~~~~~~~~~~
    private string[] sceneName;
    private int sceneIndex; 
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private GameState currentState;

    // Instantiate ScriptableObject Data
    private void InstantiateData()
    {
        startCrossFade = Instantiate(_data.StartCrossFade);
        endCrossFade = Instantiate(_data.EndCrossFade);
        sceneName = _data.sceneName;

        DontDestroyOnLoad(_data.StartCrossFade);
        DontDestroyOnLoad(_data.EndCrossFade);
    }
    // Start is called before the first frame update
    void Start()
    {

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Map1");
        }
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
