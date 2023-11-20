using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPotionShopOnCollision : MonoBehaviour
{

    // Switch to potion shop scene on collision with this game object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            SceneManager.LoadScene(2);
            GameManager.Instance.SwitchSceneToPotionLevel(); // Switch scene to potion shop
        }
    }

}
