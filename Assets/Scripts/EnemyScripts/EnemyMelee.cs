using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMelee : EnemyBase
{
    new void Start()
    {
        base.Start();
        OnDeath += () => StopAllCoroutines();
    }

    protected override void AttackPlayer()
    {
        rb.velocity = Vector2.zero;
        isStunned = true;
        isAttacking = true;
        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 dir = ((Vector2)(player.transform.position - transform.position)).normalized;
        rb.AddForce(dir * 20f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        var hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(attackRange, attackRange), angle);
        Debug.DrawLine(transform.position, transform.position + (Vector3)(dir * attackRange), Color.red, 1f);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                DoDamage(damage);
            }
        }
        isStunned = false;
        isAttacking = false;
    }
}
