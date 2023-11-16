using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public bool isBrewing = false;

    public Button ingredient1Button;
    public Button ingredient2Button;
    public Button ingredient3Button;
    public Button potionRetrievalButton;
    public Button increaseTempButton;
    public Button decreaseTempButton;
    public Button brewButton;

    public Sprite ingredient1Image;
    public Sprite ingredient2Image;
    public Sprite ingredient3Image;
    public Sprite potionImage;

    public TMP_Text temperatureDisplayText;
    public TMP_Text timeRemainingText;


    private void Awake()
    {
            ingredient1Button.onClick.AddListener(() => AddIngredientToInventory());
            ingredient2Button.onClick.AddListener(() => AddIngredientToInventory());
            ingredient3Button.onClick.AddListener(() => AddIngredientToInventory());
            //increaseTempButton.onClick.AddListener(() => AddIngredientToInventory());
            //decreaseTempButton.onClick.AddListener(() => AddIngredientToInventory());
            brewButton.onClick.AddListener(() => BrewPotion(ingredient1, ingredient2, ingredient3)); // Add button listener to brew potion when pressed
    }

    private void Update()
    {
        UpdateBrewButtonStatus(); // NEED TO MAKE DO THIS ONLY WHEN CHANGE OCCURS
    }

    public void AddIngredientToSlot(ItemData ingredient)
    {

    }
    public void AddIngredientToInventory()
    {

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
        isBrewing = true;
        timeCooked = 0;
        timeAtDesiredTemp = 0;
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3);
        desiredTemp = potionRecipe.desiredTemp;
        cookTime = potionRecipe.cookTime;
        if (timeCooked > cookTime)
        {
            // Display image of potion that is being made
            timeRemainingText.GetComponent<TMPro.TextMeshProUGUI>().text = ((cookTime - timeCooked).ToString());
            if (desiredTemp == currentTemp)
            {
                timeAtDesiredTemp++;
            }
            brewButton.interactable = false;
        } 
        else
        {
            brewButton.interactable = true;
            timeRemainingText.GetComponent<TMPro.TextMeshProUGUI>().text = ("Done!");
            // Make potion retrievable
        }
    }

    public void UpdateBrewButtonStatus()
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3);
        if (potionRecipe != null)
        {
            brewButton.interactable = true;
        }
        else
        {
            // Do not allow making of potion and keep brew button grayed out
            brewButton.interactable = false;
        }
    }
    public void ResetPotionRetrievalArea()
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3);
        if (isBrewing)
        {
            potionImage = potionRecipe.potionIcon;
        }
        else
        {
            potionImage = null;
        }
    }

}
