using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_trigger_script : MonoBehaviour
{
    public GameObject TextBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TextBox.SetActive(true);
    }
}
