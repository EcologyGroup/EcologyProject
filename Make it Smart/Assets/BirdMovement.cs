using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birdmovement : MonoBehaviour
{
    private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position.x = transform.position.x + speed;
        if (transform.position.x >= 210f)
        {
            transform.position = new Vector2(-240f, transform.position.y);
        }
    }
}