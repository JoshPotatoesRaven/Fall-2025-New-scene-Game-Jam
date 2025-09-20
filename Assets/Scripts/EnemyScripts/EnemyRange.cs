using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : EnemyBase
{
    public GameObject bullet;

    // Add a lifetime field if needed
    public float lifetime = 2f;
    public float bulletSpeed = 20f;

    public bool doesRichochet = false;
    new protected void Start()
    {
        base.Start();
        OnDeath += () => StopAllCoroutines();

        detectRange = 40;
    }

    protected override void AttackPlayer()
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
            bulletScript.range = 100;
            bulletScript.speed = bulletSpeed;
            bulletScript.damage = damage;
            bulletScript.lifetime = 10;
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
        else if (doesRichochet)
        {
            // Use collision normal from BulletScript if available, otherwise default to Vector2.right
            Vector2 normal = Vector2.right;
            BulletScript bulletScript = other.GetComponent<BulletScript>();

            Vector2 reflectedDir = Vector2.Reflect(other.transform.right, normal);
            GameObject newBulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            var newBulletScript = newBulletInstance.GetComponent<BulletScript>();
            if (newBulletScript != null)
            {
                newBulletScript.range = 100;
                newBulletScript.speed = bulletSpeed;
                newBulletScript.damage = damage;
                newBulletScript.lifetime = 10;
                newBulletScript.direction = reflectedDir;
                newBulletScript.OnTriggerEnter += OnBulletTriggerEnter;
            }
        }
    }
}