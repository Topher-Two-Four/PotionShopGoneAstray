using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [Header("Item Info:")]
    public bool isIngredient; // Whether this item is an ingredient
    public bool isSellable;

    [Header("2D Item Width and Height:")]
    public int width = 1; // Width of inventory item
    public int height = 1; // Height of inventory item

    [Header("2D Item Icon:")]
    public Sprite itemIcon; // The UI display icon for this item

    [Header("3D Item Object:")]
    public ItemObject itemObject;

}
