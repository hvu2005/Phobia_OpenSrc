using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] private int hp;

    //~~~~~~~~~~~~~~~~~DetectPlayer~~~~~~~~~~~~~~~~~~~
    [Header("Detect Player System")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float detectRadius;
    [SerializeField] private float detectDistance;
    [SerializeField] private Vector3 detectOffset;
    //~~~~~~~~~~~~~~~~~Patrolling~~~~~~~~~~~~~~~~~~~~~~
    [Header("Patrolling")]


    protected PlayerBehave _player;
    public void Initialize()
    {
        gameObject.tag = "Enemy";
        _player = GameManager.instance.player.GetComponent<PlayerBehave>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Patrolling());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        
        
    }
#endif

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Slash"))
        {
            if(InputManager.instance.look < 0f)
            {
                _player.rb.velocity = new Vector2(_player.rb.velocity.x, 10f);
            }


            hp--;
            if(hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Patrolling()
    {
        yield return null;
    }

    public bool DetectedPlayer()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position + detectOffset, detectRadius, Vector2.down, detectDistance, whatIsPlayer);
        return hit.collider != null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + detectOffset, detectRadius);

    }
}
