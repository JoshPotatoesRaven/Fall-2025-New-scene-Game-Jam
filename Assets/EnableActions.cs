using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableActions : MonoBehaviour
{
    // Start is called before the first frame update

    public bool actionsEnabled = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.CompareTag("NoShoot"))
        {
            actionsEnabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.CompareTag("NoShoot"))
        {
            actionsEnabled = true;
        }
    }
}
