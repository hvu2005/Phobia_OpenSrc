using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehave : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Jumping();
    }
    private void Moving()
    {
        rb.velocity = new Vector2(InputManager.instance.move * moveSpeed, rb.velocity.y);
    }
    private void Jumping()
    {
        if(InputManager.instance.isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
