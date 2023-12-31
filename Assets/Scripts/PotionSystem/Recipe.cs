using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public PotionData potion; // Potion that will be made from this recipe

    public ItemData ingredient1; // First ingredient in this recipe
    public ItemData ingredient2; // Second ingredient in this recipe
    public ItemData ingredient3; // Third ingredient in this recipe
    public ItemData ingredient4; // Fourth ingredient in this recipe

    public float cookTime = 120f; // How long the recipe takes to cook
    public int desiredTemp = 3; // The desired temperature for this potion between 1 and 5
    public bool needStirring = false;
    public bool needsLidOn = false;

    public int quality = 5; // The desired temperature for this potion between 1 and 5

    public float ultraQualityTimeLimit = 90f; // Time in desired temperature range required to make an ultra quality potion
    public float highQualityTimeLimit = 70f;// Time in desired temperature range required to make a high quality potion
    public float mediumQualityTimeLimit = 50f; // Time in desired temperature range required to make a medium quality potion
    public float lowQualityTimeLimit = 30f; // Time in desired temperature range required to make a low quality potion
}

