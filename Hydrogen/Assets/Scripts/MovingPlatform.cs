using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] int startingPoint;
    [SerializeField] private Transform[] points;
    [SerializeField] private float platformVelocity;

    private int i;

    void Start()
    {
        transform.position = points[startingPoint].position;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, platformVelocity * Time.deltaTime);
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
    */
}


