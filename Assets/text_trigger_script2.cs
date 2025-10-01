using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_trigger_script2 : MonoBehaviour
{
    public GameObject TextBox;
    // Start is called before the first frame update
    void Start()
    {
        /*if(TextBox != null)
        {
            TextBox.SetActive(false);
        }*/
        
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && TextBox != null)
        {
            TextBox.SetActive(true);
        }
        
    }
}
