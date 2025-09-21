using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // convert mouse position into world coordinates
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get direction you want to point at
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

        // set vector of transform directly
        transform.up = direction;

        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * speed * Time.deltaTime * 50;
    }
}
