using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float startSpeed;
    public float xtraSpeed;
    public float maxXtraSpeed;
    private int hitCounter = 0;
    public bool P1Start = true;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Launch());
    }

    public void RestartBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
    public IEnumerator Launch()
    {
        RestartBall();
        hitCounter = 0;
        yield return new WaitForSeconds(2);
        if (P1Start)
        {
            MoveBall(new Vector2(-1,0));
           
        }
        else
        {
            MoveBall(new Vector2(1,0));
            
        }
        
    }
    public void MoveBall(Vector2 direction)
    {
        direction = direction.normalized;

        float ballSpeed = startSpeed + hitCounter * xtraSpeed;

        rb.velocity = direction * ballSpeed;
    }

    public void IncreaseCounter()
    {
        if (hitCounter * xtraSpeed < maxXtraSpeed)
        {
            hitCounter++;
        }
    }
}
