using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCraftingSystem : MonoBehaviour
{
    public bool isFailed = false;
    public int currentTemp = 3;
    public float cookTime = 120f;
    public float timeCooked = 0f;
    public int desiredTemp = 3;
    public float timeAtDesiredTemp = 0f;
    public int potionQuality = 4;

    public ItemData ingredient1;
    public ItemData ingredient2;
    public ItemData ingredient3;

    public float ultraQualityTimeLimit = 90f;
    public float highQualityTimeLimit = 70f;
    public float mediumQualityTimeLimit = 50f;
    public float lowQualityTimeLimit = 30f;

    private void Start()
    {
        Po
    }



    public int CheckPotionQuality(int cookTime, float timeAtDesiredTemp)
    {
        float qualityPercentage = (timeAtDesiredTemp / cookTime);
        if (qualityPercentage >= ultraQualityTimeLimit)
        {
            isFailed = false;
            return 4;
        }
        else if (qualityPercentage >= highQualityTimeLimit)
        {
            isFailed = false;
            return 3;
        }
        else if (qualityPercentage >= mediumQualityTimeLimit)
        {
            isFailed = false;
            return 2;
        }
        else if (qualityPercentage >= lowQualityTimeLimit)
        {
            isFailed = false;
            return 1;
        }
        else
        {
            isFailed = true;
            return 0;
        }
    }

    public void BrewPotion(ItemData ingredient1, ItemData ingredient2, ItemData ingredient3)
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3);
        if (potionRecipe != null)
        {
            // Make potion and highlight button is colored
            // If button pressed:
            ItemData potionBrewed = new ItemData();
            //potionBrewed = RecipeList.Instance.AddPotionToInventory(potionRecipe);
            
        }
        else
        {
            // Do not allow making of potion and keep brew button grayed out
        }

    }
}
