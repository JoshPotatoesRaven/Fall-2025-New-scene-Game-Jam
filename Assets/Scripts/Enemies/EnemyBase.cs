using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;

    [Header("Stats")]
    public int range;
    public int health;
    public int damage;
    public float speed;

    [Header("Attack")]
    public float attackCooldown = 1.0f;
    
    private float _nextAttackTime = 0f;
    private bool isStunned = false;
    private bool isAttacking = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isStunned) {
            return;
        }

        if (CanHitPlayer()) {
            if (Time.time >= _nextAttackTime) {
                AttackPlayer();
                _nextAttackTime = Time.time + attackCooldown;
            }
        } else {
            ChasePlayer();
        }
    }

    private bool CanHitPlayer()
    {
        if (player == null || isAttacking) return false;
        Vector2 dir = ((Vector2)(player.transform.position - transform.position)).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, range);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void ChasePlayer()
    {
        if (player == null) return;
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }

    private void AttackPlayer()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    public void DoDamage(int damage)
    {
        player.GetComponent<Player>().TakeDamage(damage);
    }

    public void Die() 
    {
        Destroy(gameObject);
    }
}