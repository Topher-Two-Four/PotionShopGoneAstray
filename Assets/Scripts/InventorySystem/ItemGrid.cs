using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemGrid : MonoBehaviour
{
    [HideInInspector] public const float tileSizeWidth = 32;
    [HideInInspector] public const float tileSizeHeight = 32;

    public int GetGridSizeWidth()
    {
        return gridSizeWidth;
    }

    public int GetGridSizeHeight()
    {
        return gridSizeHeight;
    }

    public InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;

    [SerializeField] int gridSizeWidth = 10;
    [SerializeField] int gridSizeHeight = 20;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }


    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

    public InventoryItem PickUpItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }

        CleanGridReference(toReturn);

        return toReturn;
    }
    public InventoryItem RemoveItem(int x, int y)
    {

        InventoryItem itemToRemove = inventoryItemSlot[x, y];

        if (itemToRemove == null) { return null; }

        Destroy(itemToRemove.gameObject);

        inventoryItemSlot[x, y] = null;

        return itemToRemove;
    }

    public void ClearGrid(ItemGrid inventoryGrid)
    {
        for (int x = 0; x < gridSizeWidth; x++)
        {
            for (int y = 0; y < gridSizeHeight; y++)
            {
                InventoryItem inventoryItem = inventoryItemSlot[x, y];
                if (inventoryItem != null && inventoryItem.GetInventoryItemData())
                {
                    inventoryItem.Delete();
                }
            }
        }
    }

    public void RemoveRandomItem(ItemGrid inventoryGrid)
    {
    int x = Random.Range(0, gridSizeWidth);
    int y = Random.Range(0, gridSizeHeight);
    InventoryItem inventoryItem = inventoryItemSlot[x, y];
    Debug.Log("Selecting " + inventoryItem + "using item grid script...");

    if (inventoryItem != null && inventoryItem.GetInventoryItemData())
         {
         inventoryItem.Delete();
         Debug.Log("Removing " + inventoryItem + "using item grid script...");
         }
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int iy = 0; iy < item.HEIGHT; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x, y];
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.HEIGHT + 1;
        int width = gridSizeWidth - itemToInsert.WIDTH + 1;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (CheckAvailableSpace(x, y, itemToInsert.WIDTH, itemToInsert.HEIGHT) == true)
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        //Debug.Log("No space left for item.");
        return null;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundaryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            CleanGridReference(overlapItem);
        }

        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;
        Vector2 position = CalculatePositionOnGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);
        return position;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if (overlapItem != inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    }                 
                }
            }
        }

        return true;
    }

    public List<PotionData> FindPotionsInInventory()
    {
        List<PotionData> potionsInInventory = new List<PotionData>();
    
        for (int x = 0; x < gridSizeWidth; x++)
        {
            for (int y = 0; y < gridSizeHeight; y++)
            {
                InventoryItem inventoryItem = inventoryItemSlot[x, y];
                if (inventoryItem != null && inventoryItem.GetInventoryItemData() is PotionData potionData)
                {
                    if (!potionsInInventory.Contains(potionData))
                    {
                        potionsInInventory.Add(potionData);
                    }
                }
            }
        }

        return potionsInInventory;
    }

    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
 
                    return false;

                }
            }
        }

        return true;
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    bool PositionCheck(int posX, int posY)
    {
        if (posX < 0 || posY < 0)
        {
            return false;
        }

        if (posX >= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

        public bool BoundaryCheck(int posX, int posY, int width, int height)
        {
            if (PositionCheck(posX, posY) == false) { return false; }

            posX += width - 1;
            posY += height - 1;

            if(PositionCheck(posX, posY) == false) { return false; }

            return true;
        }

    public void RemovePotionFromInventory(PotionData potionData)
    {
        for (int x = 0; x < gridSizeWidth; x++)
        {
            for (int y = 0; y < gridSizeHeight; y++)
            {
                InventoryItem item = inventoryItemSlot[x, y];
                if (item != null && item.GetInventoryItemData() == potionData)
                {
                    RemoveItem(x, y);
                    return;
                }
            }
        }
    }

    public void RemoveItemFromInventory(ItemData itemData)
    {
        for (int x = 0; x < gridSizeWidth; x++)
        {
            for (int y = 0; y < gridSizeHeight; y++)
            {
                InventoryItem item = inventoryItemSlot[x, y];
                if (item != null && item.GetInventoryItemData() == itemData)
                {
                    RemoveItem(x, y);
                    return;
                }
            }
        }
    }

}
