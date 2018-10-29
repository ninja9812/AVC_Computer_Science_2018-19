using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /* Config parameters */
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpStrength = 5;

    /* State */
    bool isGrounded = true;
    bool isMoving = false;

    /* Cached components */
    Animator myAnimator;
    SpriteRenderer mySprite;
    Rigidbody2D myBody;
    Collider2D myCollider2D;

    /* Use this for initialization */
    void Start ()
    {
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
    }

    /* Update is called once per frame */
    void Update ()
    {
        // check if player is on ground
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // check if player is moving
        Move();

        // check if player pressed jump button
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // animate the player
        Animate();
    }

    /* Implement player movement using Input class */
    private void Move()
    {
        // get user input (horizontal axis set in unity editor, default: arrow keys)
        var deltaX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        // check direction of movement
        if (deltaX > 0)
        {
            mySprite.flipX = false;
            isMoving = true;
        }
        else if (deltaX < 0)
        {
            mySprite.flipX = true;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        myBody.velocity = new Vector2(deltaX, myBody.velocity.y);
        // transform.position = new Vector2(transform.position.x + deltaX, transform.position.y);
    }

    /* Implement player jump using velocity */
    private void Jump()
    {
        // player can only jump if currently on the ground
        if (isGrounded)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpStrength);
        }
    }

    /* Implement player animations using the animator config */
    private void Animate()
    {
        // animations on the ground
        if (isGrounded)
        {
            if (isMoving)
            {
            myAnimator.Play("Hero-Run");
            }
            else
            {
            myAnimator.Play("Hero-Idle");
            }
        }
        // animation in the air
        else
        {
            myAnimator.Play("Hero-Jump");
        }
    }
}
