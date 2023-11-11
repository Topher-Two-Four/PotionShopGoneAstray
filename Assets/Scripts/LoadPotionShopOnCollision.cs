using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPotionShopOnCollision : MonoBehaviour
{

    // Switch to potion shop scene on collision with this game object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.SwitchSceneToPotionLevel(); // Switch scene to potion shop
        }
    }

}
