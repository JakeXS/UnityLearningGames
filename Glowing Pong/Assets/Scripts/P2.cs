using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2 : MonoBehaviour
{
    public float RacketSpeed;

    private Rigidbody2D rb;
    private Vector2 racketDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical2");
        racketDirection = new Vector2(0, directionY).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = racketDirection * RacketSpeed;
    }
}
