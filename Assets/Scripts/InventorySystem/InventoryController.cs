using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [Header("Inventory Settings:")]
    [Tooltip("The prefab for items.")]
    [SerializeField] private GameObject itemPrefab; // Prefab for the item object
    [Tooltip("The prefab for potions.")]
    [SerializeField] private GameObject potionPrefab; // Prefab for the potion object
    [Tooltip("The transform of the inventory canvas.")]
    [SerializeField] private Transform canvasTransform; // Transform of the inventory canvas
    [Tooltip("The inventory canvas game object.")]
    [SerializeField] private GameObject inventoryCanvas;
    [Tooltip("The inventory grid used by the player.")]
    [SerializeField] private ItemGrid inventoryGrid; // Inventory grid used by the player

    [SerializeField] private GameObject satchelCanvas;
    [SerializeField] private GameObject leftBedroomStorageCanvas;
    [SerializeField] private GameObject rightBedroomStorageCanvas;
    [SerializeField] private GameObject wallBedroomStorageCanvas;
    [SerializeField] private GameObject dresserBedroomStorageCanvas;

    [Header("Button Settings:")]
    [Tooltip("The button that trashes an item when pressed.")]
    [SerializeField] private Button trashButton;

    public ItemGrid SelectedItemGrid {
        get => selectedItemGrid; // Get currently selected item grid
        set // Set as currently selected item grid
        {
            selectedItemGrid = value; // Store item grid as variable
            inventoryHighlight.SetParent(value); // Add highlight to inventory item
        }
    }

    [HideInInspector] public List<ItemData> items; // List of ItemData scriptable objects in the player's inventory

    private ItemGrid selectedItemGrid; // Item grid that is currently selected
    private InventoryHighlight inventoryHighlight; // Inventory item highlighter
    private InventoryItem selectedItem; // Item that is currently selected
    private InventoryItem overlapItem; // Item that is overlapping the currently selected item
    private RectTransform rectTransform; // Rectangular transform used for highlighter
    private bool isSpaceForItem;

    public static InventoryController Instance { get; private set; } // Singleton logic

    private void Start()
    {
        isSpaceForItem = true; // Set initially true when beginning the game
        ToggleOffBedroomStorageCanvas();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this);} else { Instance = this;} // Singleton logic

        inventoryHighlight = GetComponent<InventoryHighlight>(); // Get highlight component

        trashButton.onClick.AddListener(() => DeleteItem());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !GameManager.Instance.IsBedroomOpen()) { ToggleInventoryCanvas(); } // When left mouse button is pressed run method

        if (inventoryCanvas.activeSelf) // Only do the following code if the inventory canvas is open
        {
            ItemIconDrag(); // Drag item icon using script

            if (Input.GetKeyDown(KeyCode.R)) { RotateItem(); } // Rotate selected inventory item

            if (Input.GetKeyDown(KeyCode.Delete)) { DeleteItem(); } // Rotate selected inventory item

            if (Input.GetKeyDown(KeyCode.G)) { DropItem(); } // Rotate selected inventory item

            if (selectedItemGrid == null) { inventoryHighlight.Show(false); return; } // Don't show highlight if selected item grid doesn't exist and return from method

            HandleHighlight(); // Use script to handle highlight of inventory item

            if (Input.GetMouseButtonDown(0)) { LeftMouseButtonPress(); } // When left mouse button is pressed run method

            if (Input.GetMouseButton(1)) { RightMouseButtonPress(); } // When right mouse button is pressed run method

        }

    }

    public void CreateInventoryItem(ItemData itemData)
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        inventoryItem.Set(itemData);
    }

    public void CreateInventoryItem(PotionData potionData, int qualityLevel, Color qualityColor)
    {
        InventoryItem inventoryItem = Instantiate(potionPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        inventoryItem.SetQuality(qualityLevel, qualityColor);

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        inventoryItem.Set(potionData);
    }

    public void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = inventoryGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null)
        {
            //Debug.Log("Space for object is null.");
            isSpaceForItem = false;
            return;
        }

        inventoryGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    // Figure out how InventoryItem relates to ItemData

    public bool CheckForItemSpace(ItemData itemData)
    {
        InventoryItem inventoryItem = Instantiate(potionPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        inventoryItem.Set(itemData);

        Vector2Int? posOnGrid = inventoryGrid.FindSpaceForObject(inventoryItem);

        if (posOnGrid == null)
        {
            //Debug.Log("Space for object is null.");
            isSpaceForItem = false;
            selectedItem = null;
            return false;
        }
        //Debug.Log("Room for object exists.");
        isSpaceForItem = true;
        selectedItem = null;
        return true;
    }

    public void InsertItem(ItemData itemData)
    {
        if (inventoryGrid == null || !isSpaceForItem)
            {
            //Debug.Log(isSpaceForItem);
            return;
            } // Return if inventory grid doesn't exist or if there is no space for item in inventory

            // Need to return if FindSpaceForObject(itemToInsert) is false
            
            CreateInventoryItem(itemData); // Create new inventory item from ItemData object
            
            InventoryItem itemToInsert = selectedItem; // Declare inventory item to insert to be currently selected item

            Vector2Int? posOnGrid = inventoryGrid.FindSpaceForObject(itemToInsert);
            
            InsertItem(itemToInsert); // Insert inventory item into next open spot in inventory
            
            selectedItem = null; // Reset selected item to null to clear selection
    }

    public void InsertPotion(PotionData potionData, int qualityLevel, Color qualityColor)
    {
        if (inventoryGrid == null || !isSpaceForItem)
        {
            //Debug.Log(isSpaceForItem);
            return;
        } // Return if inventory grid doesn't exist or if there is no space for item in inventory

        // Need to return if FindSpaceForObject(itemToInsert) is false

        potionData.quality = qualityLevel;

        CreateInventoryItem(potionData, qualityLevel, qualityColor); // Create new inventory item from ItemData object

        InventoryItem itemToInsert = selectedItem; // Declare inventory item to insert to be currently selected item

        InsertItem(itemToInsert); // Insert inventory item into next open spot in inventory

        selectedItem = null; // Reset selected item to null to clear selection
    }

    public bool CheckForItemSelected()
    {
        if (selectedItem == null)
        {
            //Debug.Log("No item selected");
            return false;
        }
        else
        {
            //Debug.Log("Item selected");
            return true;
        }
    }

    public void AddItemObjectToInventory(ItemData itemData)
    {
        //Debug.Log(itemData);
        if (itemData != null && isSpaceForItem && selectedItem == null)
        {
            AudioManager.Instance.PlayPickUpMazeItemSound();
            InsertItem(itemData);
            itemData = null;
        }
    }

    public void ToggleInventoryCanvas()
    {

        if (inventoryCanvas.gameObject.activeSelf) // If inventory canvas is active then deactivate it
        {
            AudioManager.Instance.PlayCloseInventorySound();

            Cursor.visible = false; // Hide cursor
            Cursor.lockState = CursorLockMode.Locked; // Unlock and confine cursor to game screen
            GameManager.Instance.UnfreezeRotation();
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                Cursor.lockState = CursorLockMode.Confined; // Unlock cursor and confine to game screen
                Cursor.visible = true; // Show cursor
            }


            inventoryCanvas.gameObject.SetActive(false); // Deactivate canvas object
        } 
        else // If inventory canvas is inactive then activate it
        {
            AudioManager.Instance.PlayOpenInventorySound();

            Cursor.lockState = CursorLockMode.Confined; // Lock cursor in one place
            Cursor.visible = true; // Hide cursor
            GameManager.Instance.FreezeRotation();
            inventoryCanvas.gameObject.SetActive(true); // Activate canvas object
        }
    }

    public void ToggleInventoryCanvasOn()
    {
        // AudioManager.Instance.PlayOpenInventorySound();

        Cursor.lockState = CursorLockMode.Confined; // Lock cursor in one place
        Cursor.visible = true; // Hide cursor
        //GameManager.Instance.FreezeRotation();
        inventoryCanvas.gameObject.SetActive(true); // Activate canvas object
    }

    public void ToggleInventoryCanvasOff()
    {
        AudioManager.Instance.PlayCloseInventorySound();

        if (inventoryCanvas.gameObject.activeSelf) // If inventory canvas is active then deactivate it
        {
            Cursor.visible = false; // Hide cursor
            Cursor.lockState = CursorLockMode.Locked; // Unlock and confine cursor to game screen
            inventoryCanvas.gameObject.SetActive(false); // Deactivate canvas object

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0) ||
                SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) ||
                SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                Cursor.lockState = CursorLockMode.Confined; // Unlock cursor and confine to game screen
                Cursor.visible = true; // Show cursor
            }
        }
    }


    public PotionData FindPotionOfType(Order potionOrdered)
    {
        List<PotionData> potionsInInvetory = inventoryGrid.FindPotionsInInventory();

        foreach (PotionData potion in potionsInInvetory)
        {
            if ((potion.isAntidote && potionOrdered.isAntidote) ||
                (potion.isBenefit && potionOrdered.isBenefit) ||
                (potion.isCrippling && potionOrdered.isCrippling) ||
                (potion.isDeath && potionOrdered.isDeath) ||
                (potion.isHatred && potionOrdered.isHatred ||
                (potion.isHealth && potionOrdered.isHealth) ||
                (potion.isLove && potionOrdered.isLove) ||
                (potion.isLucky && potionOrdered.isLucky) ||
                (potion.isPoison && potionOrdered.isPoison)))
                {
                return potion;
                }
            }

        return null;
        }

    public void SellPotion(Order order)
    {
        AudioManager.Instance.PlayCompleteOrderSound();
        PotionData potionData = FindPotionOfType(order);
        potionData.sellPrice = (potionData.quality * potionData.baseValue) * potionData.numberOfIngredients;
        GameManager.Instance.AddCurrencyToPlayer(potionData.sellPrice);
        MoralitySystem.Instance.AdjustMoralityCounter(potionData);
        inventoryGrid.RemovePotionFromInventory(potionData);
        order.orderCompletedMask.gameObject.SetActive(true);
        order.turnInPotionButton.gameObject.SetActive(false);
        OrderSystem.Instance.CheckForCompleteOrders();
    }


    public void RemovePotionFromInventory(PotionData potionData)
    {
        inventoryGrid.RemovePotionFromInventory(potionData);
    }

    public void RemoveItemFromInventory(ItemData itemData)
    {
        //Debug.Log("Removing " + itemData);
        inventoryGrid.RemoveItemFromInventory(itemData);
        //Debug.Log("Removed " + itemData);

    }

    public void AddIngredientToPotionCraftingSpace(Vector2Int tileGridPosition)
    {
        InventoryItem selectedItem = selectedItemGrid.GetItem(tileGridPosition.x, tileGridPosition.y);

        if  ((PotionCraftingSystem.Instance.isBrewing || (PotionCraftingSystem.Instance.isRetrievable) ||
             (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(2)))) { return; }
        
        if (selectedItem != null && selectedItem.GetInventoryItemData().isIngredient && isSpaceForItem)
        {
            selectedItemGrid.RemoveItem(tileGridPosition.x, tileGridPosition.y);
            AudioManager.Instance.PlayAddIngredientSound();
            PotionCraftingSystem.Instance.AddIngredientToSlot(selectedItem.GetInventoryItemData());
            Destroy(selectedItem.gameObject);
        }
        else if (selectedItem != null && selectedItem.GetType() == typeof(PotionData))
        {
            selectedItemGrid.RemoveItem(tileGridPosition.x, tileGridPosition.y);

            Destroy(selectedItem.gameObject);
        }
        
        else
        {
            return;
        }
    }

    public void DeleteItem()
    {
        if (selectedItem != null && !selectedItem.GetInventoryItemData().cantBeDiscarded)
        {
            AudioManager.Instance.PlayTrashInventorySound();
            selectedItem.Delete();
        }
        else
        {
            return;
        }
    }

    public void DropItem()
    {
        if (selectedItem != null && 
            selectedItem.GetType() != typeof(PotionData) &&
            !selectedItem.GetInventoryItemData().cantBeDiscarded)
        {
            AudioManager.Instance.PlayDropItemSound();
            selectedItem.Drop();
        }
        else
        {
            return;
        }
    }

    public void ClearInventoryGrid()
    {
        inventoryGrid?.ClearGrid(inventoryGrid); // If inventory grid exists then clear it
    }
    public void RemoveRandomItemFromGrid()
    {
        inventoryGrid?.RemoveRandomItem(inventoryGrid); // If inventory grid exists then remove an item from it
        //Debug.Log("Trying to remove random item from item grid script...");
    }

    Vector2Int oldPosition;
    InventoryItem itemToHighlight;

    public void SetInventoryGrid(ItemGrid savedPlayerInventory)
    {
        inventoryGrid = savedPlayerInventory;
    }

    public ItemGrid GetInventoryGrid()
    {
        return inventoryGrid;
    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if (oldPosition == positionOnGrid) { return; }
        oldPosition = positionOnGrid;
        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);

            if (itemToHighlight != null)
            {
                inventoryHighlight.Show(true);
                inventoryHighlight.SetSize(itemToHighlight);
                inventoryHighlight.SetPosition(selectedItemGrid, itemToHighlight);
            }
            else
            {
                inventoryHighlight.Show(false);
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundaryCheck(
                positionOnGrid.x,
                positionOnGrid.y,
                selectedItem.WIDTH,
                selectedItem.HEIGHT)
                );
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid, selectedItem, positionOnGrid.x, positionOnGrid.y);
        }
    }
    private void RotateItem()
    {
        if (selectedItem == null) { return; }
        AudioManager.Instance.PlayRotateInventoryItemSound();
        selectedItem.Rotate(); // Rotate selected item
    }

    private void LeftMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);

        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }
    private void RightMouseButtonPress()
    {
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem != null)
        {
            InventoryItem itemToRemove = selectedItemGrid.RemoveItem(tileGridPosition.x, tileGridPosition.y);

            if (itemToRemove != null)
            {
                Destroy(itemToRemove.gameObject);
            }
        }
        else
        {
            AddIngredientToPotionCraftingSpace(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;

        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }

        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
                rectTransform.SetAsLastSibling();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem != null)
        {
            AudioManager.Instance.PlayPickUpInventoryItemSound();
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            rectTransform.position = Input.mousePosition;
        }
    }


    public void ToggleOnBedroomStorageCanvas()
    {
        leftBedroomStorageCanvas.SetActive(true);
        rightBedroomStorageCanvas.SetActive(true);
        wallBedroomStorageCanvas.SetActive(true);
        dresserBedroomStorageCanvas.SetActive(true);
    }

    public void ToggleOffBedroomStorageCanvas()
    {
        leftBedroomStorageCanvas.SetActive(false);
        rightBedroomStorageCanvas.SetActive(false);
        wallBedroomStorageCanvas.SetActive(false);
        dresserBedroomStorageCanvas.SetActive(false);
    }

    public bool CheckIfBedroomOpen()
    {
        return RoomManager.Instance.IsBedRoomOpen();
    }

}
