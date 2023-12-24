using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{

    public List<GameObject> roomList = new List<GameObject>(); // List for holding room game objects
    public List<Button> buttonList = new List<Button>(); // List for holding buttons, index corresponding to each room
    public GameObject currentRoom; // The current room that is active/open
    public bool isCauldronRoom; 

    //private bool isAnyRoomOpen; // Keep track of whether a room canvas is open or not

    private void Awake()
    {
        // Assign a listener to each button in the button list
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(() => ToggleRoom(button));
        }

        GameManager.Instance.ToggleOnDoorToMaze();

    }

    private void OnDestroy()
    {
        if (GameManager.Instance.doorToMaze != null)
        {
            GameManager.Instance.ToggleOffDoorToMaze();
        }
    }

    // Toggles on and off room game objects for navigation of 2D scene
    private void ToggleRoom(Button button)
    {
        // Keep track of the index number for which button was pressed
        int buttonIndex = buttonList.IndexOf(button);

        // Check whether index is valid
        if (buttonIndex >= 0 && buttonIndex < roomList.Count)
        {
            // Set current room game object to inactive
            currentRoom.SetActive(false);
            // Switch current room to the new room
            currentRoom = roomList[buttonIndex];
            // Set new current room game object to active
            currentRoom.SetActive(true);
            // Keep track that a room game object is open and being displayed
            //isAnyRoomOpen = true;

            if (currentRoom == roomList[1] || currentRoom == roomList[4])
            {
                GameManager.Instance.ToggleOffDoorToMaze();
                GameManager.Instance.ToggleOnPotionCraftingCanvas();
            } 
            else if (currentRoom == roomList[3] || currentRoom == roomList[6])
            {
                GameManager.Instance.ToggleOffPotionCraftingCanvas(); // Later on will have toggle batch of game objects and logic based on potion shop room
                GameManager.Instance.ToggleOnDoorToMaze();
            }
            else
            {
                GameManager.Instance.ToggleOffDoorToMaze();
                GameManager.Instance.ToggleOffPotionCraftingCanvas();
            }
        }

    }


}
