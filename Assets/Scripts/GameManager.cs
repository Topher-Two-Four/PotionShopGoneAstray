using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Use scene manager to switch to Potion Level
    public void SwitchSceneToPotionLevel()
    {
        SceneManager.LoadScene(0);

        // Unlock cursor, confine to game screen
        Cursor.lockState = CursorLockMode.Confined;

        // Hide cursor
        Cursor.visible = false;

    }

    // Use scene manager to switch to Maze Level
    public void SwitchSceneToMazeLevel()
    {

        // Ranomize which maze scene is loaded

        // Use scene manager to load first scene out of list, which is the potion shop
        SceneManager.LoadScene(1);

        // Lock cursor in one place
        Cursor.lockState = CursorLockMode.Locked;

        // Hide cursor
        Cursor.visible = true;

    }

}
