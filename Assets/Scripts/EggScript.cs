using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EggScript : MonoBehaviour
{
    public float collisionSpeedThreshold = 3.0f;

    public float activeSpeedThreshold = 1.0f;
    public int bounceCount = 0;
    Rigidbody2D rb;
    void Awake()
    {
        //Start animation
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (rb.velocity.magnitude < activeSpeedThreshold)
        {
            bounceCount = 0;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void EndSpawnAnimation()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    public void AddBounceCount()
    {
        
        bounceCount += 1;

        if (bounceCount >= 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.relativeVelocity.magnitude > collisionSpeedThreshold)
        //{
            
            GameObject otherObject = collision.collider.gameObject;
            if (otherObject.CompareTag("Wall") || otherObject.CompareTag("Egg"))
            {
                //Break egg
                //Destroy(gameObject);
                AddBounceCount();
            }
        //}
    }
    
}
