using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    /* Config parameters */
    [SerializeField] protected int health = 100;
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected float aggroDistance = 10;
    [SerializeField] protected GameObject player;

    /* Cached components */
    protected SpriteRenderer mySprite;
    protected Collider2D myCollider2D;
    protected Animator myAnimator;

    /* Use this for initialization */
	void Awake ()
    {
        myAnimator = GetComponent<Animator>();
        mySprite = GetComponent<SpriteRenderer>();
	}

    /* Get distance between enemy and camera */
    protected float GetDistanceFromPlayer()
    {
        // player is active
        if (player.activeSelf)
        {
            var playerPosition = player.transform.TransformPoint(Vector3.zero);
            var myPosition = transform.TransformPoint(Vector3.zero);
            return playerPosition.x - myPosition.x;
        }
        // player is inactive
        else
        {
            return aggroDistance + 1;
        }
    }
}
