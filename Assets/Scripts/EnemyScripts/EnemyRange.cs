using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyBase
{
    public GameObject bullet;

    new void Start()
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
        yield return new WaitForSeconds(0.2f);
        Vector2 dir = ((Vector2)(player.transform.position - transform.position)).normalized;
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletInstance.range = range;
        bulletInstance.speed = speed;
        bulletInstance.damage = damage;
        bulletInstance.lifetime = lifetime;
        bulletInstance.direction = dir;
        bulletInstance.OnTriggerEnter += OnBulletTriggerEnter;
        isAttacking = false;
    }

    private void OnBulletTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoDamage(damage);
        }
        else if (other.CompareTag("Wall"))
        {
            Vector2 normal = other.GetContact(0).normal;
            Vector2 reflectedDir = Vector2.Reflect(bulletInstance.transform.right, normal);
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletInstance.range = range;
            bulletInstance.speed = speed;
            bulletInstance.damage = damage;
            bulletInstance.lifetime = lifetime;
            bulletInstance.direction = reflectedDir;
        }
    }
}