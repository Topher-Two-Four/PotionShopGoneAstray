using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public string potionName;
    public ItemData ingredient1 = null;
    public ItemData ingredient2 = null;
    public ItemData ingredient3 = null;

    public float cookTime = 120f;
    public int desiredTemp = 3;

    public Sprite potionIcon;

    // INSERT REMAINDER OF ITEM OBJECT DATA HERE

}

