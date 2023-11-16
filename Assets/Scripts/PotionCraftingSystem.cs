using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PotionCraftingSystem : MonoBehaviour
{
    public bool isFailed = false; // Variable to track whether potion crafting process has failed
    public int currentTemp = 3; // Current temperature of the cauldron
    public float cookTime = 120f; // Total cook time of the potion
    public float timeCooked = 0f; // Time that the potion has been cooking
    public int desiredTemp = 3; // Desired temperature for the potion being made
    public float timeAtDesiredTemp = 0f; // Time that potion has been cooking at the desired temperature
    public int potionQuality = 4; // Quality of the potion crafted

    public ItemData ingredient1; // First ingredient ItemData scriptable object
    public ItemData ingredient2; // Second ingredient ItemData scriptable object
    public ItemData ingredient3; // Third ingredient ItemData scriptable object

    public float ultraQualityTimeLimit = 90f; // Time in desired temperature range required to make an ultra quality potion
    public float highQualityTimeLimit = 70f;// Time in desired temperature range required to make a high quality potion
    public float mediumQualityTimeLimit = 50f; // Time in desired temperature range required to make a medium quality potion
    public float lowQualityTimeLimit = 30f; // Time in desired temperature range required to make a low quality potion

    public bool isBrewing = false; // Variable to track whether a potion is currently being brewed
    public bool isRetrievable = false; // Variable to track whether a potion is ready to be retrieved

    public Button ingredient1Button; // Button for the first ingredient
    public Button ingredient2Button; // Button for the second ingredient
    public Button ingredient3Button; // Button for the third ingredient
    public Button potionRetrievalButton; // Button for potion retrieval
    public Button increaseTempButton; // Button to increase cooking temperature
    public Button decreaseTempButton; // Button to increase cooking temperature
    public Button brewButton; // Button to begin brewing

    public Sprite ingredient1Image; // Image for the first ingredient
    public Sprite ingredient2Image; // Image for the second ingredient
    public Sprite ingredient3Image; // Image for the third ingredient
    public Sprite potionImage; // Image for the potion created or being created

    public TMP_Text temperatureDisplayText; // TMP text game object for displaying the current temperature
    public TMP_Text timeRemainingText; // TMP text game object for displaying amount of cook time remaining


    private void Awake()
    {
            ingredient1Button.onClick.AddListener(() => AddIngredientToInventory()); // Add button listener for first ingredient space
            ingredient2Button.onClick.AddListener(() => AddIngredientToInventory()); // Add button listener for second ingredient space
            ingredient3Button.onClick.AddListener(() => AddIngredientToInventory()); // Add button listener for third ingredient space
            //increaseTempButton.onClick.AddListener(() => AddIngredientToInventory()); // Add button listener for increase temperature button
            //decreaseTempButton.onClick.AddListener(() => AddIngredientToInventory());// Add button listener for decrease temperature button
            brewButton.onClick.AddListener(() => BrewPotion(ingredient1, ingredient2, ingredient3)); // Add button listener to brew potion when pressed
    }

    private void Update()
    {
        UpdateBrewButtonStatus(); // NEED TO MAKE DO THIS ONLY WHEN CHANGE OCCURS
    }

    public void BrewPotion(ItemData ingredient1, ItemData ingredient2, ItemData ingredient3) // Brew potion using three ingredients
    {
        isBrewing = true; // Declare that the potion is brewing
        timeCooked = 0; // Reset cooking time
        timeAtDesiredTemp = 0; // Reset time at desired temperature
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3); // Get potion recipe from recipe list instance
        // Remove ingredient 1
        // Remove ingredient 2
        // Remove ingredient 3
        desiredTemp = potionRecipe.desiredTemp; // Set desired temperature to recipe desired temperature
        cookTime = potionRecipe.cookTime; // Set cook time to recipe cook time
        if (timeCooked > cookTime) // Check whether there is still time remaining for potion to brew
        {
            potionImage = potionRecipe.potionIcon; // Display image of potion that is being made
            timeRemainingText.GetComponent<TMPro.TextMeshProUGUI>().text = ((cookTime - timeCooked).ToString()); // Display time remaining until potion brewed
            if (currentTemp == desiredTemp) // Check whether the current temperature is set to the desired temperature
            {
                timeAtDesiredTemp++; // Increment time at desired temperature
            }
            brewButton.interactable = false; // Make the brew button non-interactable
        }
        else // If no time is left on the brewing clock
        {
            brewButton.interactable = true; // Make the brew button interactable
            timeRemainingText.GetComponent<TMPro.TextMeshProUGUI>().text = ("Done!"); // Display that the potion has finished brewing on time display
            // Make potion retrievable
            isBrewing = false; // Declare that brewing is no longer occurring
        }
    }


    public void AddIngredientToSlot(ItemData ingredient) // Add an ingredient from the inventory into the crafting slot
    {
        if (ingredient.isIngredient == true) // Check whether the item is an ingredient
        {
            // If ingredient 1 space full, then next, if ingredient 2 space full, then next,
            // If ingredient 3 space full, then do not add item to crafting area
        }
        else
        {
            return; // Return if no space available amongst the three ingredient spaces
        }

    }
    public void AddIngredientToInventory()
    {
        // If button clicked has an ingredient then add it to inventory
        // Reset button image
        // Else return
    }

    public int CheckPotionQuality(int cookTime, float timeAtDesiredTemp) // Check what quality of potion has been made, based on time in desired temperature range
    {
        float qualityPercentage = (timeAtDesiredTemp / cookTime); // Calculate variable to represent the quality of potion made, based on time in desired temperature range

        if (qualityPercentage >= ultraQualityTimeLimit) // Check whether quality percentage is within ultra quality time range
        {
            isFailed = false; // Declare that the potion crafting process has succeeded
            return 4; // Return variable to represent an ultra quality potion (4)
        }
        else if (qualityPercentage >= highQualityTimeLimit) // Check whether quality percentage is within high quality time range
        {
            isFailed = false; // Declare that the potion crafting process has succeeded
            return 3; // Return variable to represent an ultra quality potion (3)
        }
        else if (qualityPercentage >= mediumQualityTimeLimit) // Check whether quality percentage is within medium quality time range
        {
            isFailed = false; // Declare that the potion crafting process has succeeded
            return 2; // Return variable to represent an ultra quality potion (2)
        }
        else if (qualityPercentage >= lowQualityTimeLimit) // Check whether quality percentage is within low quality time range
        {
            isFailed = false; // Declare that the potion crafting process has succeeded
            return 1; // Return variable to represent an ultra quality potion (1)
        }
        else // Potion making has failed if lower that the low quality time limit
        {
            isFailed = true; // Declare that the potion crafting process has failed
            return 0; // // Return variable to represent that a potion was not crafted, or if it was it is inert (0)
        }
    }

   
    public void UpdateBrewButtonStatus()
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3); // Get potion recipe from instance of the recipe list

        if (potionRecipe != null) // Check whether a potion recipe exists with the combination of these three ingredients
        {
            brewButton.interactable = true; // If a recipe with this ingredient combination does exist then make the brew button interactable
        }
        else
        {
            brewButton.interactable = false; // If a recipe with this ingredient combination does not exist then make the brew button uninteractable (and grayed out)
        }
    }
    public void SetPotionRetrievalArea() // Set the image for the potion retrieval area
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3); // Get potio

        if (isBrewing) // Check whether brewing is occurring
        {
            potionImage = potionRecipe.potionIcon; // Display potion image if it's being brewed
            // Add grayish tranpsparency over top of image while unable to be retrieved
        }
        else if (isRetrievable) // Check whether the potion is retrievable
        {
            potionImage = potionRecipe.potionIcon; // Display potion image if it's ready to be retrieved
        }
        else // If brewing isn't occurring and a potion isn't ready to be retrieved then assume potion image doesn't need to be there
        {
            potionImage = null; // Set potion image to null to remove potion image from screen display
        }
    }

}
