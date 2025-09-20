using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    float collisionSpeedThreshold = 3.0f;

    public GameObject box;

    public GameObject breakText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > collisionSpeedThreshold)
        {
            GameObject otherObject = collision.collider.gameObject;
            if (otherObject.CompareTag("Egg"))
            {
                //Break self and show text.
                box.SetActive(false);
                BoxCollider2D selfCollider = gameObject.GetComponent<BoxCollider2D>();
                selfCollider.enabled = false;

                breakText.SetActive(true);
            }
        }
    }
}
