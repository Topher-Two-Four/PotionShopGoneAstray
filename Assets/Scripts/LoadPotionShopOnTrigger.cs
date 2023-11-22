using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPotionShopOnTrigger : MonoBehaviour
{

    // Switch to potion shop scene on collision trigger with this game object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.SwitchSceneToPotionLevel(); // Switch scene to potion shop
        }
    }
}
