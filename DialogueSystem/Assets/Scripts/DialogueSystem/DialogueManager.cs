using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text actorName;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private DialogueObject dialogueData;
    [SerializeField]
    private GameObject dialogueBox;
    [SerializeField]
    private GameObject spacebarUI;

    private Actor[] actor;

    [SerializeField]
    private TypeEffect typeEffect;
    [SerializeField]
    private ChoiceHandler choiceHandler;
    
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spacebarUI.SetActive(false);
        typeEffect = GetComponent<TypeEffect>();
        choiceHandler = GetComponent<ChoiceHandler>();
        player = FindObjectOfType<ThirdPersonController>().gameObject;
        CloseDialogue();
    }

    public void OpenDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        enablePlayerMovement(false);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private void enablePlayerMovement(bool isEnable)
    {
        player.GetComponent<PlayerInput>().enabled = isEnable;
        Cursor.visible = !isEnable;
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        actor = dialogueObject.Actors;

        for(int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            DialogueText dialogue = dialogueObject.Dialogue[i];
            actorName.text = actor[dialogue.ActorId].ActorName;
            yield return typeEffect.Run(dialogue.Text, dialogueText);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasChoices) break;

            spacebarUI.active = true;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            spacebarUI.active = false;
        }

        if(dialogueObject.HasChoices)
        {
            choiceHandler.ShowChoices(dialogueObject.Choices);
        }
        else
        {
            CloseDialogue();
        }
    }

    private void CloseDialogue()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = string.Empty;
        enablePlayerMovement(true);
    }
    
}
