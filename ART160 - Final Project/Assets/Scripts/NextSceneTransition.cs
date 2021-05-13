using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTransition : MonoBehaviour
{
    public Enemy enemyToDefeat;
    public string nextSceneString; 


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (enemyToDefeat.isDead)
            {
                SceneManager.LoadScene(nextSceneString);
            }
        }

    }

}
