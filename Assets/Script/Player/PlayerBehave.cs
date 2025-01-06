using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using UnityEditor.XR;
using UnityEngine;

public class PlayerBehave : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    [SerializeField] private float moveSpeed;
    public bool canMove { get; set; } = true;
    private bool _isFacingRight = true;
    [SerializeField] private int hp;
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
    //~~~~~~~~~~~~~~~~~~~~~~TakeDmg~~~~~~~~~~~~~~~~~~~~~~
    [Header("Take Damage")]
    [SerializeField] private float immortalDuration;
    private bool _isImmortal;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance.UpdatePlayerHp(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(!InputManager.instance.canGetAction)
        {
            return;
        }
        Moving();
        Slashing();
        Jumping();
        GroundCheck();
        Flip();
    }
    public void ForceToFlip(bool isFacingRight)
    {
        _isFacingRight = isFacingRight;
        Vector3 localScale = isFacingRight ? Vector3.one : new Vector3(-1f, 1f, 1f);
        transform.localScale = localScale;
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
        if(collision.CompareTag("Enemy") && !_isImmortal)
        {
            TakeDmg();
            if(hp <= 0)
            {
                Debug.Log("chet");
            }
        }
    }
    private void Moving()
    {
        rb.velocity = new Vector2(InputManager.instance.move * moveSpeed, rb.velocity.y);
    }
    private void Jumping()
    {
        if(InputManager.instance.canGetAction && isGrounded)
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
    private void TakeDmg()
    {

        GameManager.instance.TakeDmgScreen();
        StartCoroutine(ImmortalCoroutine());
        StartCoroutine(KnockBackCoroutine());

        GameManager.instance.UpdatePlayerHp(--hp);
        
    }
    private IEnumerator KnockBackCoroutine()
    {
        _canJump = false;
        InputManager.instance.canGetAction = false;
        InputManager.instance.ResetAction();
        float orginGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero; 

        yield return new WaitForSeconds(0.15f);
        rb.gravityScale = orginGravity;
        float xPos = _isFacingRight ? -5f : 5f;
        rb.velocity = new Vector2(xPos, 5f); 

        yield return new WaitForSeconds(0.5f);
        InputManager.instance.canGetAction = true;
    }
    private IEnumerator ImmortalCoroutine()
    {
        _isImmortal = true;
        float elapsedTime = 0f;
        Color playerColor = GetComponent<SpriteRenderer>().color;

        while (elapsedTime < immortalDuration)
        {
            float a = Mathf.PingPong(Time.time / 0.2f, 1f);
            playerColor.a = Mathf.Lerp(1f, 0f, a);
            GetComponent<SpriteRenderer>().color = playerColor; 

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isImmortal = false;
        playerColor.a = 1f;
        GetComponent<SpriteRenderer>().color = playerColor;
    }
    private void Slashing()
    {
        if (InputManager.instance.slash)
        {
            if (InputManager.instance.look != 0f)
            {
                attackIndex = InputManager.instance.look < 0f ? 3 : 2;
                verticalSlasher.transform.localScale = InputManager.instance.look > 0f ? Vector3.one : new Vector3(1f, -1f, 1f);
                StartCoroutine(SlashBox(verticalSlasher, new Vector3(0f,2.75f,0f)));
            }
            else
            {
                StartCoroutine(SlashBox(horizontalSlasher));
                attackIndex++;
                attackIndex %= 2;
            }
        }
    }

    private IEnumerator SlashBox(GameObject slasher, Vector3? offset = null)
    {
        Vector3 finalOffset = offset ?? Vector3.zero;

        Vector3 localScale = slasher.transform.localScale;
        localScale.x = _isFacingRight ? 1 : -1;
        slasher.transform.localScale = localScale;

        slasher.SetActive(true);        
        while(InputManager.instance.isSlashing)
        {
            slasher.transform.position = transform.position + finalOffset;
            yield return null;
        }
        slasher.SetActive(false);
    }
}
