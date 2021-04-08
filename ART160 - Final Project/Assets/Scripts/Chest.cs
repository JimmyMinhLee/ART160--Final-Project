using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Chest : MonoBehaviour
{

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public string chestText;
    private bool interactable = false;
    public List<Item> itemsContained;
    public GameObject player; 


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player is in talking range of this NPC
            interactable = true;
            player = collision.gameObject;
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
                dialogueText.text = chestText;
                dialogueBox.SetActive(true);
                PlayerInventory inventory = player.GetComponent<PlayerInventory>();
                foreach (Item currItem in itemsContained)
                {
                    inventory.AddItem(currItem);
                }

                itemsContained = new List<Item>(); // Setting to an empty list afterwards
            }
        }
    }
}
