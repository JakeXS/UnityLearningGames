using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public BallController ballController;
    public ScoreManager scoreManager;
    public GameObject hitSFX;
    private void Bounce(Collision2D collision)
    {
        
        Vector3 ballPos = transform.position;
        Vector3 racktPos = collision.transform.position;
        float racketHeight = collision.collider.bounds.size.y;
        float posX;
        if (collision.gameObject.name == "Player1")
        {
            posX = 1;
        }
        else
        {
            posX = -1;
        }

        float posY = (ballPos.y - racktPos.y) / racketHeight;
        ballController.IncreaseCounter();
        ballController.MoveBall(new Vector2(posX, posY));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2" )
        {
            Bounce(collision);
        }
        else if (collision.gameObject.name == "RightBorder")
        {
            scoreManager.P1Goal();
            ballController.P1Start = false;
            StartCoroutine(ballController.Launch());
        }
        else if (collision.gameObject.name == "LeftBorder")
        {
            scoreManager.P2Goal();
            ballController.P1Start = true;
            StartCoroutine(ballController.Launch());
        }

        Instantiate(hitSFX, transform.position, transform.rotation);
    }
}
