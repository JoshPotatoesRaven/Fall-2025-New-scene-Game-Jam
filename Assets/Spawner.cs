using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public float spawnTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Call SpawnEnemy() first after 10s, then repeat every 10s
        InvokeRepeating(nameof(SpawnEnemy), 10f, 10f);
    }

    void SpawnEnemy()
    {
        Debug.Log("Spawned Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
