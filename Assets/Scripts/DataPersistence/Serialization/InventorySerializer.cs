using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySerializer : MonoBehaviour
{
    private string inventoryItemKey;

    public static InventorySerializer Instance { get; private set; } // Singleton logic

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public string SerializeData(ItemGrid inventoryGrid)
    {
        inventoryItemKey = "";
        HashSet<InventoryItem> processedItems = new HashSet<InventoryItem>();


        for (int x = 0; x < inventoryGrid.GetGridSizeWidth(); x++)
        {
            for (int y = 0; y < inventoryGrid.GetGridSizeHeight(); y++)
            {
                InventoryItem inventoryItem = inventoryGrid.GetItem(x, y);
                if (inventoryItem != null && !processedItems.Contains(inventoryItem))
                {
                    processedItems.Add(inventoryItem);

                    if (inventoryItem.GetInventoryItemData() != null)
                    {
                        int itemId = inventoryItem.GetInventoryItemData().ingredientID;
                        int quantity = 1;

                        inventoryItemKey += SerializeIngredientData(itemId, quantity);
                    }
                }
            }
        }

        List<PotionData> potionsInInventory = inventoryGrid.FindPotionsInInventory();
        
        foreach (PotionData potion in potionsInInventory)
        {
            inventoryItemKey += SerializePotionData(potion);
            Debug.Log(potion + " serialized.");
        }

        Debug.Log("Serialized Inventory Key: " + inventoryItemKey);

        return inventoryItemKey;
    }

    public void DeserializeData(string loadedInventoryKey)
    {
        int numberOfItems = loadedInventoryKey.Length / 4;

        while (numberOfItems > 0)
        {
            if (loadedInventoryKey[0] == 0)
            {
                char ingredientIDPart1 = loadedInventoryKey[2];
                char ingredientIDPart2 = loadedInventoryKey[3];

                string ingredientID = (ingredientIDPart1 + ingredientIDPart2).ToString();

                // Get ingredient by ID

                // Add item to inventory
            } 
            else
            {
                char potionQuality = loadedInventoryKey[2];
                char potionIDPart1 = loadedInventoryKey[2];
                char potionIDPart2 = loadedInventoryKey[3];

                string potionID = (potionIDPart1 + potionIDPart2).ToString();

                // Create potion type of this type and add to inventory
                // PotionCraftingSystem.Instance.AddPotionToInventory(potionData, potionQuality);
            }
        }
    }

    private string SerializeIngredientData(int itemId, int quantity)
    {

        return itemId.ToString("D4");
    }

    private string SerializePotionData(PotionData potionData)
    {
        string potionKey = "1";
        potionKey += potionData.ingredientID.ToString("D2");
        potionKey += potionData.quality.ToString("D1");

        return potionKey;
    }
}
