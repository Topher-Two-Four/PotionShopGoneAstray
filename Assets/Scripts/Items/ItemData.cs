using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int width = 1; // Width of inventory item
    public int height = 1; // Height of inventory item

    public bool isIngredient; // Whether this item is an ingredient
    public bool isSellable;

    public Sprite itemIcon; // The UI display icon for this item


}
