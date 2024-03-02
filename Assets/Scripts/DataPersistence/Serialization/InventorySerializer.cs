using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySerializer : MonoBehaviour
{

    PotionData potionData;

    public void SerializeData(ItemGrid inventoryGrid)
    {
        for (int x = 0; x < inventoryGrid.GetGridSizeWidth(); x++)
        {
            for (int y = 0; y < inventoryGrid.GetGridSizeHeight(); y++)
            {
                InventoryItem inventoryItem = inventoryGrid.inventoryItemSlot[x, y];
                //Debug.Log("Selecting " + inventoryItem + "using item grid script...");
                if (inventoryItem != null && inventoryItem.GetInventoryItemData())
                {
                    // Serialize data
                }
            }
            
        }

        if (potionData.isAntidote)
        {
            // Add a 1
        }
        else if (potionData.isBenefit)
        {
            // Add a 2
        }
        else if (potionData.isBenefit)
        {
            // Add a 3
        }
        else if (potionData.isBenefit)
        {
            // Add a 4
        }
        else if (potionData.isBenefit)
        {
            // Add a 5
        }
        else if (potionData.isBenefit)
        {
            // Add a 6
        }
        else if (potionData.isBenefit)
        {
            // Add a 7
        } else
        {
            // Add an 8
        }
    }
}
