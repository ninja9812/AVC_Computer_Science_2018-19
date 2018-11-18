using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /* Config parameters */
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpStrength = 5;
    [SerializeField] int health = 500;
    [SerializeField] int invincibilityTime = 1;
    [SerializeField] float iFrameSpeed = 3;
    [SerializeField] Color damageColor = new Color(0.4f, 0.4f, 0.4f, 1f);

    /* State variables */
    bool isGrounded = true;
    bool isMoving = false;
    float invincibilityCounter = -1;
    Color defaultColor = new Color(1f, 1f, 1f, 1f);

    /* Cached components */
    Animator myAnimator;
    SpriteRenderer mySprite;
    Rigidbody2D myBody;
    Collider2D myCollider2D;

    // debugging
    public Color myColor;

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
        // handle collisions
        HandleCollisions();

        // check if player is moving
        Move();

        // check if player pressed jump button
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // animate the player
        Animate();

        // update invincibilityCounter
        if (invincibilityCounter >= 0)
        {
            invincibilityCounter -= 1 * Time.deltaTime;
        }

        // check if player has died
        if (health <= 0)
        {
            Die();
        }
        myColor = mySprite.color;
    }

    /* Implement collision handling */
    private void HandleCollisions()
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

        // check if player is hit by any damage and not invincible
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Player Damage")) && invincibilityCounter < 0)
        {
            health -= 50;
            invincibilityCounter = invincibilityTime;
        }

        // check if player fell into kill zone
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Kill Zone")))
        {
            health = 0;
        }
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

        // move using velocity (smoother than transform)
        myBody.velocity = new Vector2(deltaX, myBody.velocity.y);
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
                myAnimator.Play("Run");
            }
            else
            {
                myAnimator.Play("Idle");
            }
        }
        // animation in the air
        else
        {
            myAnimator.Play("Jump");
        }
    }

    /* Implement death */
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
