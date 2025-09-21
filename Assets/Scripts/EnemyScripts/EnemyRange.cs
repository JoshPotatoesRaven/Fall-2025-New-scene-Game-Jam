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
        OnDeath += () =>
        {
            Instantiate(SpawnEgg, transform.position + new Vector3(Random.Range(-3,3), Random.Range(-3, 3), Random.Range(-3, 3)), Quaternion.identity);
        };
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
      
        bulletScript.range = 100;
        bulletScript.speed = bulletSpeed;
        bulletScript.damage = damage;
        bulletScript.lifetime = 10;
        bulletScript.direction = dir;
        bulletScript.OnTriggerEnter += OnBulletTriggerEnter;
        

        isAttacking = false;
    }

    private void OnBulletTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DoDamage(damage);
        }
    }
}