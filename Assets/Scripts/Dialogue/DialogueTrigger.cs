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
    public GameObject dialogueIcon;
    public int storyProgression; // needs to be updated with which part of the story is happening so the right dialogue can be shown
    public bool isMainManager = false;

    void Start()
    {
        if (isMainManager)
        {
            storyProgression = PlayerPrefs.GetInt("Progression");
            switch (storyProgression)
            {
                case 0:
                    dialogueIcon.SetActive(true);
                    break;
                case 1:
                    dialogueIcon.SetActive(true);
                    break;
                case 2:
                    dialogueIcon.SetActive(true);
                    break;
                case 3:
                    dialogueIcon.SetActive(true);
                    break;
                case 4:
                    dialogueIcon.SetActive(true);
                    break;
                default:
                    dialogueIcon.SetActive(false);
                    break;
            }
        }
    }
    public void TriggerDialogue()
    {
        if (isMainManager)
        {
            storyProgression = PlayerPrefs.GetInt("Progression");

            switch (storyProgression)
            {
                case 0:
                    dialogueManager.StartDialogue(dialogueBartending); 
                    dialogueIcon.SetActive(false);
                    break;
                case 1:
                    dialogueManager.StartDialogue(dialogueMatching); 
                    dialogueIcon.SetActive(false);
                    break;
                case 2:
                    dialogueManager.StartDialogue(dialogueBlackjack);
                    dialogueIcon.SetActive(false);
                    break;
                case 3:
                    dialogueManager.StartDialogue(dialoguePoker); 
                    dialogueIcon.SetActive(false);
                    break;
                case 4:
                    dialogueManager.StartDialogue(dialogueSlots);
                    dialogueIcon.SetActive(false);
                    break;
                default:
                    dialogueManager.StartDialogue(dialogue); 
                    dialogueIcon.SetActive(false);
                    break;
            }
            PlayerPrefs.Save();
        }
        else
        {
            dialogueManager.StartDialogue(dialogue);
            dialogueIcon.SetActive(false);
        }
    }
}
