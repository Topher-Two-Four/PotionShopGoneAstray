using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [Header("List of Rooms:")]
    [Tooltip("The list of room game objects in the potion shop.")]
    [SerializeField] private GameObject cauldronRoom;
    [SerializeField] private GameObject orderRoom;
    [SerializeField] private GameObject mazeDoorRoom;
    [SerializeField] private GameObject registerRoom;

    [Header("List of Buttons Corresponding to Rooms:")]
    [Tooltip("The list of navigational buttons which correspond to rooms in the potion shop.")]
    [SerializeField] private Button leftNavigationButton;
    [SerializeField] private Button rightNavigationButton;

    [HideInInspector] public GameObject currentRoom; // The current room that is active/open
    [HideInInspector] public bool isCauldronRoom;

    [HideInInspector] public bool cauldronRoomOpen;
    [HideInInspector] public bool orderRoomOpen;
    [HideInInspector] public bool mazeDoorRoomOpen;
    [HideInInspector] public bool registerRoomOpen;

    private void Awake()
    {
        AudioManager.Instance.sfxSource.Stop();
        AudioManager.Instance.sfx2Source.Stop();
        AudioManager.Instance.musicSource.pitch = 1.0f;
        AudioManager.Instance.PlayMusic("ShopMusic");

        cauldronRoomOpen = false;
        orderRoomOpen = false;
        mazeDoorRoomOpen = false;
        registerRoomOpen = true;

        currentRoom = registerRoom;

        leftNavigationButton.onClick.AddListener(() => LeftArrowKeyPress());
        rightNavigationButton.onClick.AddListener(() => RightArrowKeyPress());
        GameManager.Instance.ToggleOffLoadingScreenCanvas();
        GameManager.Instance.ToggleCursorOn();
        TutorialManager.Instance.ToggleOnLookingAroundShopTutorial();

        InventorySerializer.Instance.SerializeData(InventoryController.Instance.GetInventoryGrid());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            LeftArrowKeyPress();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RightArrowKeyPress();
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.GetDoorToMaze() != null)
        {
            GameManager.Instance.ToggleOffDoorToMaze();
        }
    }

    private void SwitchToCauldronRoom()
    {
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = cauldronRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleCursorOn();
        GameManager.Instance.ToggleOffDoorToMaze();
        GameManager.Instance.ToggleOnPotionCraftingCanvas();
        GameManager.Instance.ToggleOffOrderDisplay();
        GameManager.Instance.ToggleOnBookshelfCanvas();

        TutorialManager.Instance.ToggleOnBrewingStepsIntroTutorial();
        TutorialManager.Instance.ToggleOnBrewingSteps1Tutorial();
        TutorialManager.Instance.ToggleOnBrewingSteps2Tutorial();

        cauldronRoomOpen = true;
        orderRoomOpen = false;
        mazeDoorRoomOpen = false;
        registerRoomOpen = false;
    }

    private void SwitchToOrderRoom()
    {
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = orderRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleCursorOn();
        GameManager.Instance.ToggleOffPotionCraftingCanvas();
        GameManager.Instance.ToggleOffDoorToMaze();
        GameManager.Instance.ToggleOnOrderDisplay();
        GameManager.Instance.ToggleOffBookshelfCanvas();

        TutorialManager.Instance.ToggleOnDeliveryTutorial();
        TutorialManager.Instance.ToggleOnSellPotionsTutorial();

        cauldronRoomOpen = false;
        orderRoomOpen = true;
        mazeDoorRoomOpen = false;
        registerRoomOpen = false;
    }
    private void SwitchToMazeDoorRoom()
    {
        GameManager.Instance.ToggleCursorOn();
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = mazeDoorRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleOffPotionCraftingCanvas();
        GameManager.Instance.ToggleOnDoorToMaze();
        GameManager.Instance.ToggleOffOrderDisplay();
        GameManager.Instance.ToggleOffBookshelfCanvas();

        TutorialManager.Instance.ToggleOnMazeAccessTutorial();

        cauldronRoomOpen = false;
        orderRoomOpen = false;
        mazeDoorRoomOpen = true;
        registerRoomOpen = false;
    }
    private void SwitchToRegisterRoom()
    {
        GameManager.Instance.ToggleCursorOn();
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = registerRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleOffDoorToMaze();
        GameManager.Instance.ToggleOffPotionCraftingCanvas();
        GameManager.Instance.ToggleOffOrderDisplay();
        GameManager.Instance.ToggleOffBookshelfCanvas();

        cauldronRoomOpen = false;
        orderRoomOpen = false;
        mazeDoorRoomOpen = false;
        registerRoomOpen = true;
    }

    private void LeftArrowKeyPress()
    {
        AudioManager.Instance.PlaySFX("NavigatePotionShop");

        if (cauldronRoomOpen)
        {
            SwitchToRegisterRoom();
        }
        else if (orderRoomOpen)
        {
            SwitchToCauldronRoom();
        }
        else if (mazeDoorRoomOpen)
        {
            SwitchToOrderRoom();
        }
        else // Register room open
        {
            SwitchToMazeDoorRoom();
        }
    }

    private void RightArrowKeyPress()
    {
        AudioManager.Instance.PlaySFX("NavigatePotionShop");

        if (cauldronRoomOpen)
        {
            SwitchToOrderRoom();
        }
        else if (orderRoomOpen)
        {
            SwitchToMazeDoorRoom();
        }
        else if (mazeDoorRoomOpen)
        {
            SwitchToRegisterRoom();
        }
        else // Register room open
        {
            SwitchToCauldronRoom();
        }
    }

}
