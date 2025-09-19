using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMelee : EnemyBase
{
    void Start()
    {
        base.Start();
    }

    void AttackPlayer()
    {   
        isAttacking = true;
        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 dir = ((Vector2)(player.transform.position - transform.position)).normalized;
        rb.AddForce(dir * 100f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        var hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(range, range), angle);
        foreach (Collider2D hit in hits) {
            if (hit.CompareTag("Player")) {
                DoDamage(damage);
            }
        }
        isAttacking = false;
    }
}