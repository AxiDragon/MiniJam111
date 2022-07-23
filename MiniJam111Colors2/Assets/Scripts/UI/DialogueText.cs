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
        Radio
    }

    private void Awake()
    {
        closeAction = () => { gameObject.SetActive(false); };
        dialogueLength = dialogue.Length;
        rectTransform = GetComponent<RectTransform>();
        print(rectTransform);
        displayTransform = rectTransform;
        hideTransform = displayTransform;
        hideTransform.position -= Vector3.one * 200f;

        rectTransform = hideTransform;

        gameObject.SetActive(false);
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
            
            if (ClampIndex())
                DisplayText();
        }
    }

    private bool ClampIndex()
    {
        if (currentDialogue >= dialogueLength)
        {
            finishEvent.Invoke();
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
        LeanTween.move(rectTransform, rectTransform.position + rectTransform.up * -300f, 1f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(closeAction);
    }

    public void ShowDialogue()
    {
        LeanTween.move(rectTransform, rectTransform.position + rectTransform.up * 200f, 1f).setEase(LeanTweenType.easeInOutCubic);
        DisplayText();
    }
}
