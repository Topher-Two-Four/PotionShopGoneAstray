using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPotionShopOnCollision : MonoBehaviour
{
    [Header("Time Removed On Collision:")]
    [Tooltip("The amount of time to remove when collision with player occurs.")]
    [SerializeField] private float caughtTimeRemoveAmount = 120.0f;
    [Tooltip("The amount of items to remove from player's inventory when collision with them occurs.")]
    [SerializeField] private int caughtItemRemoveAmount = 3;

    private bool timeRemoved = false;

    // Switch to potion shop scene on collision with this game object
    private void OnCollisionEnter(Collision collision)
    {
        if (!timeRemoved)
        {
            if (collision.gameObject.tag == "Player")
            {
                for (int i = 0; i < caughtItemRemoveAmount; i++)
                {
                    InventoryController.Instance.RemoveRandomItemFromGrid();
                    //Debug.Log("Trying to remove random item from collision script...");
                }
                timeRemoved = true;
                if (gameObject.tag == "MazeEnemy")
                {
                    CutsceneManager.Instance.PlayBirdCatchCutscene();
                } 

                if (gameObject.tag == "FallBoundary")
                {
                    CutsceneManager.Instance.PlayFallCutscene();
                }

                GameManager.Instance.timeRemaining -= caughtTimeRemoveAmount; // Remove time from day if player is caught by enemy in maze
                GameManager.Instance.SwitchSceneToPotionLevel(); // Switch scene to potion shop
            }
        }
    }

}
