using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    public GameObject SpawnEgg;

    [Header("Stats")]
    public int detectRange;
    public int attackRange;
    public int health;
    public int damage;
    public int points;
    public float speed;

    [Header("Attack")]
    public float attackCooldown = 1.0f;

    protected float _nextAttackTime = 0f;
    protected bool isStunned = false;
    protected bool isAttacking = false;

    public event Action OnDeath;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isStunned)
        {
            return;
        }
        if (CanHitPlayer())
        {
            if (Time.time >= _nextAttackTime)
            {
                AttackPlayer();
                _nextAttackTime = Time.time + attackCooldown;
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    void LateUpdate()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.right = direction;
    }

    private bool CanHitPlayer()
    {
        if (player == null || isAttacking) return false;
        Vector2 dir = ((Vector2)(player.transform.position - transform.position)).normalized;
        int layerMask = LayerMask.GetMask("Default", "Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, detectRange, layerMask);

        Debug.Log(hit.collider);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void ChasePlayer()
    {
        if (player == null) return;
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure attack hitbox has this tag
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
        }
    }

    protected virtual void AttackPlayer()
    {

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashRed();
        if (health <= 0)
        {
            Die();
        }
    }

    public void DoDamage(int damage)
    {
        //player.GetComponent<Player>().TakeDamage(damage);
    }

    protected void FlashRed()
    {
        StartCoroutine(FlashRedCoroutine());
    }
    private IEnumerator FlashRedCoroutine()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }
    public void Die()
    {
        Instantiate(SpawnEgg);
        Destroy(gameObject);
        OnDeath?.Invoke();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject otherObject = collision.collider.gameObject;
        if (otherObject.CompareTag("Egg"))
        {
            TakeDamage(1);
        }
        
    }
}