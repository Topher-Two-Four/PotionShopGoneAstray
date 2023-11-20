using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int width = 1; // Width of inventory item
    public int height = 1; // Height of inventory item

    public bool isIngredient; // Whether this item is an ingredient
    public bool isPotion; // Whether this item is a potion
    public int quality; // The quality of the item from 1-5
    public int baseValue; // The base currency value of this item

    public Sprite itemIcon; // The UI display icon for this item


}
