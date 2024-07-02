using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dialoguewriter : MonoBehaviour
{
    public TMP_Text text;
    public float typingSpeed = 0.05f; // Adjust the speed to control the typing animation
    public AudioSource voice;
    private string dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText = text.text; // Get the dialogue text from the TMP_Text component
        text.text = ""; // Clear the text to make it appear gradually

     
    }

    private IEnumerator WriteText()
    {
        foreach (char letter in dialogueText)
        {
            text.text += letter; // Add the next letter to the text

            // Wait for a short time to simulate typing animation
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void play()
    {
        StartCoroutine(WriteText());
        voice.Play();
    }
}
