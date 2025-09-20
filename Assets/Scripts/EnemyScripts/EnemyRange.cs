using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyBase
{
    public GameObject bullet;

    // Add a lifetime field if needed
    public float lifetime = 2f;

    new protected void Start()
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

        // Get BulletScript component and set its properties
        var bulletScript = bulletInstance.GetComponent<BulletScript>();
        if (bulletScript != null)
        {
            bulletScript.range = range;
            bulletScript.speed = speed;
            bulletScript.damage = damage;
            bulletScript.lifetime = lifetime;
            bulletScript.direction = dir;
            bulletScript.OnTriggerEnter += OnBulletTriggerEnter;
        }

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
            // Use collision normal from BulletScript if available, otherwise default to Vector2.right
            Vector2 normal = Vector2.right;
            BulletScript bulletScript = other.GetComponent<BulletScript>();

            Vector2 reflectedDir = Vector2.Reflect(other.transform.right, normal);
            GameObject newBulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            var newBulletScript = newBulletInstance.GetComponent<BulletScript>();
            if (newBulletScript != null)
            {
                newBulletScript.range = range;
                newBulletScript.speed = speed;
                newBulletScript.damage = damage;
                newBulletScript.lifetime = lifetime;
                newBulletScript.direction = reflectedDir;
                newBulletScript.OnTriggerEnter += OnBulletTriggerEnter;
            }
        }
    }
}