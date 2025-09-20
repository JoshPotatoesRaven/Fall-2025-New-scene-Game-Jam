using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float damage;
    public float range;
    public float lifetime;
    public Vector2 direction;

    public event Action<Collider2D> OnTriggerEnter;

    void Start()
    {

        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        transform.right = direction;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        range -= speed * Time.deltaTime;
        if (range <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnter?.Invoke(other);
        Destroy(gameObject);
    }
}