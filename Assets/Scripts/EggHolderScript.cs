using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHolderScript : MonoBehaviour
{
    [Header("Trail Settings")]
    public int trailLength = 3;                 // how many trailing positions
    public float recordInterval = 0.1f;         // how often to record position
    public float followSpeed = 10f;             // smoothness of following

    [Header("Trail Spacing")]
    public int spacing = 2; // how many history steps between followers
    [Header("References")]
    public GameObject itemPrefab;               // prefab to instantiate when pickup happens

    private List<Vector3> positionHistory = new List<Vector3>();
    private List<Transform> followers = new List<Transform>();
    private float recordTimer = 0f;

    void Start()
    {
        // Pre-fill the history with starting position
        for (int i = 0; i < trailLength * spacing; i++) // extra buffer for smoothness
            positionHistory.Add(transform.position);
    }

    void Update()
    {
        // Record player position at intervals
        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            positionHistory.Insert(0, transform.position);
            positionHistory.RemoveAt(positionHistory.Count - 1);
            recordTimer = 0f;
        }

        // Update follower positions
        for (int i = 0; i < followers.Count; i++)
        {
            int index = Mathf.Min((i + 1) * spacing, positionHistory.Count - 1);

            Vector3 targetPos = positionHistory[index];
            followers[i].position = Vector3.Lerp(
                followers[i].position,
                targetPos,
                followSpeed * Time.deltaTime
            );
        }
    }

    // Call this when the player picks up an item
    public void AddFollower()
    {
        GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        followers.Add(newItem.transform);
    }

    public void RemoveFollower()
    {
        if (followers.Count > 0)
        {
            // Get the last follower in the list
            Transform lastFollower = followers[followers.Count - 1];

            // Remove it from the list
            followers.RemoveAt(followers.Count - 1);

            // Destroy the GameObject so it disappears
            if (lastFollower != null)
                Destroy(lastFollower.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Egg"))
        {
            // Add a new follower
            AddFollower();

            // Destroy the Egg object so it disappears after pickup
            Destroy(collision.collider.gameObject);
        }
    }
    public int FollowerCount => followers.Count;

    public Transform PopLastFollower()
    {
        if (followers.Count == 0) return null;

        Transform last = followers[followers.Count - 1];
        followers.RemoveAt(followers.Count - 1);
        return last;
    }
}

