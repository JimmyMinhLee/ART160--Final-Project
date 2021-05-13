using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class NPC : MonoBehaviour
{

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    private bool interactable = false;

    public List<string> NPCTexts;
    public int currentTextToDisplay = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player is in talking range of this NPC
            interactable = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactable = false;
            if (dialogueBox.activeInHierarchy == true) 
            {
                dialogueBox.SetActive(false); 
            }
            // Player is no longer in talking range of the NPC 
        }
    }

    private void Update()
    {
        if (interactable & Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueBox.activeInHierarchy == false)
            {
                dialogueBox.SetActive(true);
                dialogueText.text = NPCTexts[currentTextToDisplay]; 
            }
        }

        if (dialogueBox.activeInHierarchy == true & Input.GetKeyDown(KeyCode.G))
        {
            if (currentTextToDisplay == NPCTexts.Count - 1)
            {
                currentTextToDisplay = 0;
                dialogueBox.SetActive(false); 
            }

            else
            {
                currentTextToDisplay += 1;
                dialogueText.text = NPCTexts[currentTextToDisplay];
            }

        }
    }
}
