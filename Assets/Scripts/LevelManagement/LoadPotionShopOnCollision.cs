using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPotionShopOnCollision : MonoBehaviour
{
    [Header("Time Removed On Collision:")]
    [Tooltip("The amount of time to remove when collision with player occurs.")]
    [SerializeField] private float caughtTimeRemoveAmount = 120.0f;

    // Switch to potion shop scene on collision with this game object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.timeRemaining -= caughtTimeRemoveAmount; // Remove time from day if player is caught by enemy in maze
            GameManager.Instance.SwitchSceneToPotionLevel(); // Switch scene to potion shop
        }
    }

}
