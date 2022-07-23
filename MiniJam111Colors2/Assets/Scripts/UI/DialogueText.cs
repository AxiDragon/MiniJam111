using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class DialogueText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] DialogueSegment[] dialogue;
    [SerializeField] UnityEvent finishEvent;
    int currentDialogue = 0;
    int dialogueLength;

    [Serializable]
    struct DialogueSegment
    {
        public string title;
        [TextArea]
        public string content;
        public UnityEvent advanceEvent;
    }

    private void Awake()
    {
        dialogueLength = dialogue.Length;
        DisplayText();
    }

    private void DisplayText()
    {
        DialogueSegment dialogueSegment = dialogue[currentDialogue];
        title.text = dialogueSegment.title;
        content.text = dialogueSegment.content;
    }

    public void AdvanceDialogue(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame())
        {
            if (dialogue[currentDialogue].advanceEvent != null)
            {
                dialogue[currentDialogue].advanceEvent.Invoke();
            }

            currentDialogue++;
            DisplayText();
        }
    }

    public void RetractDialogue(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame())
        {
            currentDialogue--;
            ClampIndex();
            DisplayText();
        }
    }

    private void ClampIndex()
    {
        if (currentDialogue >= dialogueLength)
        {
            finishEvent.Invoke();
        }
        else
        {
            currentDialogue = Mathf.Clamp(currentDialogue, 0, dialogueLength - 1);
        }
    }
}
