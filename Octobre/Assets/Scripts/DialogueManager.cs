using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text dialogueBox;
    public Queue<string> sentences;
    public float typingSpeed;

    private string sentence;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}
	
    public void StartDialogue (Dialogue dialogue)
    {
        Debug.Log("Talks:" + dialogue.name);

        sentences.Clear();
        typingSpeed = dialogue.typingSpeed;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //si il a pas fini de type er qu'il a pas pas commencé, fini de type
        if ((dialogueBox.text != " " + sentence ) && (dialogueBox.text != "") && (dialogueBox.text != sentence))
        {
           
           // IEnumerator stillType = Type();
            StopAllCoroutines();
            //StopCoroutine(Type());
            dialogueBox.text = sentence;
        }
        else // si il a fini, change de phrase
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            sentence = sentences.Dequeue();
            dialogueBox.text = " ";
            StartCoroutine(Type());
        }
        
    }

    public void EndDialogue()
    {
        Debug.Log("End of conv");
        dialogueBox.text = "";
        StopAllCoroutines();
        //IEnumerator stillType = Type();
        //StopCoroutine(Type());
    }

    public IEnumerator Type()
    {
        char[] letters = sentence.ToCharArray();
        foreach (char letter in letters)
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }     
    }



}
