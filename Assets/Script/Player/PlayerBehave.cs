using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBehave : MonoBehaviour
{
    public static PlayerBehave instance;

    public Rigidbody2D rb { get; private set; }
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool isFacingRight = true;
    //~~~~~~~~~~~~~~~~~~~~GroundCheck~~~~~~~~~~~~~~~~~~~~
    [SerializeField] private Vector2 groundCheckOffset;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float checkGroundRange;
    private bool isGrounded;
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private bool isSlashing;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Jumping();
        GroundCheck();
        Flip();
    }
    private void Flip()
    {
        if (InputManager.instance.move > 0f && !isFacingRight || InputManager.instance.move < 0f && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScaleX = transform.localScale;
            localScaleX.x *= -1;
            transform.localScale = localScaleX; 
        }
    }
    private void Moving()
    {
        rb.velocity = new Vector2(InputManager.instance.move * moveSpeed, rb.velocity.y);
    }
    private void Jumping()
    {
        if(InputManager.instance.isJumping && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void GroundCheck()
    {
        Vector2 pos = (Vector2)transform.position + groundCheckOffset;
        RaycastHit2D hit = Physics2D.Raycast(pos,Vector2.right,checkGroundRange,whatIsGround);
        isGrounded = hit.collider != null;
        Debug.DrawRay(pos,Vector2.right*checkGroundRange, Color.red);
    }
    private void Slashing()
    {
        if(InputManager.instance.isSlashing && !isSlashing)
        {

        }
    }
}
