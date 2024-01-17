using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [Header("Item Info:")]
    [Tooltip("Whether or not the item is a potion ingredient.")]
    public bool isIngredient; // Whether this item is an ingredient
    [Tooltip("Whether or not the item is sellable.")]
    public bool isSellable;

    [Header("2D Item Width and Height:")]
    [Tooltip("The inventory width of the item.")]
    public int width = 1; // Width of inventory item
    [Tooltip("The inventory height of the item.")]
    public int height = 1; // Height of inventory item

    [Header("2D Item Icon:")]
    [Tooltip("The 2D icon for the item.")]
    public Sprite itemIcon; // The UI display icon for this item

    [Header("3D Item Object:")]
    [Tooltip("The planar 3D object that displays the item icon.")]
    public ItemObject itemObject;

}
