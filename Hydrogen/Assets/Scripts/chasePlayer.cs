using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasePlayer : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float jumpForce;
    private Rigidbody2D enemyBody;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (direction.x > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
    }

    void Jump()
    {
        enemyBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
//helloo
