using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private ConversationData conversation;
    private bool _playerIsCloseBy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForInteract());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator WaitForInteract()
    {
        yield return new WaitUntil(() => _playerIsCloseBy && InputManager.instance.look > 0f);
        DialogueManager.instance.SetUpConversation(conversation.paragraphs);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _playerIsCloseBy = true;
        }
    }
    private void Flip()
    {

    }
}
