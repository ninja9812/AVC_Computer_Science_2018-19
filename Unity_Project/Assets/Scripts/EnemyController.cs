using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    /* Config parameters */
    [SerializeField] protected int health = 100;
    [SerializeField][RangeAttribute(0.01f, 0.1f)] protected float moveSpeed;
    [SerializeField] protected float aggroDistance = 10;
    [SerializeField] protected GameObject player;

    /* Cached components */
    protected SpriteRenderer mySprite;
    protected Collider2D myCollider2D;

    /* Get distance between enemy and camera */
    protected float GetDistanceFromPlayer()
    {
        var playerPosition = player.transform.TransformPoint(Vector3.zero);
        var myPosition = transform.TransformPoint(Vector3.zero);
        return playerPosition.x - myPosition.x;
    }
}
