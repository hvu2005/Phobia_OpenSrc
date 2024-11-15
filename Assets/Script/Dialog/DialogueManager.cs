using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private DialogueData _data;

    private Text dialogText;
    private GameObject dialogPanel;
    //private GameObject avatar;
    [SerializeField] private string[] dialogue;
    private Queue<string> paragraphs = new Queue<string>();
    private bool isTyping;

    private void InstantiateData()
    {
        dialogPanel = Instantiate(_data.dialogPanel, GameManager.instance.canvas.transform);
        dialogText = Instantiate(_data.dialogText.GetComponent<Text>(), dialogPanel.transform);
        dialogText.GetComponent<Text>().text = "";
        //avatar = Instantiate(avatar, dialogPanel.transform);
        dialogPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
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
        InstantiateData();
        OpenDialogue(dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.instance.isGetAnyKeyDown && !isTyping)
        {
            NextLine();
        }
    }
    public void OpenDialogue(string[] conversation)
    {
        foreach(string dialogue in conversation)
        {
            paragraphs.Enqueue(dialogue);
        }
        dialogPanel.SetActive(true);
        StartCoroutine(Typing(paragraphs.Dequeue()));
    }
    private IEnumerator Typing(string s)
    {
        isTyping = true;
        yield return new WaitForSeconds(0.75f);
        foreach(char c in s)
        {
            dialogText.GetComponent<Text>().text += c;
            yield return new WaitForSeconds(0.01f);
        }
        isTyping=false;
    }
    private void NextLine()
    {
        if(paragraphs.Count > 0)
        {
            dialogText.GetComponent<Text>().text = "";
            StartCoroutine(Typing(paragraphs.Dequeue()));
        }
        else
        {
            dialogText.GetComponent<Text>().text = "";
            dialogPanel.SetActive(false);
        }
    }
}
