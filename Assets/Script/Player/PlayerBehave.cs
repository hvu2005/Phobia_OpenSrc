using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.XR;
using UnityEngine;

public class PlayerBehave : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    [SerializeField] private float moveSpeed;
    public bool canMove { get; set; } = true;
    private bool _isFacingRight = true;
    //~~~~~~~~~~~~~~~~~~~~GroundCheck~~~~~~~~~~~~~~~~~~~~
    [Header("GroundCheck")]
    [SerializeField] private Vector2 groundCheckOffset;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float checkGroundRange;
    public bool isGrounded { get; private set; }
    //~~~~~~~~~~~~~~~~~~~~~Slashing~~~~~~~~~~~~~~~~~~~~~~
    [Header("Slashing")]
    [SerializeField] private GameObject horizontalSlasher;
    [SerializeField] private GameObject verticalSlasher;
    private bool _isSlashing;
    public int attackIndex { get; private set; }
    //~~~~~~~~~~~~~~~~~~~~~~Jumping~~~~~~~~~~~~~~~~~~~~~~
    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float _jumpTimeCounter;
    private bool _canJump;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove || !InputManager.instance.canGetAction)
        {
            return;
        }
        Moving();
        Slashing();
        Jumping();
        GroundCheck();
        Flip();
    }
    private void Flip()
    {
        if(!InputManager.instance.isSlashing)
        {
            if (InputManager.instance.move > 0f && !_isFacingRight || InputManager.instance.move < 0f && _isFacingRight)
            {
                _isFacingRight = !_isFacingRight;
                Vector3 localScaleX = transform.localScale;
                localScaleX.x *= -1;
                transform.localScale = localScaleX;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void Moving()
    {
        rb.velocity = new Vector2(InputManager.instance.move * moveSpeed, rb.velocity.y);
    }
    private void Jumping()
    {
        if(isGrounded)
        {
            _jumpTimeCounter = jumpTime;
            _canJump = true;
        }
        if(InputManager.instance.isJumping && _jumpTimeCounter > 0f && _canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _jumpTimeCounter -= Time.deltaTime;
        }
        if(!InputManager.instance.isJumping && !isGrounded)
        {
            _canJump = false;
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
        if (InputManager.instance.slash)
        {
            if (InputManager.instance.look != 0f && !isGrounded)
            {
                attackIndex = InputManager.instance.look < 0f ? 3 : 2;
            }
            else
            {
                horizontalSlasher.SetActive(true);
                attackIndex++;
                attackIndex %= 2;
            }
        }
    }
}
