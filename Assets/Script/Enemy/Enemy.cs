using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;

    [SerializeField] private float hp;

    //~~~~~~~~~~~~~~~~~DetectPlayer~~~~~~~~~~~~~~~~~~~
    [Header("DetectPlayerSystem")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float detectRadius;
    [SerializeField] private float detectDistance;
    [SerializeField] private Vector3 detectOffset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Slash"))
        {
            hp--;
        }
    }
    
    private void DetectPlayer()
    {

    }
}
