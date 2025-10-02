using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_controller_script : MonoBehaviour
{
    public GameObject portal2;
    //private SpriteRenderer spriteRenderer;
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
        if(other.CompareTag("Player"))
        {
            portal2.GetComponent<SpriteRenderer>().enabled = true;
        }
        
     
    }
}
