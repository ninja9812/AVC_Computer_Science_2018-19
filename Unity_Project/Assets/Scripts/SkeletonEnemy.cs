using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : EnemyController
{

    /* State */
    bool isWaiting = true;

    /* Cached components */
    Animator myAnimator;

	/* Use this for initialization */
	void Start ()
    {
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
	}

	/* Update is called once per frame */
	void Update ()
    {
        // skeleton is active
        if (!isWaiting)
        {
            MoveTowardsPlayer();
            myAnimator.Play("Walk");
            if (Mathf.Abs(GetDistanceFromPlayer()) > aggroDistance)
            {
                myAnimator.Play("Drop");
                isWaiting = true;
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

        transform.position = new Vector2(transform.position.x + deltaX, transform.position.y);
    }
}
