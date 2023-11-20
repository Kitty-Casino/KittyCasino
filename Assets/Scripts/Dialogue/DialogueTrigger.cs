using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; // add as many as needed to be played at correct story points
    public Dialogue dialogueBartending; 
    public Dialogue dialogueMatching; 
    public Dialogue dialogueBlackjack; 
    public Dialogue dialoguePoker; 
    public Dialogue dialogueSlots; 
    public DialogueManager dialogueManager;
    public int storyProgression; // needs to be updated with which part of the story is happening so the right dialogue can be shown
    public bool isMainManager = false;

    public void TriggerDialogue()
    {
        if (isMainManager)
        {
            storyProgression = PlayerPrefs.GetInt("Progression");

            switch (storyProgression)
            {
                case 0:
                    dialogueManager.StartDialogue(dialogueBartending); // will need to change every story point
                    break;
                case 1:
                    dialogueManager.StartDialogue(dialogueMatching); 
                    break;
                case 2:
                    dialogueManager.StartDialogue(dialogueBlackjack); 
                    break;
                case 3:
                    dialogueManager.StartDialogue(dialoguePoker); 
                    break;
                case 4:
                    dialogueManager.StartDialogue(dialogueSlots); 
                    break;
                default:
                    dialogueManager.StartDialogue(dialogue); 
                    break;
            }
        }
        else
        {
            dialogueManager.StartDialogue(dialogue);
        }
    }
}
