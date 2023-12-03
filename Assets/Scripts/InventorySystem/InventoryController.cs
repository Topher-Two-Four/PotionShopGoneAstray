using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public ItemGrid inventoryGrid; // Inventory grid used by the player

    private bool isSpaceForItem;

    [HideInInspector]
    private ItemGrid selectedItemGrid; // Item grid that is currently selected

    public ItemGrid SelectedItemGrid {  
        get => selectedItemGrid; // Get currently selected item grid
        set // Set as currently selected item grid
        {
            selectedItemGrid = value; // Store item grid as variable
            inventoryHighlight.SetParent(value); // Add highlight to inventory item
        }
    }

    InventoryItem selectedItem; // Item that is currently selected
    InventoryItem overlapItem; // Item that is overlapping the currently selected item
    RectTransform rectTransform; // Rectangular transform used for highlighter

    [SerializeField] List<ItemData> items; // List of ItemData scriptable objects
    [SerializeField] GameObject itemPrefab; // Prefab for the item object
    [SerializeField] GameObject potionPrefab; // Prefab for the potion object
    [SerializeField] Transform canvasTransform; // Transform of the inventory canvas

    InventoryHighlight inventoryHighlight; // Inventory item highlighter


    public static InventoryController Instance { get; private set; } // Singleton logic

    private void Start()
    {
        isSpaceForItem = true; // Set initially true when beginning the game
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this);} else { Instance = this;} // Singleton logic

        inventoryHighlight = GetComponent<InventoryHighlight>(); // Get highlight componenet
    }

    private void Update()
    {
        ItemIconDrag(); // Drag item icon using script

        if (Input.GetKeyDown(KeyCode.R)) { RotateItem(); } // Rotate selected inventory item

        if (selectedItemGrid == null) { inventoryHighlight.Show(false); return; } // Don't show highlight if selected item grid doesn't exist and return from method

        HandleHighlight(); // Use script to handle highlight of inventory item

        if (Input.GetKeyDown(KeyCode.Tab)) { ToggleInventoryCanvas(); } // When left mouse button is pressed run method

        if (Input.GetMouseButtonDown(0)) { LeftMouseButtonPress(); } // When left mouse button is pressed run method

        if (Input.GetMouseButton(1)) { RightMouseButtonPress(); } // When right mouse button is pressed run method

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

    public void CreateInventoryItem(PotionData potionData, int qualityLevel)
    {
        InventoryItem inventoryItem = Instantiate(potionPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        inventoryItem.SetQuality(qualityLevel);

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
            Debug.Log("Space for object is null.");
            isSpaceForItem = false;
            return;
        }

        inventoryGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    public void InsertItem(ItemData itemData)
    {
        if (inventoryGrid == null || !isSpaceForItem)
            {
            Debug.Log(isSpaceForItem);
            return;
            } // Return if inventory grid doesn't exist or if there is no space for item in inventory

            // Need to return if FindSpaceForObject(itemToInsert) is false
            
            CreateInventoryItem(itemData); // Create new inventory item from ItemData object
            
            InventoryItem itemToInsert = selectedItem; // Declare inventory item to insert to be currently selected item
            
            InsertItem(itemToInsert); // Insert inventory item into next open spot in inventory
            
            selectedItem = null; // Reset selected item to null to clear selection
    }

    public void InsertPotion(PotionData potionData, int qualityLevel)
    {
        if (inventoryGrid == null || !isSpaceForItem)
        {
            Debug.Log(isSpaceForItem);
            return;
        } // Return if inventory grid doesn't exist or if there is no space for item in inventory

        // Need to return if FindSpaceForObject(itemToInsert) is false

        potionData.quality = qualityLevel;

        CreateInventoryItem(potionData, qualityLevel); // Create new inventory item from ItemData object

        InventoryItem itemToInsert = selectedItem; // Declare inventory item to insert to be currently selected item

        InsertItem(itemToInsert); // Insert inventory item into next open spot in inventory

        selectedItem = null; // Reset selected item to null to clear selection
    }


    public void AddItemObjectToInventory(ItemData itemData)
    {
        Debug.Log(itemData);
        if (itemData != null && isSpaceForItem)
        {
            //if (inventoryCanvas.)
            InsertItem(itemData);
            itemData = null;
        }
    }

    public void ToggleInventoryCanvas()
    {
        if (inventoryCanvas.gameObject.activeSelf)
        {
            inventoryCanvas.gameObject.SetActive(false);
        } 
        else
        {
            inventoryCanvas.gameObject.SetActive(true);
        }
    }

    public void SellPotion(PotionData potionData)
    {
        potionData.sellPrice = (potionData.quality * potionData.baseValue);
        GameManager.Instance.playerCurrency += potionData.sellPrice;
    }

    public void AddIngredientToPotionCraftingSpace(Vector2Int tileGridPosition)
    {
        InventoryItem selectedItem = selectedItemGrid.GetItem(tileGridPosition.x, tileGridPosition.y);

        if  (PotionCraftingSystem.Instance.isBrewing || PotionCraftingSystem.Instance.isRetrievable) { return; }

        if (selectedItem != null && selectedItem.itemData.isIngredient && isSpaceForItem)
        {
            selectedItemGrid.RemoveItem(tileGridPosition.x, tileGridPosition.y);

            PotionCraftingSystem.Instance.AddIngredientToSlot(selectedItem.itemData);
            Destroy(selectedItem.gameObject);
        }
        else if (selectedItem != null && selectedItem.GetType() == typeof(PotionData))
        {
            selectedItemGrid.RemoveItem(tileGridPosition.x, tileGridPosition.y);

            //SellPotion(selectedItem.potionData);
            Destroy(selectedItem.gameObject);
        }
        
        else
        {
            return;
        }
    }

    Vector2Int oldPosition;
    InventoryItem itemToHighlight;

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

}
