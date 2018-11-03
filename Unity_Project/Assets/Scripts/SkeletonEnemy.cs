using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : EnemyController
{

    /* State variables */
    bool isWaiting = true;

    /* Cached components */
    Rigidbody2D myBody;

    /* Use this for initialization */
	void Start ()
    {
        myBody = GetComponent<Rigidbody2D>();
	}

	/* Update is called once per frame */
	void Update ()
    {
        // skeleton is active
        if (!isWaiting)
        {
            // deactivate skeleton
            if (Mathf.Abs(GetDistanceFromPlayer()) > aggroDistance)
            {
                myAnimator.Play("Drop");
                isWaiting = true;
                myBody.velocity = new Vector2(0, myBody.velocity.y);
            }
            // activate after finishing rise animation
            else if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Rise"))
            {
                MoveTowardsPlayer();
                myAnimator.Play("Walk");
            }
        }
        // skeleton is inactive and waiting
        else
        {
            if (Mathf.Abs(GetDistanceFromPlayer()) <= aggroDistance)
            {
                myAnimator.Play("Rise");
                isWaiting = false;
            }
        }
	}

    private void MoveTowardsPlayer()
    {
        var deltaX = moveSpeed;

        // direction of motion (sprite points left by default)
        if (GetDistanceFromPlayer() < 0)
        {
            mySprite.flipX = false;
            deltaX *= -1;
        }
        else
        {
            mySprite.flipX = true;
        }

        myBody.velocity = new Vector2(deltaX, myBody.velocity.y);
    }
}
