using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Object")]
public class DialogueObject : ScriptableObject
{
    [SerializeField]
    private DialogueText[] dialogue;

    [SerializeField]
    private Actor[] actors;

    [SerializeField]
    private Choice[] choices;

    public DialogueText[] Dialogue => dialogue;
    
    public Actor[] Actors => actors;

    public Choice[] Choices => choices;

    public bool HasChoices => Choices != null && Choices.Length > 0;
}

[System.Serializable]
public class DialogueText
{
    [SerializeField]
    private int actorId;
    [SerializeField]
    [TextArea]
    private string dialogueText;

    public int ActorId => actorId;

    public string Text => dialogueText;
}

[System.Serializable]
public class Actor
{
    [SerializeField]
    private string actorName;

    public string ActorName => actorName;
}
