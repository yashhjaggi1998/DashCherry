using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float dirX = 0f;

    [SerializeField] private LayerMask jumpableGround;

    [SerializeField]private float moveSpeed = 6f;
    [SerializeField]private float jumpSpeed = 14f;

    private enum MovementState { idle, running, jumping, falling}

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //horizontal movement
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        //vertical movement
        if(Input.GetButtonDown("Jump") && isGrounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
            state = MovementState.idle;

        if(rb.velocity.y > .1f)
            state = MovementState.jumping;
        else if(rb.velocity.y < -.1f)
            state = MovementState.falling;

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        //BoxCast method returns true if player box overlaps with ground layer.
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
