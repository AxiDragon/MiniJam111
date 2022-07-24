using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueText dialogue;
    [SerializeField] string triggerTag = "Player";
    bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (other.CompareTag(triggerTag))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        dialogue.gameObject.SetActive(true);
        dialogue.ShowDialogue();
        triggered = true;
    }
}
