using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour {

    public float bounceForce;

    private Rigidbody2D playerRigidBody;


    // Use this for initialization
    void Start () {

        playerRigidBody = transform.parent.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
    }

         void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);
                playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, bounceForce, 0f);

            }
        }


    
}
