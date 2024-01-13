using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [HideInInspector] public int quality;

    private ItemData itemData;
    private Image potionBackground;

    public int HEIGHT
    {
        get
        {
            if (rotated == false)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }

    public int WIDTH
    {
        get
        {
            if (rotated == false)
            {
                return itemData.width;
            }
            return itemData.height;
        }
    }

    public int onGridPositionX;
    public int onGridPositionY;

    public bool rotated = false;

    public void SetQuality(int qualityLevel, Color qualityColor)
    {
        quality = qualityLevel;
        potionBackground = this.gameObject.transform.GetChild(0).GetComponent<Image>();
        //Debug.Log(qualityColor);
        potionBackground.color = qualityColor;

        // Assign image to potion inventory item
    }

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.width * ItemGrid.tileSizeWidth;
        size.y = itemData.height * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    internal void Rotate()
    {
        rotated = !rotated;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0, 0, rotated == true ? 90f : 0f);

    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void Drop()
    {
        if (GameManager.Instance.GetPlayerCapsule().activeSelf)
        {
            Vector3 spawnLocation = GameManager.Instance.GetDropSpawnLocation().transform.position;
            Instantiate(itemData.itemObject, spawnLocation, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public ItemData GetInventoryItemData()
    {
        return itemData;
    }

}
