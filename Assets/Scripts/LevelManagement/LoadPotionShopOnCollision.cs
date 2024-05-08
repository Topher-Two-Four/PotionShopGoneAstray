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

    private bool _timeRemoved = false;
    private bool _hasRemovedItems = false;

    // Switch to potion shop scene on collision with this game object
    private void OnCollisionEnter(Collision collision)
    {
        if (!_timeRemoved)
        {
            if (collision.gameObject.tag == "Player")
            {
                _timeRemoved = true;
                if (gameObject.tag == "MazeEnemy")
                {
                    AudioManager.Instance.PlayBirdCatchSound();
                    CutsceneManager.Instance.PlayBirdCatchCutscene();
                }
                else if (gameObject.tag == "FallBoundary")
                {
                    CutsceneManager.Instance.PlayFallCutscene();
                }
                else
                {
                    CutsceneManager.Instance.PlayLoadingCutscene();
                }

                if (!_hasRemovedItems)
                {
                    for (int i = 0; i < caughtItemRemoveAmount; i++)
                    {
                        InventoryController.Instance.RemoveRandomItemFromGrid();

                    }
                    _hasRemovedItems = true;
                    //Debug.Log(_hasRemovedItems);
                }

                GameManager.Instance.timeRemaining -= caughtTimeRemoveAmount; // Remove time from day if player is caught by enemy in maze
                GameManager.Instance.SwitchSceneToPotionLevel(); // Switch scene to potion shop
            }
        }
    }

}
