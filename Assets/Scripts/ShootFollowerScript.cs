using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootFollowerScript : MonoBehaviour
{
    public EggHolderScript trailingItems;         // reference to your trail script
    public GameObject projectilePrefab;         // prefab to instantiate
    EnableActions enableActions;
    private float coolDownTimer;
    public float coolDownDuration;
    public Rigidbody2D rb;

    public float shootForce = 10f;
    public float recoilDuration;
    public float recoilTimer;
    public float recoil;// how fast the projectile launches
    public PlayerMovement playerMovement;

    void Start()
    {
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
        enableActions = GetComponent<EnableActions>();
        coolDownTimer = coolDownDuration;
        recoilTimer = 0;

    }
    void Update()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDir = (mouseScreenPosition - (Vector2)transform.position).normalized;
       

        if (Input.GetMouseButtonDown(1) && enableActions.actionsEnabled &&
            coolDownTimer <= 0) // Right click
        {
            ShootFollower();
            coolDownTimer = coolDownDuration;
           
            
            StartCoroutine(ApplyKnockback(shootDir, recoil, recoilDuration));
            //recoilTimer = recoilDuration;
           
        }/*
        if(recoilTimer <= 0)
        {
            playerMovement.enabled = true;
            recoilTimer -= Time.deltaTime;
        }*/
        
        coolDownTimer -= Time.deltaTime;
        
    }

    void ShootFollower()
    {
        // Make sure we actually have a follower to shoot
        if (trailingItems == null || trailingItems.FollowerCount == 0)
            return;



        // Start cooldown
        StartCoroutine(trailingItems.PickupCooldownRoutine());

        // 1. Remove the last follower from the trail
        Transform lastFollower = trailingItems.PopLastFollower();
        if (lastFollower == null) return;

        // 2. Create a projectile at that position
        GameObject proj = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // 3. Apply force in the direction the player is facing
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
            // NOTE: using transform.up assumes your player "forward" is up
        }

        // Destroy the old follower object
        Destroy(lastFollower.gameObject);
    }

    IEnumerator ApplyKnockback(Vector2 direction, float force, float duration)
    {
        if (playerMovement != null)
            playerMovement.canMove = false;

        rb.AddForce(-direction * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        if (playerMovement != null)
            playerMovement.canMove = true;
    }
}

