using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text speakerName;

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
    }
    private IEnumerator StartConversation()
    {
        while (paragraphs.Count > 0)
        {
            yield return new WaitUntil(() => InputManager.instance.isGetAnyKeyDown && !isTyping);
            NextLine();
        }
        //end conversation
        yield return new WaitUntil(() => InputManager.instance.isGetAnyKeyDown && !isTyping);
        NextLine();
    }
    private void OpenDialogue(ConversationData conversation)
    {

        InputManager.instance.canGetAction = false;

        foreach (string dialogue in conversation.paragraphs)
        {
            paragraphs.Enqueue(dialogue);
        }
        speakerName.text = conversation.speakerName + ":";
        dialogPanel.SetActive(true);

        StartCoroutine(StartConversation());
    }
    private IEnumerator Typing(string s)
    {
        isTyping = true;
        yield return new WaitForSeconds(0.75f);
        foreach (char c in s)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }
    private void NextLine()
    {
        if (paragraphs.Count > 0)
        {
            dialogText.text = "";
            StartCoroutine(Typing(paragraphs.Dequeue()));
        }
        else
        {
            dialogText.text = "";
            dialogPanel.SetActive(false);
            InputManager.instance.canGetAction = true;
        }
    }

    public void SetUpConversation(ConversationData converstation)
    {
        OpenDialogue(converstation);
    }
}
