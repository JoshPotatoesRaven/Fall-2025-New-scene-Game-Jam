using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EggScript : MonoBehaviour
{
    public float collisionSpeedThreshold = 3.0f;

    Rigidbody2D rb;
    void Awake()
    {
        //Start animation
        rb = GetComponent<Rigidbody2D>();
    }

    public void EndSpawnAnimation()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > collisionSpeedThreshold)
        {
            GameObject otherObject = collision.collider.gameObject;
            if (!otherObject.CompareTag("Player"))
            {
                //Break egg
                Destroy(gameObject);
            }
        }
    }
    */
}
