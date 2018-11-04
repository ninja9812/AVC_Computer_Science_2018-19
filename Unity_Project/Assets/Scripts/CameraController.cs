using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    /* Config parameters */
    [SerializeField] float lookAhead;
    [SerializeField] float smoothing;
    [SerializeField] GameObject target;

    /* Cached components */
    SpriteRenderer targetSprite;

    /* Use this for initialization */
    void Start()
    {
        targetSprite = target.GetComponent<SpriteRenderer>();
    }

    /* Update is called once per frame */
    void Update ()
    {
        var targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        if (!targetSprite.flipX)
        {
            targetPosition = new Vector3(targetPosition.x + lookAhead, targetPosition.y, targetPosition.z);
        }
        else
        {
            targetPosition = new Vector3(targetPosition.x - lookAhead, targetPosition.y, targetPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}
