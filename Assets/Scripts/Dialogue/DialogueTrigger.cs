using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; // add as many as needed to be played at correct story points
    public DialogueManager dialogueManager;
    public int storyProgression; // needs to be updated with which part of the story is happening so the right dialogue can be shown

    public void TriggerDialogue()
    {
        switch (storyProgression)
        {
            case 0:
                dialogueManager.StartDialogue(dialogue); // will need to change every story point
                break;
        }
    }
}
