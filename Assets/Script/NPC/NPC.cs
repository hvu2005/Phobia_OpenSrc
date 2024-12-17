using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private List<ConversationData> conversationData;
    private bool _playerIsCloseBy;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(WaitForInteract());
    }

    private IEnumerator WaitForInteract()
    {
        yield return new WaitUntil(() => _playerIsCloseBy && InputManager.instance.look > 0f);
        Flip();
        DialogueManager.instance.SetUpConversation(conversationData);
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
        //Vector3 localScaleX = transform.localScale;
        //localScaleX.x *= -1;
        //transform.localScale = localScaleX;
    }
}
