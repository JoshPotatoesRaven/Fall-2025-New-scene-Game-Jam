using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float startTime = 5;
    public float spawnTimer = 10;
    public GameObject spawnEntity;

    // Start is called before the first frame update
    void Start()
    {
        // Call SpawnEnemy() first after startTime, then repeat every spawnTimer
        InvokeRepeating(nameof(SpawnEnemy), startTime, spawnTimer);
    }

    void SpawnEnemy()
    {
        Debug.Log("Spawned Enemy");
        Instantiate(spawnEntity, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
