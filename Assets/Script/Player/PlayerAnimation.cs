using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private bool hasJumped;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalking",InputManager.instance.move != 0f);
        animator.SetBool("isJumping", PlayerBehave.instance.rb.velocity.y != 0f);
    }
}
