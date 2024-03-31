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
        //Vector2 directionEnemy = enemyBody.velocity.normlized;

        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Check for obstacles and jump
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);
        // if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
        // {
        //     Jump();
        // }

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
