using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public bool isEndedConversation {  get; private set; } 

    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private TMP_Text speakerName;

    [SerializeField] private GameObject avatar;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private string[] dialogue;

    private Queue<string> _paragraphs = new Queue<string>();
    private Queue<ConversationData> _conversationQueue = new Queue<ConversationData>();

    private bool _isTyping;
    private Coroutine _typingCoroutine;
    private Coroutine _skipTypingCoroutine;
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
        while (_paragraphs.Count > 0)
        {
            NextLine();
            yield return new WaitUntil(() => InputManager.instance.isGetAnyKeyDown && !_isTyping);
        }
        //end conversation
        yield return new WaitUntil(() => InputManager.instance.isGetAnyKeyDown && !_isTyping);
        NextLine();
    }
    private void OpenDialogue(ConversationData conversation)
    {
        isEndedConversation = false;

        //InputManager.instance.canGetAction = false;

        foreach (string dialogue in conversation.paragraphs)
        {
            _paragraphs.Enqueue(dialogue);
        }
        speakerName.text = conversation.speakerName + ":";
        
        dialogPanel.SetActive(true);
        SetAvatarSide(conversation);
        dialogPanel.transform.DOScaleY(2.5f, 0.3f);

        StartCoroutine(StartConversation());
    }
    private void SetAvatarSide(ConversationData conversation)
    {
        avatar.GetComponent<Image>().sprite = conversation.avatar;
        avatar.GetComponent<Image>().SetNativeSize();
        switch (conversation.avtSide)
        {
            case AvatarSide.Left:
                avatar.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                break;
            case AvatarSide.Right:
                avatar.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
        }
    }
    private IEnumerator Typing(string s)
    {
        _isTyping = true;
        yield return new WaitForSeconds(0.75f);

        dialogText.text = "";
        string displayedText = "";
        int alphaIndex = 0;

        _skipTypingCoroutine = StartCoroutine(SkipTyping(s));

        foreach (char c in s)
        {
            alphaIndex++;
            displayedText = s.Insert(alphaIndex, "<color=#00000000>");
            dialogText.text = displayedText;
            
            yield return new WaitForSeconds(0.02f);
        }

        StopCoroutine(_skipTypingCoroutine);

        _isTyping = false;
    }
    private IEnumerator SkipTyping(string s)
    {
        yield return new WaitUntil(() => InputManager.instance.isGetAnyKeyDown);
        yield return new WaitUntil(() => InputManager.instance.isGetAnyKeyDown);
        StopCoroutine(_typingCoroutine);
        dialogText.text = s;
        _isTyping = false;
    }
    private void NextLine()
    {
        if (_paragraphs.Count > 0)
        {
            dialogText.text = "";
            _typingCoroutine = StartCoroutine(Typing(_paragraphs.Dequeue()));
        }
        else
        {
            dialogText.text = "";
            dialogPanel.transform.DOScaleY(0f, 0.25f).OnComplete(() =>
            {
                dialogPanel.SetActive(false);
            });
            isEndedConversation = true;
            //InputManager.instance.canGetAction = true;
        }
    }

    public void SetUpConversation(List<ConversationData> converstations)
    {
        StartCoroutine(IESetUpConversation(converstations));
    }
    private IEnumerator IESetUpConversation(List<ConversationData> converstations)
    {
        foreach(ConversationData conversation in converstations)
        {
            _conversationQueue.Enqueue(conversation);
        }
        InputManager.instance.canGetAction = false;
        while (_conversationQueue.Count > 0)
        {
            OpenDialogue(_conversationQueue.Dequeue());
            yield return new WaitUntil(() => isEndedConversation);
            yield return new WaitForSeconds(0.5f);
        }
        InputManager.instance.canGetAction = true;
        yield return null;

    }
}
