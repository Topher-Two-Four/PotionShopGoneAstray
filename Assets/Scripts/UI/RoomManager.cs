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
    [SerializeField] private GameObject bedRoom;

    [Header("List of Buttons Corresponding to Rooms:")]
    [Tooltip("The list of navigational buttons which correspond to rooms in the potion shop.")]
    [SerializeField] private Button leftNavigationButton;
    [SerializeField] private Button rightNavigationButton;
    [SerializeField] private Button enterBedroomButton;
    [SerializeField] private Button leaveBedroomButton;

    [HideInInspector] public GameObject currentRoom; // The current room that is active/open
    [HideInInspector] public bool isCauldronRoom;

    [HideInInspector] public bool cauldronRoomOpen;
    [HideInInspector] public bool orderRoomOpen;
    [HideInInspector] public bool mazeDoorRoomOpen;
    [HideInInspector] public bool registerRoomOpen;
    [HideInInspector] public bool bedRoomOpen;

    public static RoomManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); } else { Instance = this; } // Singleton logic

        AudioManager.Instance.sfx1Source.Stop();
        AudioManager.Instance.sfx2Source.Stop();
        AudioManager.Instance.musicSource.pitch = 1.0f;
        AudioManager.Instance.PlayPotionShopMusic();

        cauldronRoomOpen = false;
        orderRoomOpen = false;
        mazeDoorRoomOpen = false;
        registerRoomOpen = true;
        bedRoomOpen = false;

        currentRoom = registerRoom;

        InventoryController.Instance.ToggleOffBedroomStorageCanvas();

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
            if (!bedRoomOpen)
            {
                LeftArrowKeyPress();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (!bedRoomOpen)
            {
                RightArrowKeyPress();
            }
        }
    }

    public void SwitchToCauldronRoom()
    {
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = cauldronRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleCursorOn();
        GameManager.Instance.ToggleOnPotionCraftingCanvas();
        GameManager.Instance.ToggleOffOrderDisplay();
        GameManager.Instance.ToggleOnBookshelfCanvas();
        InventoryController.Instance.ToggleOffBedroomStorageCanvas();

        TutorialManager.Instance.ToggleOnBrewingStepsIntroTutorial();
        TutorialManager.Instance.ToggleOnBrewingSteps1Tutorial();
        TutorialManager.Instance.ToggleOnBrewingSteps2Tutorial();

        cauldronRoomOpen = true;
        orderRoomOpen = false;
        mazeDoorRoomOpen = false;
        registerRoomOpen = false;
        bedRoomOpen = false;

    }

    public void SwitchToOrderRoom()
    {
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = orderRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleCursorOn();
        GameManager.Instance.ToggleOffPotionCraftingCanvas();
        GameManager.Instance.ToggleOnOrderDisplay();
        GameManager.Instance.ToggleOffBookshelfCanvas();
        InventoryController.Instance.ToggleOffBedroomStorageCanvas();

        TutorialManager.Instance.ToggleOnDeliveryTutorial();
        TutorialManager.Instance.ToggleOnSellPotionsTutorial();

        cauldronRoomOpen = false;
        orderRoomOpen = true;
        mazeDoorRoomOpen = false;
        registerRoomOpen = false;
        bedRoomOpen = false;

    }
    public void SwitchToMazeDoorRoom()
    {
        GameManager.Instance.ToggleCursorOn();
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = mazeDoorRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleOffPotionCraftingCanvas();
        GameManager.Instance.ToggleOffOrderDisplay();
        GameManager.Instance.ToggleOffBookshelfCanvas();
        InventoryController.Instance.ToggleOffBedroomStorageCanvas();

        TutorialManager.Instance.ToggleOnMazeAccessTutorial();

        cauldronRoomOpen = false;
        orderRoomOpen = false;
        mazeDoorRoomOpen = true;
        registerRoomOpen = false;
        bedRoomOpen = false;

    }
    public void SwitchToRegisterRoom()
    {
        GameManager.Instance.ToggleCursorOn();
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = registerRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active

        GameManager.Instance.ToggleOffPotionCraftingCanvas();
        GameManager.Instance.ToggleOffOrderDisplay();
        GameManager.Instance.ToggleOffBookshelfCanvas();
        InventoryController.Instance.ToggleOffBedroomStorageCanvas();

        cauldronRoomOpen = false;
        orderRoomOpen = false;
        mazeDoorRoomOpen = false;
        registerRoomOpen = true;
        bedRoomOpen = false;
    }

    public void SwitchToBedRoom()
    {
        currentRoom.SetActive(false); // Set current room game object to inactive
        currentRoom = bedRoom; // Switch current room to the new room
        currentRoom.SetActive(true); // Set new current room game object to active
        GameManager.Instance.ToggleCursorOn();
        GameManager.Instance.ToggleOffPotionCraftingCanvas();
        GameManager.Instance.ToggleOffOrderDisplay();
        GameManager.Instance.ToggleOffBookshelfCanvas();

        InventoryController.Instance.ToggleInventoryCanvasOn();
        InventoryController.Instance.ToggleOnBedroomStorageCanvas();

        cauldronRoomOpen = false;
        orderRoomOpen = false;
        mazeDoorRoomOpen = false;
        registerRoomOpen = false;
        bedRoomOpen = true;
    }

    public void EnterMaze()
    {
        GameManager.Instance.SwitchSceneToMazeLevel();
    }

    private void LeftArrowKeyPress()
    {
        AudioManager.Instance.PlayNavigatePotionShopSound();

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
        AudioManager.Instance.PlayNavigatePotionShopSound();

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

    public bool IsBedRoomOpen()
    {
        return bedRoomOpen;
    }
}
