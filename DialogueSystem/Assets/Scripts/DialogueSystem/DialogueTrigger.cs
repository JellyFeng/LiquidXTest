using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private DialogueObject dialogueObject;
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private GameObject interactUI;

    bool playerInRange = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();    
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E)) 
        {
            interactUI.SetActive(false);
            dialogueManager.OpenDialogue(dialogueObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetPlayerInteractable(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetPlayerInteractable(false);
        }
    }

    private void SetPlayerInteractable(bool isInRange)
    {
        interactUI.SetActive(isInRange);
        playerInRange = isInRange;
    }
}
