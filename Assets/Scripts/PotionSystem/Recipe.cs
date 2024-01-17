using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    [Header("Potion:")]
    [Tooltip("The potion that is produced using this recipe.")]
    public PotionData potion; // Potion that will be made from this recipe

    [Header("Ingredients (Between 1-4):")]
    [Tooltip("The first ingredient for the potion recipe.")]
    public ItemData ingredient1; // First ingredient in this recipe
    [Tooltip("The second ingredient for the potion recipe.")]
    public ItemData ingredient2; // Second ingredient in this recipe
    [Tooltip("The third ingredient for the potion recipe.")]
    public ItemData ingredient3; // Third ingredient in this recipe
    [Tooltip("The fourth ingredient for the potion recipe.")]
    public ItemData ingredient4; // Fourth ingredient in this recipe

    [Header("Brewing Settings:")]
    [Tooltip("The desired temperature for brewing this recipe.")]
    public int desiredTemp = 3; // The desired temperature for this potion between 1 and 5
    [Tooltip("Whether or not the potion recipe requires stirring halfway through.")]
    public bool needStirring = false;
    [Tooltip("Whether or not the potion recipe requires the lid to be on.")]
    public bool needsLidOn = false;

    [Header("Brew Quality Time Ranges:")]
    [Tooltip("The time needed to make an ultra quality potion from this recipe.")]
    [HideInInspector] public float ultraQualityTimeLimit = 90f; // Time in desired temperature range required to make an ultra quality potion
    [Tooltip("The time needed to make a high quality potion from this recipe.")]
    [HideInInspector] public float highQualityTimeLimit = 70f;// Time in desired temperature range required to make a high quality potion
    [Tooltip("The time needed to make a medium quality potion from this recipe.")]
    [HideInInspector] public float mediumQualityTimeLimit = 50f; // Time in desired temperature range required to make a medium quality potion
    [Tooltip("The time needed to make a low quality potion from this recipe.")]
    [HideInInspector] public float lowQualityTimeLimit = 30f; // Time in desired temperature range required to make a low quality potion
    
    [HideInInspector] public int quality = 5; // The quality of the potion made, with 1 being lowest and 5 highest
    [HideInInspector] public float cookTime = 120f; // How long the recipe takes to cook

}

