using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_script : MonoBehaviour
{
    public GameObject Player;
    public Camera MainCamera;
    public Vector3 location;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.position = location;
            MainCamera.transform.position = new Vector3(location.x, location.y, location.z - 10);
        }
    }
}
