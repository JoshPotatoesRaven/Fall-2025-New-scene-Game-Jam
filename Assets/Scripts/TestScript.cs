using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public float myVar;
    public GameObject myObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 currentPos = transform.position;
        transform.position += Vector3.left * Time.deltaTime * myVar;
        //gameObject.SetActive()
    }

    
}
