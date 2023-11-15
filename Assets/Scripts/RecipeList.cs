using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeList : MonoBehaviour
{
    public Recipe[] recipeList;

    private ItemData ingredient1;
    private ItemData ingredient2;
    private ItemData ingredient3;

    public static RecipeList Instance { get; private set; } // Singleton logic
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public Recipe FindRecipe(ItemData item1, ItemData item2, ItemData item3)
    {
        foreach (Recipe recipe in recipeList)
        {
            if (recipe.ingredient1 == item1 || recipe.ingredient2 == item1 || recipe.ingredient3 == item1)
            {
                ingredient1 = item1;
                if (recipe.ingredient1 == item2 || recipe.ingredient2 == item2 || recipe.ingredient3 == item2)
                {
                    ingredient2 = item2;
                    if (recipe.ingredient3 == null)
                    {
                        return recipe;                       
                    }
                    if (recipe.ingredient3 == item3 || recipe.ingredient2 == item3 || recipe.ingredient3 == item3 && item3 != null)
                    {
                        ingredient3 = item3;
                        return recipe;
                    }
                }
                else
                {
                    return null;
                }
            } 
        }
            return null;
    }

    internal Recipe AddPotionToInventory(Recipe potionRecipe)
    {
        foreach (Recipe recipe in recipeList)
        {
            if (recipe == potionRecipe && potionRecipe.potionName != null)
            {
                Debug.Log("Created 1x " + (potionRecipe.potionName));
                return recipe;
            }
        }
        return null;
    }
}
