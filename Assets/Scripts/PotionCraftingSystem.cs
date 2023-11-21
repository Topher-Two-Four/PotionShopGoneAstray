using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public ItemData potionBeingBrewed; // Third ingredient ItemData scriptable object

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

    public Color freezingTempDisplayColor = new Color(0, 117, 191);
    public Color lowTempDisplayColor = new Color(0, 0, 255);
    public Color mediumTempDisplayColor = new Color(0, 255, 0);
    public Color hotTempDisplayColor = new Color(108, 255, 0);
    public Color boilingTempDisplayColor = new Color(255, 0, 0);

    public Image temperatureDisplayImage; // Background image for the temperature display
    public Image ingredient1Image; // Image for the first ingredient
    public Image ingredient2Image; // Image for the second ingredient
    public Image ingredient3Image; // Image for the third ingredient
    public Image potionImage; // Image for the potion created or being created
    public Sprite emptySlotImage; // Image for an empty slot

    public TMP_Text temperatureDisplayText; // TMP text game object for displaying the current temperature
    public TMP_Text timeRemainingText; // TMP text game object for displaying amount of cook time remaining

    public Recipe potionRecipe;

    public static PotionCraftingSystem Instance { get; private set; } // Singleton logic

    // *** MONOBEHAVIOR ***

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

        ingredient1Button.onClick.AddListener(() => RetrieveItem(ingredient1, 1)); // Add button listener for first ingredient space
        ingredient2Button.onClick.AddListener(() => RetrieveItem(ingredient2, 2)); // Add button listener for second ingredient space
        ingredient3Button.onClick.AddListener(() => RetrieveItem(ingredient3, 3)); // Add button listener for third ingredient space
        
        increaseTempButton.onClick.AddListener(() => IncreaseTemperature()); // Add button listener for increase temperature button
        decreaseTempButton.onClick.AddListener(() => DecreaseTemperature());// Add button listener for decrease temperature button
        
        brewButton.onClick.AddListener(() => BrewPotion(ingredient1, ingredient2, ingredient3)); // Add button listener to brew potion when pressed
        
        potionRetrievalButton.onClick.AddListener(() => RetrievePotion());// Add button listener for potion retrieval area
        
        UpdateTemperatureDisplay();
    }

    private void Update()
    {
        UpdateIngredientIcons();
        UpdatePotionIcon();
        UpdateBrewButtonStatus();
    }


    // *** POTION BREWING ***

    public void BrewPotion(ItemData ingredient1, ItemData ingredient2, ItemData ingredient3) // Brew potion using three ingredients
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3);

        if (potionRecipe != null)
        {
            isBrewing = true;
            BrewPotionWIthRecipe(potionRecipe);
            potionImage.sprite = potionRecipe.potionIcon;
            UpdateBrewingTimerDisplay(potionRecipe.cookTime);
            Debug.Log(potionRecipe);
        }
    }

    private void BrewPotionWIthRecipe(Recipe potionRecipe)
    {
        StartCoroutine(BrewingProcess(potionRecipe));
    }

    private IEnumerator BrewingProcess(Recipe potionRecipe)
    {
        timeCooked = 0f;
        timeAtDesiredTemp = 0f;
        potionBeingBrewed = potionRecipe.potion;
        ingredient1 = null;
        ingredient2 = null;
        ingredient3 = null;
        Debug.Log("Brewing process started");
        while (timeCooked < potionRecipe.cookTime)
        {

            if (currentTemp == desiredTemp)
            {
                float timeRemaining = Mathf.Max(0, cookTime - timeCooked);
                UpdateBrewingTimerDisplay(timeRemaining);
                timeCooked += Time.deltaTime;
                timeAtDesiredTemp += Time.deltaTime;
                Debug.Log("At desired temp");
            } 
            else
            {
                float timeRemaining = Mathf.Max(0, cookTime - timeCooked);
                UpdateBrewingTimerDisplay(timeRemaining);
                timeCooked += Time.deltaTime;
                Debug.Log("Not at desired temp");
            }
            yield return null;
        }
        DisplayBrewingComplete();
    }

    public void UpdateBrewButtonStatus()
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3); // Get potion recipe from instance of the recipe list
        if (potionRecipe != null) // Check whether a potion recipe exists with the combination of these three ingredients
        {
            if (isBrewing) // If recipe is not currently brewing
            {
                brewButton.interactable = false; // If a recipe with this ingredient combination does not exist then make the brew button uninteractable (and grayed out)
            }
            else
            {
                if (!isRetrievable) // If recipe is not retrievable
                {
                    brewButton.interactable = false;
                    potionImage.sprite = potionRecipe.potionIcon;
                    timeRemainingText.text = ("Done!");
                }
                else
                {
                    brewButton.interactable = true;
                    potionImage.sprite = potionRecipe.potionIcon;
                    timeRemainingText.text = cookTime.ToString();
                }
                brewButton.interactable = true; // If a recipe with this ingredient combination does exist then make the brew button interactable
                potionImage.sprite = potionRecipe.potionIcon;
                timeRemainingText.text = cookTime.ToString();
            }

        }
    }

    private void UpdateBrewingTimerDisplay(float timeRemaining)
    {
        timeRemainingText.text = (((int)timeRemaining).ToString());
    }

    private void DisplayBrewingComplete()
    {
        Debug.Log("Brewing complete.");
        timeRemainingText.text = ("Done!");
        isBrewing = false;
        isRetrievable = true;
        potionRetrievalButton.interactable = true;
    }

    private void UpdateIngredientIcons()
    {
        if (ingredient1 != null)
        {
            ingredient1Image.sprite = ingredient1.itemIcon;
        }
        else
        {
            ingredient1Image.sprite = emptySlotImage;
        }

        if (ingredient2 != null)
        {
            ingredient2Image.sprite = ingredient2.itemIcon;
        }
        else
        {
            ingredient2Image.sprite = emptySlotImage;
        }

        if (ingredient3 != null)
        {
            ingredient3Image.sprite = ingredient3.itemIcon;
        }
        else
        {
            ingredient3Image.sprite = emptySlotImage;
        }
    }

    private void UpdatePotionIcon()
    {
        if (potionBeingBrewed != null)
        {
            potionImage.sprite = potionBeingBrewed.itemIcon;
        }
        else
        {
            potionImage.sprite = emptySlotImage;
        }
    }


    public void AddIngredientToSlot(ItemData ingredient) // Add an ingredient from the inventory into the crafting slot
    {
        Debug.Log("Adding " + ingredient);
        if (ingredient.isIngredient == true) // Check whether the item is an ingredient
        {
            if (ingredient1 == null)
            {
                ingredient1 = ingredient;
            }
            else if (ingredient2 == null)
            {
                ingredient2 = ingredient;
            }
            else if (ingredient3 == null)
            {
                ingredient3 = ingredient;
            }
            else
            {
                return;
            }
        }
        else
        {
            Debug.Log("No available space.");
            return; // Return if no space available amongst the three ingredient spaces
        }
    }

    public void AddItemToInventory(ItemData itemData)
    {
        Debug.Log(itemData);
        if (itemData != null)
        {
            InventoryController.Instance.InsertItem(itemData);
            itemData = null;
            if (itemData == potionBeingBrewed)
            {
                potionBeingBrewed = null;
            }
            if (itemData == ingredient1)
            {
                itemData = null;
            }
            else if (itemData == ingredient2)
            {
                itemData = null;
            }
            else if (itemData == ingredient3)
            {
                itemData = null;
            }
            else
            {
                return;
            }
        } 

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

    public void SetPotionRetrievalArea() // Set the image for the potion retrieval area
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3); // Get potio
        Debug.Log("Setting retrieval area.");

        if (isBrewing) // Check whether brewing is occurring
        {
            potionImage.sprite = potionRecipe.potionIcon; // Display potion image if it's being brewed
            // Add grayish tranpsparency over top of image while unable to be retrieved
        }
        else if (isRetrievable) // Check whether the potion is retrievable
        {
            potionImage.sprite = potionRecipe.potionIcon; // Display potion image if it's ready to be retrieved
        }
        else // If brewing isn't occurring and a potion isn't ready to be retrieved then assume potion image doesn't need to be there
        {
            potionImage = null; // Set potion image to null to remove potion image from screen display
        }
    }

    // *** CAULDRON CONTROLS ***

    private void IncreaseTemperature()
    {
        if (currentTemp < 5) 
        {
            currentTemp++;
            UpdateTemperatureDisplay();
        } else
        {
            currentTemp = 5;
            UpdateTemperatureDisplay();
        }
    }

    private void DecreaseTemperature()
    {
        if (currentTemp > 1)
        {
            currentTemp--;
            UpdateTemperatureDisplay();
        }
        else
        {
            currentTemp = 1;
            UpdateTemperatureDisplay();
        }
    }

    private void UpdateTemperatureDisplay()
    {
        switch (currentTemp)
        {
            case 5:
                temperatureDisplayText.text = ("Boiling");
                temperatureDisplayImage.color = boilingTempDisplayColor;
                break;
            case 4:
                temperatureDisplayText.text = ("Hot");
                temperatureDisplayImage.color = hotTempDisplayColor;
                break;
            case 3:
                temperatureDisplayText.text = ("Medium");
                temperatureDisplayImage.color = mediumTempDisplayColor;
                break;
            case 2:
                temperatureDisplayText.text = ("Low");
                temperatureDisplayImage.color = lowTempDisplayColor;
                break;
            case 1:
                temperatureDisplayText.text = ("Freezing");
                temperatureDisplayImage.color = freezingTempDisplayColor;
                break;
            default:
                temperatureDisplayText.text = ("Off");
                temperatureDisplayImage.color = new Color(255, 255, 255);
                break;
        }           
    }

    // *** ITEM RETRIEVAL ***

    private void RetrievePotion()
    {
        Debug.Log(potionBeingBrewed);
        Debug.Log("Trying to retrieve potion");
        if (!isBrewing)
        {
            if (isRetrievable)
            {
                Debug.Log("Retrieving potion");
                AddItemToInventory(potionBeingBrewed);
                potionBeingBrewed = null;
            }
            else
            {
                Debug.Log("Potion not retrievable");
            }
        }
        else
        {
            Debug.Log("Brewing not complete");
        }
    }

    private void RetrieveItem(ItemData ingredient, int ingredientSlot)
    {
        AddItemToInventory(ingredient);
        if (ingredientSlot == 1)
        {
            ingredient1 = null;
        }
        else if (ingredientSlot == 2)
        {
            ingredient2 = null;
        }
        else
        {
            ingredient3 = null;
        }

    }

}