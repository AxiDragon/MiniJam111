using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class DialogueText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] DialogueSegment[] dialogue;
    [SerializeField] UnityEvent finishEvent;
    [SerializeField] float characterInterval = .05f;
    RectTransform rectTransform;
    Vector3 displayed;
    Vector3 hidden;
    int currentDialogue = 0;
    int dialogueLength;
    IEnumerator timer;
    IEnumerator textTyper;

    [Serializable]
    struct DialogueSegment
    {
        public Character character;
        [TextArea]
        public string content;
        public UnityEvent advanceEvent;
        public float customInterval;
    }

    enum Character
    {
        Clover,
        Radio
    }

    private void Awake()
    {
        dialogueLength = dialogue.Length;
        rectTransform = GetComponent<RectTransform>();
        displayed = rectTransform.position;
        hidden = displayed - Vector3.up * 300f;

        rectTransform.position = hidden;

        ShowDialogue();
        DisplayText();
    }

    private void DisplayText()
    {
        if (textTyper != null)
            StopCoroutine(textTyper);

        DialogueSegment dialogueSegment = dialogue[currentDialogue];
        title.text = dialogueSegment.character.ToString();

        float typeInterval = dialogueSegment.customInterval == 0f ? characterInterval : dialogueSegment.customInterval;

        textTyper = TextTyper(dialogueSegment.content, typeInterval);
        StartCoroutine(textTyper);
    }

    public void AdvanceDialogue(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame())
        {
            if (timer != null)
                StopCoroutine(timer);

            dialogue[currentDialogue].advanceEvent.Invoke();
            currentDialogue++;
            
            ClampIndex();
            DisplayText();
        }
    }

    public void RetractDialogue(InputAction.CallbackContext callback)
    {
        if (callback.action.WasPerformedThisFrame())
        {
            if (timer != null)
                StopCoroutine(timer);

            dialogue[currentDialogue].advanceEvent.Invoke();
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
        
        currentDialogue = Mathf.Clamp(currentDialogue, 0, dialogueLength - 1);
    }

    public void AdvanceTimer(float time)
    {
        timer = AdvanceTimerCoroutine(time);
        StartCoroutine(timer);
    }

    IEnumerator AdvanceTimerCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        dialogue[currentDialogue].advanceEvent.Invoke();
        currentDialogue++;

        ClampIndex();
        DisplayText();
    }

    IEnumerator TextTyper(string text, float interval)
    {
        string textString = "";

        for (int i = 0; i < text.Length; i++)
        {
            textString += text[i];
            content.text = textString;
            yield return new WaitForSeconds(interval);
        }
    }

    public void CloseDialogue()
    {
        LeanTween.move(rectTransform, hidden, 1f).setEase(LeanTweenType.easeInOutCubic);
    }

    public void ShowDialogue()
    {
        LeanTween.move(rectTransform, displayed, 1f).setEase(LeanTweenType.easeInOutCubic);
    }
}
