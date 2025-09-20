using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

//Attach to player
public class TailSwipe : MonoBehaviour
{
    CircleCollider2D hitbox;

    GameObject graphics;

    public float hitboxActiveTime = 0.2f;

    public float cooldownTotalTime = 0.5f;

    public float timer = 0;

    public float knockbackForce = 5;

    public bool ready;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<CircleCollider2D>();
        graphics = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && timer <= 0)
        {
            hitbox.enabled = true;
            graphics.SetActive(true);
            timer = cooldownTotalTime;
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < cooldownTotalTime - hitboxActiveTime)
        {
            hitbox.enabled = false;
            graphics.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            //Apply force and end attack
            hitbox.enabled = false;

            Rigidbody2D eggRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDir = (collision.transform.position - transform.position).normalized;
            eggRb.AddForce(forceDir * knockbackForce);
        }
    }
}
