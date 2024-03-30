using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrogenCollision : MonoBehaviour
{
    [SerializeField] private float maxHydrogen;
    private float currentHydrogen;
    [SerializeField] private float hydrogenAdd;

    private void Awake()
    {
        currentHydrogen = 10f;
    }

    private void Update()
    {
        print(currentHydrogen);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hydrogen"))
        {
            currentHydrogen += hydrogenAdd;
            Destroy(collision.gameObject);
        }
    }
}
