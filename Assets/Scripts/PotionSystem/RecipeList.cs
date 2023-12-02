using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeList : MonoBehaviour
{
    public Recipe[] recipeList; // List of recipe scriptable objects, which contains the recipes available for crafting in the game

    private ItemData ingredient1; // First ingredient ItemData object
    private ItemData ingredient2; // Second ingredient ItemData object
    private ItemData ingredient3; // Third ingredient ItemData object
    private ItemData ingredient4; // Fourth ingredient ItemData object

    public static RecipeList Instance { get; private set; } // Make recipe list into a singleton
    private void Awake()
    {
        if (Instance != null && Instance != this) // Check if an instance exists that is not this instance
        {
            Destroy(this); // If an instance already exists then destroy this instance
        }
        else
        {
            Instance = this; // If an instance doesn't already exist then set this as the single instance
        }
    }

    public Recipe FindRecipe(ItemData item1, ItemData item2, ItemData item3, ItemData item4) // Search if a recipe exists containing a combination of three ingredients
    {
        foreach (Recipe recipe in recipeList) // Iterate through recipes in the list of recipes
        {


            if (item1 == null && item2 == null && item3 == null && item4 == null) { return null; }



            if (recipe.ingredient1 == item1 || recipe.ingredient2 == item1 || recipe.ingredient3 == item1 || recipe.ingredient4 == item1) // Check whether the first ItemData object is in any recipes
            {
                ingredient1 = item1; // If the first ItemData object is in a recipe then set it as the first ingredient
                
                if (recipe.ingredient2 == null && recipe.ingredient3 == null && recipe.ingredient4 == null)
                {
                    Debug.Log(recipe);
                    return recipe;
                }

                if (recipe.ingredient1 == item2 || recipe.ingredient2 == item2 || recipe.ingredient3 == item2 || recipe.ingredient4 == item2) // Check whether the second ItemData object is in any recipes
                {
                    ingredient2 = item2; // If the second ItemData object is in a recipe then set it as the second ingredient
                    
                    if (recipe.ingredient3 == null && recipe.ingredient4 == null) // Check whether a third ingredient exists
                    {
                        Debug.Log(recipe);
                        return recipe; // Return two-ingredient recipe if there is not a third our fourth ingredient
                    }

                    if (recipe.ingredient3 == item3 || recipe.ingredient2 == item3 || recipe.ingredient3 == item3 || recipe.ingredient4 == item3 && item3 != null && item4 != null)
                    {
                        ingredient3 = item3; // If the third ItemData object is in a recipe then set it as the third ingredient

                        if (recipe.ingredient4 == null)
                        {
                            Debug.Log(recipe);
                            return recipe; // Return three-ingredient recipe if there is not a fourth ingredient
                        }

                        if (recipe.ingredient1 == item4 || recipe.ingredient2 == item4 || recipe.ingredient3 == item4 || recipe.ingredient4 == item4 && item4 != null)
                        {
                            ingredient4 = item4;
                            Debug.Log(recipe);
                            return recipe; // Return four-ingredient recipe
                        }
                    }
                }
            }
        }

        return null;
    }

    internal Recipe AddPotionToInventory(Recipe potionRecipe) // Add crafted potion to player inventory
    {
        foreach (Recipe recipe in recipeList) // Iterate through recipe list
        {
            if (recipe == potionRecipe && potionRecipe.potion != null) // Check whether a recipe exists
            {
                Debug.Log("Created 1x "); // Display console indication that potion has been created and added to player inventory
                return recipe; // Return recipe of potion
            }
        }
        return null; // If no recipe is found then return null
    }
}
