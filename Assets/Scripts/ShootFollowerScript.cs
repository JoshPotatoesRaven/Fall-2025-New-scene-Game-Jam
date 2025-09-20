using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootFollowerScript : MonoBehaviour
{
    public EggHolderScript trailingItems;         // reference to your trail script
    public GameObject projectilePrefab;         // prefab to instantiate
    public float shootForce = 10f;              // how fast the projectile launches

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            ShootFollower();
        }
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
}

