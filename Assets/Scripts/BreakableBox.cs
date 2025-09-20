using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public float collisionSpeedThreshold = 3.0f;
    public int health = 1;
    public GameObject box;
    Animator animator;
    public String animationStateName;

    public int breakTextIndex;
    GameObject breakText;
    public GameObject textBackground;
    // Start is called before the first frame update
    void Start()
    {
        animator = box.GetComponent<Animator>();
        animator.Play(animationStateName, 0, 0f);
        animator.speed = 0f;
        breakText = transform.GetChild(breakTextIndex).gameObject;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > collisionSpeedThreshold)
        {
            GameObject otherObject = collision.collider.gameObject;
            if (otherObject.CompareTag("Egg"))
            {
                Destroy(otherObject);

                health -= 1;
                if (health <= 0) {
                    Break();
                }
            }
        }
    }

    void Break()
    {
        
        //Break self and show text.
        BoxCollider2D selfCollider = gameObject.GetComponent<BoxCollider2D>();
        selfCollider.enabled = false;
        animator.speed = 1f;
        breakText.SetActive(true);
        
    }
}
