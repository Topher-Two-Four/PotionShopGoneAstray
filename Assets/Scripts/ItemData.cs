using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int width = 1;
    public int height = 1;

    public InventoryItem itemToInsertToInventory;

    public bool isIngredient;


    public Sprite itemIcon;
    public MeshRenderer mesh;

    // INSERT REMAINDER OF ITEM OBJECT DATA HERE

}
