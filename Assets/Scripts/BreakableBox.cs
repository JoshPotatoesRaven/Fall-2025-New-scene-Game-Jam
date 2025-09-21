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

    public int damagedHealthValue = 1;
    public int damagedIndex = 1;
    public int breakTextIndex;
    GameObject breakText;

    public bool isHealingWall = true;

    GameObject gameManager;
    HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        if (animationStateName != "")
        {
            animator = box.GetComponent<Animator>();
            animator.Play(animationStateName, 0, 0f);
            animator.speed = 0f;
        }

        breakText = transform.GetChild(breakTextIndex).gameObject;

        gameManager = GameObject.FindGameObjectWithTag("GameController");
        healthManager = gameManager.GetComponent<HealthManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > collisionSpeedThreshold)
        {
            GameObject otherObject = collision.collider.gameObject;
            if (otherObject.CompareTag("Egg"))
            {
                EggScript eggScript = otherObject.GetComponent<EggScript>();
                if (eggScript.bounceCount >= 1)
                {
                    Destroy(otherObject);

                    takeDamage();
                }

            }
        }
    }


    void takeDamage()
    {
        health -= 1;
        if (health == damagedHealthValue)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(damagedIndex).gameObject.SetActive(true);
        }

        if (health <= 0)
        {
            Break();
        }
    }

    void Break()
    {

        //Break self and show text.
        BoxCollider2D selfCollider = gameObject.GetComponent<BoxCollider2D>();
        selfCollider.enabled = false;
        
        if (animationStateName != "")
        {
            animator.speed = 1f;
        }


        breakText.SetActive(true);

        //Heal to full when breaking a box
        if (isHealingWall)
        {
            healthManager.currentHealth = healthManager.maxHealth;
        }
        
        
    }
}
