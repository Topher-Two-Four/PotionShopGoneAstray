using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPotionShopOnTrigger : MonoBehaviour
{
    [Header("Time Removed On Trigger:")]
    [Tooltip("The amount of time to remove when collision with player occurs.")]
    [SerializeField] private float caughtTimeRemoveAmount = 120.0f;
    [SerializeField] private bool backToMaze = false;

    // Switch to potion shop scene on collision trigger with this game object
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "FallBoundary")
            {
                CutsceneManager.Instance.PlayFallCutscene();
                GameManager.Instance.timeRemaining -= caughtTimeRemoveAmount; // Remove time from day if player is caught by enemy in maze
                GameManager.Instance.SwitchSceneToPotionLevel(); // Switch scene to potion shop
            }
            
            if (backToMaze)
            {
                GameManager.Instance.SwitchSceneToMazeLevel();
            }
        }
    }
}
