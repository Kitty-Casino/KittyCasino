using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public GameObject dialogueObject;
    public TextMeshProUGUI dialogueText;
    public PlayerController playerController;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueObject.SetActive(false);
    }

    void Update()
    {
        if (playerController == null)
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueObject.SetActive(true);
        sentences.Clear();
        playerController.enabled = false;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        dialogueObject.SetActive(false);
        playerController.enabled = true;
    }
}
