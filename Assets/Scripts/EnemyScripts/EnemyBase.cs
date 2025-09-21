using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    public GameObject SpawnEgg;

    GameObject gameManager;

    HealthManager healthManager;

    [Header("Stats")]
    public int detectRange;

    public float chaseRange = 15;
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
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        healthManager = gameManager.GetComponent<HealthManager>();
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
        else if (Vector2.Distance(player.transform.position, transform.position) < chaseRange)
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
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, detectRange, layerMask);

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;
            if (hit.collider.gameObject == gameObject) continue; // Ignore self
            if (hit.collider.CompareTag("Player"))
                return true;
        }
        return false;
    }

    private void ChasePlayer()
    {
        if (player == null) return;
        rb.velocity = (player.transform.position - transform.position).normalized * speed;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        transform.up = direction;

    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure attack hitbox has this tag
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
        }
    }
    */

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
        healthManager.currentHealth -= damage;
        Debug.Log(String.Format("Dealt {0} damage", damage));
    }

    protected void FlashRed()
    {
        StartCoroutine(FlashRedCoroutine());
    }
    private IEnumerator FlashRedCoroutine()
    {
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }
    public virtual void Die()
    {
        Instantiate(SpawnEgg, transform.position, Quaternion.identity);
        Destroy(gameObject);
        OnDeath?.Invoke();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject otherObject = collision.collider.gameObject;
        if (otherObject.CompareTag("Egg"))
        {
            EggScript eggScript = otherObject.GetComponent<EggScript>();
            if (eggScript.bounceCount >= 1)
            {
                TakeDamage(1);
            }
        }
        
    }
}