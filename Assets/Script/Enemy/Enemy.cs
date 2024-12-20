using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Patrolling());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Slash"))
        {
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
