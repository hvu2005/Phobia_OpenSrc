using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private List<ConversationData> conversationData;
    [SerializeField] private GameObject pointer;
    private TextMeshPro interactText;
    private bool _playerIsCloseBy;
    private bool _endConversation;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        interactText = pointer.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(WaitForInteract());
    }

    private IEnumerator WaitForInteract()
    {
        yield return new WaitUntil(() => _playerIsCloseBy && InputManager.instance.look > 0f);
        Flip();
        _player.GetComponent<PlayerBehave>().rb.velocity = Vector2.zero;
        DialogueManager.instance.SetUpConversation(conversationData);
        pointer.transform.DOScale(0f, 0.15f);
        _endConversation = true;
        StartCoroutine(returnSide());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !_endConversation)
        {
            pointer.transform.DOScale(1f, 0.15f);
            _playerIsCloseBy = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !_endConversation)
        {
            interactText.text = InputManager.instance.Data.actions["look"].bindings[2].ToDisplayString();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pointer.transform.DOScale(0f, 0.15f);

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
