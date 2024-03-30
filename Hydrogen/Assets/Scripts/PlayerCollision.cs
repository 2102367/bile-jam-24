using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float maxHydrogen;
    private float currentHydrogen;
    [SerializeField] private float startHydrogen;
    [SerializeField] private float hydrogenAdd;
    [SerializeField] private Slider hydrogenMeter;
    [SerializeField] private float hydrogenDecayMulti;
    private float hydrogenMeterValue;

    [SerializeField] private TextMeshProUGUI interactText;

    private void Awake()
    {
        currentHydrogen = startHydrogen;
        interactText.gameObject.SetActive(false);
    }
    private void Update()
    {
        currentHydrogen -= hydrogenDecayMulti * Time.deltaTime;
        hydrogenMeterValue = currentHydrogen / maxHydrogen; //convert current hydrogen to a number between 0-1 for slider
        hydrogenMeter.value = hydrogenMeterValue;

        if (currentHydrogen <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart scene on death
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hydrogen"))
        {
            currentHydrogen += hydrogenAdd;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("door"))
        {
            //show popup
            interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("door"))
        {
            print("on door");
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //go to next level
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("door"))
        {
            //remove popup
            interactText.gameObject.SetActive(false);
        }
    }
}
