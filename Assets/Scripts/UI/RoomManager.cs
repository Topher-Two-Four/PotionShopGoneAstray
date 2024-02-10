using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [Header("List of Rooms:")]
    [Tooltip("The list of room game objects in the potion shop.")]
    [SerializeField] private List<GameObject> roomList = new List<GameObject>(); // List for holding room game objects

    [Header("List of Buttons Corresponding to Rooms:")]
    [Tooltip("The list of navigational buttons which correspond to rooms in the potion shop.")]
    [SerializeField] private List<Button> buttonList = new List<Button>(); // List for holding buttons, index corresponding to each room

    [HideInInspector] public GameObject currentRoom; // The current room that is active/open
    [HideInInspector] public bool isCauldronRoom; 

    //private bool isAnyRoomOpen; // Keep track of whether a room canvas is open or not

    private void Awake()
    {
        AudioManager.Instance.PlayMusic("ShopMusic");

        // Assign a listener to each button in the button list
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(() => ToggleRoom(button));
        }

    }

    private void OnDestroy()
    {
        if (GameManager.Instance.GetDoorToMaze() != null)
        {
            GameManager.Instance.ToggleOffDoorToMaze();
        }
    }

    // Toggles on and off room game objects for navigation of 2D scene
    private void ToggleRoom(Button button)
    {
        AudioManager.Instance.PlaySFX("NavigatePotionShop");

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

            if (currentRoom == roomList[1] || currentRoom == roomList[4]) // Cauldron room
            {
                GameManager.Instance.ToggleOffDoorToMaze();
                GameManager.Instance.ToggleOnPotionCraftingCanvas();
                GameManager.Instance.ToggleOffOrderDisplay();
                GameManager.Instance.ToggleOnBookshelfCanvas();
            } 
            else if (currentRoom == roomList[3] || currentRoom == roomList[6]) // Door to maze room
            {
                GameManager.Instance.ToggleOffPotionCraftingCanvas();
                GameManager.Instance.ToggleOnDoorToMaze();
                GameManager.Instance.ToggleOffOrderDisplay();
                GameManager.Instance.ToggleOffBookshelfCanvas();
            }
            else if (currentRoom == roomList[2] || currentRoom == roomList[7]) // Order room
            {
                GameManager.Instance.ToggleOffPotionCraftingCanvas();
                GameManager.Instance.ToggleOffDoorToMaze();
                GameManager.Instance.ToggleOnOrderDisplay();
                GameManager.Instance.ToggleOffBookshelfCanvas();
            }
            else // Front register room
            {
                GameManager.Instance.ToggleOffDoorToMaze();
                GameManager.Instance.ToggleOffPotionCraftingCanvas();
                GameManager.Instance.ToggleOffOrderDisplay();
                GameManager.Instance.ToggleOffBookshelfCanvas();

            }
        }
    }


}
