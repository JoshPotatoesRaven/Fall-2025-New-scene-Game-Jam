using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //public GameObject AttackHitbox;
    public float attackDuration = 0.2f;
    public float cooldownTimer = 100f;
    public float cooldownDuration = 100f;
    public bool isAttacking = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && cooldownTimer <= 0f)
        {
            PerformAttack();
            cooldownTimer = cooldownDuration;
        }
        cooldownTimer -= Time.deltaTime;
    }

    private void PerformAttack()
    {
        isAttacking = true;

        //AttackHitbox.setActive(true);
    }
}
