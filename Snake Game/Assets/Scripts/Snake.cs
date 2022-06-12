using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    public Transform snake;
    public List<Transform> body;
    public Transform preFab;
    public int score;
    public TextMeshProUGUI scoreText;
    private void Start()
    {
        score = 0;
        body = new List<Transform>();
        body.Add(snake);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }

        scoreText.text = $"x{score}";
    }

    private void FixedUpdate()
    {
        for (int i = body.Count - 1; i > 0; i--)
        {
            body[i].position = body[i - 1].position;
        }

        snake.position = new Vector3(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y),
            0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            score++;
        }

        if (other.tag == "Body")
        {
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.Save();
            SceneManager.LoadScene(2);
        }

    }

    private void Grow()
    {
        Transform segment = Instantiate(preFab);
        segment.position = body[body.Count - 1].position;
        body.Add(segment);
    }
    
}
