using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private Text dialogText;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private string[] dialogue;
    private Queue<string> paragraphs = new Queue<string>();
    private bool isTyping;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        OpenDialogue(dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.isGetAnyKeyDown && !isTyping)
        {
            NextLine();
        }
    }
    private void OpenDialogue(string[] conversation)
    {
        //PlayerBehave.instance.canMove = false;
        InputManager.instance.canGetAction = false;
        foreach (string dialogue in conversation)
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
        foreach (char c in s)
        {
            dialogText.GetComponent<Text>().text += c;
            yield return new WaitForSeconds(0.01f);
        }
        isTyping = false;
    }
    private void NextLine()
    {
        if (paragraphs.Count > 0)
        {
            dialogText.GetComponent<Text>().text = "";
            StartCoroutine(Typing(paragraphs.Dequeue()));
        }
        else
        {
            dialogText.GetComponent<Text>().text = "";
            dialogPanel.SetActive(false);
            //PlayerBehave.instance.canMove = true;
            InputManager.instance.canGetAction = true;
        }
    }

    public void SetUpConversation(string[] converstation)
    {
        OpenDialogue(converstation);
    }
}
