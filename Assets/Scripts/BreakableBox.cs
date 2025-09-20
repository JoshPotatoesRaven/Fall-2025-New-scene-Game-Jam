using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public float collisionSpeedThreshold = 3.0f;

    public GameObject box;
    Animator animator;
    public String animationStateName;

    public GameObject breakText;
    // Start is called before the first frame update
    void Start()
    {
        animator = box.GetComponent<Animator>();
        animator.Play(animationStateName, 0, 0f);
        animator.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > collisionSpeedThreshold)
        {
            GameObject otherObject = collision.collider.gameObject;
            if (otherObject.CompareTag("Egg"))
            {
                //Break self and show text.
                BoxCollider2D selfCollider = gameObject.GetComponent<BoxCollider2D>();
                selfCollider.enabled = false;
                animator.speed = 1f;

                breakText.SetActive(true);
                        
                Destroy(otherObject);
            }
        }
    }
}
