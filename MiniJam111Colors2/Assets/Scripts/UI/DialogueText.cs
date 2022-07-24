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
    [SerializeField] bool showAtStart = false;
    bool canInteract = true;
    RectTransform rectTransform;
    RectTransform displayTransform;
    RectTransform hideTransform;
    int currentDialogue = 0;
    int dialogueLength;
    IEnumerator timer;
    IEnumerator textTyper;

    Action closeAction;

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
        Radio,
        Covorveil
    }

    private void Awake()
    {
        closeAction = () => { gameObject.SetActive(false); };
        dialogueLength = dialogue.Length;
        rectTransform = GetComponent<RectTransform>();
        displayTransform = rectTransform;
        hideTransform = displayTransform;
        hideTransform.position -= Vector3.one * 200f;

        rectTransform = hideTransform;

        if (!showAtStart)
            gameObject.SetActive(false);
    }

    private void Start()
    {
        if (showAtStart)
        {
            ShowDialogue();
        }
    }

    private void DisplayText()
    {
        if (textTyper != null)
            StopCoroutine(textTyper);

        if (currentDialogue >= dialogueLength)
            return;

        DialogueSegment dialogueSegment = dialogue[currentDialogue];
        title.text = dialogueSegment.character.ToString();

        float typeInterval = dialogueSegment.customInterval == 0f ? characterInterval : dialogueSegment.customInterval;

        textTyper = TextTyper(dialogueSegment.content, typeInterval);
        StartCoroutine(textTyper);
    }

    public void AdvanceDialogue(InputAction.CallbackContext callback)
    {
        if (!canInteract)
            return;

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
        if (!canInteract)
            return;

        if (callback.action.WasPerformedThisFrame())
        {
            if (timer != null)
                StopCoroutine(timer);

            dialogue[currentDialogue].advanceEvent.Invoke();
            currentDialogue--;
            
            if (ClampIndex())
                DisplayText();
        }
    }

    private bool ClampIndex()
    {
        if (currentDialogue >= dialogueLength)
        {
            finishEvent.Invoke();
            canInteract = false;
            return false;
        }

        currentDialogue = Mathf.Clamp(currentDialogue, 0, dialogueLength - 1);

        return true;
    }

    public void AdvanceTimer(float time)
    {
        timer = AdvanceTimerCoroutine(time);
        StartCoroutine(timer);
    }

    IEnumerator AdvanceTimerCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
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
            yield return new WaitForSecondsRealtime(interval);
        }
    }

    public void CloseDialogue()
    {
        LeanTween.move(rectTransform, rectTransform.position + rectTransform.up * -600f, 1f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(closeAction);
    }

    public void ShowDialogue()
    {
        LeanTween.move(rectTransform, rectTransform.position + rectTransform.up * 600f, 1f).setEase(LeanTweenType.easeInOutCubic);
        DisplayText();
    }
}
