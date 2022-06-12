using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public BoxCollider2D grid;
    public Snake Sc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomPos();
        }

    }

    private void RandomPos()
    {
        Bounds bounds = grid.bounds;

        int x = Random.Range((int)bounds.min.x, (int)bounds.max.x);
        int y = Random.Range((int)bounds.min.y, (int)bounds.max.y);
        foreach (Transform part in Sc.body)
        {
           if (Mathf.Pow(part.position.x - x,2) + Mathf.Pow(part.position.y - y,2) < 2)
           { 
               RandomPos();
               return;
           }
           
        }
        this.transform.position = new Vector3(
                   Mathf.Round(x),
                   Mathf.Round(y), 
                   0f); 
        
    }
}
