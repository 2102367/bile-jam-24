using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HydrogenCollision : MonoBehaviour
{
    [SerializeField] private float maxHydrogen;
    private float currentHydrogen;
    [SerializeField] private float startHydrogen;
    [SerializeField] private float hydrogenAdd;
    [SerializeField] private Slider hydrogenMeter;
    [SerializeField] private float hydrogenDecayMulti;
    private float hydrogenMeterValue;

    private void Awake()
    {
        currentHydrogen = startHydrogen;
    }
    private void Update()
    {
        currentHydrogen -= hydrogenDecayMulti * Time.deltaTime;
        hydrogenMeterValue = currentHydrogen / maxHydrogen; //convert current hydrogen to a number between 0-1 for slider
        hydrogenMeter.value = hydrogenMeterValue;
        print(hydrogenMeterValue);

        if (currentHydrogen <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
