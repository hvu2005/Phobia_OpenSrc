using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerBehave player;
    private Animator animator;
    [SerializeField] private GameObject jumpSmoke;
    private bool wasFalling;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerBehave>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking", player.rb.velocity.x != 0f);
        animator.SetBool("isJumping", player.rb.velocity.y > 0f);
        animator.SetBool("isFalling", player.rb.velocity.y < 0f);
        if(animator.GetBool("isFalling"))
        {
            wasFalling = true;
        }
        if(player.isGrounded && wasFalling)
        {
            StartCoroutine(JumpSmoke());
            wasFalling = false;
        }
        animator.SetBool("isSlashing", InputManager.instance.isSlashing);

    }
    private IEnumerator JumpSmoke()
    {
        jumpSmoke.SetActive(true);
        jumpSmoke.transform.position = transform.position + new Vector3(0f,1.92f,0f);
        yield return new WaitForSeconds(0.5f);
        jumpSmoke.SetActive(false);
    }
}
