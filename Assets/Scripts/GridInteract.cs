using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemGrid))]

public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    InventoryController inventoryController; // Inventory controller being interacted with
    ItemGrid itemGrid; // Item grid of the current inventory item

    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController; // Get inventory controller
        itemGrid = GetComponent<ItemGrid>(); // Get item grid object
    }

    public void OnPointerEnter(PointerEventData eventData) // Run when pointer enters grid space
    {
        inventoryController.SelectedItemGrid = itemGrid; // Set selected item grid to the current one
    }

    public void OnPointerExit(PointerEventData eventData) // Run when pointer exits grid space
    {
        inventoryController.SelectedItemGrid = null; // Set selected item grid to null
    }


}
