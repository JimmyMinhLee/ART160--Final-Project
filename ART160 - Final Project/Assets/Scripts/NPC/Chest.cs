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
    Animator chestAnimator;


    private void Start()
    {
        chestAnimator = GetComponent<Animator>(); 
    }
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

        if (chestAnimator.GetBool("isOpen") == true)
            {
                chestAnimator.SetBool("isOpen", false);
            }
        }
    }

    private void Update()
    {
        if (interactable & Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueBox.activeInHierarchy == false)
            {
                Debug.Log("opening chest"); 
                chestAnimator.SetBool("isOpen", true); 
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

        if (dialogueBox.activeInHierarchy & Input.GetKeyDown(KeyCode.G))
        {
            dialogueBox.SetActive(false);
            chestAnimator.SetBool("isOpen", false); 
        }
    }
}
