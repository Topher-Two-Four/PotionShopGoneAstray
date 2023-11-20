using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public ItemGrid inventoryGrid; // Inventory grid used by the player

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
    [SerializeField] Transform canvasTransform; // Transform of the inventory canvas

    InventoryHighlight inventoryHighlight; // Inventory item highlighter



    public static InventoryController Instance { get; private set; } // Singleton logic

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this);} else { Instance = this;} // Singleton logic

        inventoryHighlight = GetComponent<InventoryHighlight>(); // Get highlight componenet
    }

    private void Update()
    {
        ItemIconDrag(); // Drag item icon using script

        if (Input.GetKeyDown(KeyCode.Tab)) { if (selectedItem == null) { CreateRandomItem(); } } // Create random item and have it selected
       
        if (Input.GetKeyDown(KeyCode.Q)) { InsertRandomItem(); } // Create random item and add it to the inventory grid in next available space

        if (Input.GetKeyDown(KeyCode.R)) { RotateItem(); } // Rotate selected inventory item

        if (selectedItemGrid == null)
        {
            inventoryHighlight.Show(false); // Don't show highlight if selected item grid doesn't exist in current context
            return; // Return from method
        }

        HandleHighlight(); // Use script to handle highlight of inventory item

        if (Input.GetMouseButtonDown(0)) // When left mouse button is pressed
        {
            LeftMouseButtonPress(); // Run script for left mouse button press logic

        }

        if (Input.GetMouseButton(1)) // When right mouse button is pressed
        {
            RightMouseButtonPress(); // Run script for right mouse button press logic
        }
    }

    private void RotateItem()
    {
        if (selectedItem == null) { return; } selectedItem.Rotate(); // Rotate selected item
    }

    public void InsertItem(ItemData itemData)
    {
        if (inventoryGrid == null) { return; } // Return if inventory grid doesn't exist

            CreateInventoryItem(itemData); // Create new inventory item from ItemData object
            InventoryItem itemToInsert = selectedItem; // Declare inventory item to insert to be currently selected item
            InsertItem(itemToInsert); // Insert inventory item into next open spot in inventory
            selectedItem = null; // Reset selected item to null to clear selection

    }
    private void InsertRandomItem()
    {
        if (selectedItemGrid == null) { return; }


        CreateRandomItem(); // Creates a selected item and holds it
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    public void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = inventoryGrid.FindSpaceForObject(itemToInsert);

        if (posOnGrid == null) { return; }

        inventoryGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
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

    public void CreateInventoryItem(ItemData itemData)
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;

        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        inventoryItem.Set(itemData);
    }

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        rectTransform.SetAsLastSibling();

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
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
            Debug.Log("Call to add ingredient to inventory space");
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

    private void AddIngredientToPotionCraftingSpace(Vector2Int tileGridPosition)
    {
        InventoryItem selectedItem = selectedItemGrid.GetItem(tileGridPosition.x, tileGridPosition.y);
        Debug.Log(selectedItem);
        if (selectedItem != null && selectedItem.itemData.isIngredient)
        {
            selectedItemGrid.RemoveItem(tileGridPosition.x, tileGridPosition.y);

            PotionCraftingSystem.Instance.AddIngredientToSlot(selectedItem.itemData);
            Destroy(selectedItem.gameObject);
        }
        else if (selectedItem!= null && selectedItem.itemData.isPotion)
        {
            selectedItemGrid.RemoveItem(tileGridPosition.x, tileGridPosition.y);

            SellPotion(selectedItem.itemData);
            Destroy(selectedItem.gameObject);
        } 
        else
        {
            return;
        }
    }

    public void AddItemObjectToInventory(ItemData itemData)
    {
        Debug.Log(itemData);
        if (itemData != null)
        {
            InsertItem(itemData);
            itemData = null;
        }

    }

    public void SellPotion(ItemData itemData)
    {
        int potionValue = (itemData.quality * itemData.baseValue);
        GameManager.Instance.playerCurrency += potionValue;
    }

}
