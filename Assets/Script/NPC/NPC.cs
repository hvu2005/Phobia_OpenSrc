using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private List<ConversationData> conversationData;
    private bool _playerIsCloseBy;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WaitForInteract());
    }

    private IEnumerator WaitForInteract()
    {
        yield return new WaitUntil(() => _playerIsCloseBy && InputManager.instance.look > 0f);
        Flip();
        DialogueManager.instance.SetUpConversation(conversationData);
        StartCoroutine(returnSide());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _playerIsCloseBy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerIsCloseBy = false;
        }
    }
    private void Flip()
    {
        float playerX = _player.transform.position.x;
        float flipRotationY = (transform.position.x -  playerX < 0f) ? 180f : 0f;

        _player.GetComponent<PlayerBehave>().ForceToFlip(flipRotationY == 0f);
        transform.rotation = Quaternion.Euler(0f, flipRotationY, 0f);
    }
    private IEnumerator returnSide()
    {
        yield return new WaitUntil(() => InputManager.instance.canGetAction);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
