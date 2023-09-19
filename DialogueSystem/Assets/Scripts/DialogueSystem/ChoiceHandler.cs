using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceHandler : MonoBehaviour
{
    [SerializeField]
    private RectTransform choiceContainer;
    [SerializeField]
    private RectTransform choiceTemplate;
    [SerializeField]
    private DialogueManager dialogueManager;

    List<GameObject> choiceButtons = new List<GameObject>();

    private void Start()
    {
        choiceTemplate.gameObject.SetActive(false);
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void ShowChoices(Choice[] choices)
    {
        float choiceContainerHeight = 0;

        foreach (Choice choice in choices) 
        {
            GameObject choiceButton = Instantiate(choiceTemplate.gameObject, choiceContainer );
            choiceButton.gameObject.SetActive( true );
            choiceButton.GetComponentInChildren<Text>().text = choice.ChoiceText;
            choiceButton.GetComponent<Button>().onClick.AddListener(() => OnPickedChoice(choice));

            choiceButtons.Add(choiceButton);

            choiceContainerHeight += choiceTemplate.sizeDelta.y;
        }

        choiceContainer.sizeDelta = new Vector2(choiceContainer.sizeDelta.x, choiceContainerHeight);
        choiceContainer.gameObject.SetActive( true );
    }

    private void OnPickedChoice(Choice choice)
    {
        choiceTemplate.gameObject.SetActive(false);

        foreach(GameObject button in choiceButtons)
        {
            Destroy(button);
        }
        choiceButtons.Clear();

        dialogueManager.OpenDialogue(choice.DialogueObject);
    }
}
