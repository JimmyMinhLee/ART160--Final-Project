using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextSceneName;

    public bool ableToTeleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player is inside portal"); 
            ableToTeleport = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player has left portal"); 
            ableToTeleport = false; 
        }
    }

    private void Update()
    {
        if (ableToTeleport)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (nextSceneName != null)
                {
                    SceneManager.LoadScene(nextSceneName);
                }
            }

        }

    }
}
