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

    public Recipe FindRecipe(ItemData item1, ItemData item2, ItemData item3, ItemData item4)
    {
        List<ItemData> playerIngredients = new List<ItemData> {item1, item2, item3, item4 }.FindAll(item => item != null); // Create list from player ingredients that aren't null

        foreach (Recipe recipe in recipeList) // Iterate through recipes in the list of recipes
        {
            List<ItemData> recipeIngredients = new List<ItemData> { recipe.ingredient1, recipe.ingredient2, recipe.ingredient3, recipe.ingredient4 }.FindAll(item => item != null); // Create list from recipe ingredients that aren't null

            if (AreIngredientsIdentical(playerIngredients, recipeIngredients)) // Compare ingredients between the player and recipe ingredient lists using method that compares them
            {
                return recipe; // Return recipe if a recipe with the current ingredients is found
            }
        }
        return null; // Return null if a recipe with the current ingredients isn't found
    }

    public bool AreIngredientsIdentical(List<ItemData> playerIngredients, List<ItemData> recipeIngredients)
    {
        if (playerIngredients.Count != recipeIngredients.Count) // Check if the number of ingredients between the player and recipe ingredients lists differ
        {
            return false; // Return false if the two lists have differing numbers of ingredients
        }

        foreach (ItemData ingredient in playerIngredients) // Iterate through player ingredients list
        {
            if (!recipeIngredients.Contains(ingredient)) // Check if the recipe list contains an ingredient that the player list has
            {
                return false; // Return false if an ingredient does not have a match in both lists
            }
        }
        return true; // Return true if the two lists have the same number of ingredients and if they have equal matching ingredients in each
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
