using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using TarodevController;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float maxHydrogen;
    private float currentHydrogen;
    [SerializeField] private float startHydrogen;
    [SerializeField] private float hydrogenAdd;
    [SerializeField] private Slider hydrogenMeter;
    [SerializeField] private float hydrogenDecayMulti;
    private float hydrogenMeterValue;
    [SerializeField] private float jetpackCostPerJump;
    private bool isDraining = true;

    private bool hasJetpack = false;
    public ScriptableStats _jetPack;

    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private PlayerController playerControllerScript;

    private SignText signText;

    private void Awake()
    {
        currentHydrogen = startHydrogen;
        interactText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);

    }
    private void Update()
    {
        if (isDraining)
        {
            currentHydrogen -= hydrogenDecayMulti * Time.deltaTime;
        }

        hydrogenMeterValue = currentHydrogen / maxHydrogen; //convert current hydrogen to a number between 0-1 for slider
        hydrogenMeter.value = hydrogenMeterValue;

        if (currentHydrogen <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart scene on death
        }

        if(Input.GetKeyDown(KeyCode.Space) || hasJetpack) 
        {
            currentHydrogen -= jetpackCostPerJump;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hydrogen"))
        {
            currentHydrogen += hydrogenAdd;
            if (currentHydrogen > maxHydrogen)
            {
                currentHydrogen = maxHydrogen;
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("door") || collision.gameObject.CompareTag("jetpack") || collision.gameObject.CompareTag("dialogue"))
        {
            //show popup
            interactText.gameObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("refill"))
        {
            isDraining = false;
            currentHydrogen = maxHydrogen;
        }

        if (collision.gameObject.CompareTag("dialogue"))
        {
            signText = collision.gameObject.GetComponent<SignText>();
            dialogueText.text = signText.text;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("door"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //go to next level
            }
        }

        if (collision.gameObject.CompareTag("jetpack"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                hasJetpack = true;
                playerControllerScript._stats = _jetPack;
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("dialogue"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                //show dialogue
                dialogueText.gameObject.SetActive(true);
                interactText.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("door") || collision.gameObject.CompareTag("jetpack") || collision.gameObject.CompareTag("dialogue"))
        {
            //remove popup
            interactText.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("refill"))
        {
            isDraining = true;
        }

        if (collision.gameObject.CompareTag("dialogue"))
        {
            //hide dialogue
            dialogueText.gameObject.SetActive(false);
        }
    }
}
