using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private GameObject startCrossFade;
    [SerializeField] private GameObject endCrossFade;
    //~~~~~~~~~~~~~SceneManage~~~~~~~~~~~~~~~~~~
    [SerializeField] private string[] sceneName;
    private int sceneIndex = 0;
    //~~~~~~~~~~~~~~~TakeDmg~~~~~~~~~~~~~~~~~~~~
    [SerializeField] private Image takeDmg;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [SerializeField] private TextMeshProUGUI hpText;
    //~~~~~~~~~~~~~~~~Player~~~~~~~~~~~~~~~~~~~~
    public GameObject player { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

    public void UpdatePlayerHp(int hp)
    {
        hpText.text = hp.ToString();
    }

    public void TakeDmgScreen()
    {
        StartCoroutine(TakeDmgCoroutine());
    }
    private IEnumerator TakeDmgCoroutine()
    {
        takeDmg.gameObject.SetActive(true);

        takeDmg.DOFade(1f, 0.2f);
        yield return new WaitForSeconds(0.5f);
        takeDmg.DOFade(0f, 1.5f).OnComplete(() =>
        {
            takeDmg.gameObject.SetActive(false);
        });
    }
}