using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PotionCraftingSystem : MonoBehaviour
{
    [Header("Brew Time Settings:")]
    [Tooltip("The amount of time it takes to brew a potion.")]
    [SerializeField] private float cookTime = 60f; // Total cook time of the potion

    [Header("Dispay Color Settings:")]
    [Tooltip("The display color for the freezing temperature setting.")]
    public Color freezingTempDisplayColor = new Color(0, 117, 191);
    [Tooltip("The display color for the low temperature setting.")]
    public Color lowTempDisplayColor = new Color(0, 0, 255);
    [Tooltip("The display color for the medium temperature setting.")]
    public Color mediumTempDisplayColor = new Color(0, 255, 0);
    [Tooltip("The display color for the hot temperature setting.")]
    public Color hotTempDisplayColor = new Color(108, 255, 0);
    [Tooltip("The display color for the boiling temperature setting.")]
    public Color boilingTempDisplayColor = new Color(255, 0, 0);

    [Header("Quality Settings:")]
    [Tooltip("The percentage of quality needed when brewing to make a potion of ultra quality.")]
    [SerializeField] private float ultraQualityTimePercentage = .9f; // Time in desired temperature range required to make an ultra quality potion
    [Tooltip("The percentage of quality needed when brewing to make a potion of high quality.")]
    [SerializeField] private float highQualityTimePercentage = .7f;// Time in desired temperature range required to make a high quality potion
    [Tooltip("The percentage of quality needed when brewing to make a potion of medium quality.")]
    [SerializeField] private float mediumQualityTimePercentage = .5f; // Time in desired temperature range required to make a medium quality potion
    [Tooltip("The percentage of quality needed when brewing to make a potion of low quality.")]
    [SerializeField] private float lowQualityTimePercentage = .3f; // Time in desired temperature range required to make a low quality potion

    [Header("Potion Quality Color Settings:")]
    [Tooltip("The background color for an ultra quality potion.")]
    [SerializeField] private Color ultraQualityColor = new Color(25, 25, 99, 120); // Purple
    [Tooltip("The background color for a high quality potion.")]
    [SerializeField] private Color highQualityColor = new Color(0, 1, 255, 120); // Blue
    [Tooltip("The background color for a medium quality potion.")]
    [SerializeField] private Color mediumQualityColor = new Color(255, 0, 0, 120); // Red
    [Tooltip("The background color for a low quality potion.")]
    [SerializeField] private Color lowQualityColor = new Color(255, 103, 0, 120); // Orange
    [Tooltip("The background color for a failed/inert quality potion.")]
    [SerializeField] private Color failedColor = new Color(109, 109, 109, 120); // Gray

    [Header("Display Image Settings:")]
    [Tooltip("The background image for the temperature display.")]
    public Image temperatureDisplayImage; // Background image for the temperature display
    [Tooltip("The placeholder image for an empty slot.")]
    public Sprite emptySlotImage; // Image for an empty slot

    [Header("Button Settings:")]
    [Tooltip("The button for ingredient 1.")]
    [SerializeField] private Button ingredient1Button; // Button for the first ingredient
    [Tooltip("The button for ingredient 2.")]
    [SerializeField] private Button ingredient2Button; // Button for the second ingredient
    [Tooltip("The button for ingredient 3.")]
    [SerializeField] private Button ingredient3Button; // Button for the third ingredient
    [Tooltip("The button for ingredient 4.")]
    [SerializeField] private Button ingredient4Button; // Button for the fourth ingredient
    [Tooltip("The button for potion retrieval.")]
    [SerializeField] private Button potionRetrievalButton; // Button for potion retrieval
    [Tooltip("The button for increasing brewing temperature.")]
    [SerializeField] private Button increaseTempButton; // Button to increase cooking temperature
    [Tooltip("The button for decreasing brewing temperature.")]
    [SerializeField] private Button decreaseTempButton; // Button to increase cooking temperature
    [Tooltip("The button to put the lid on the cauldron.")]
    [SerializeField] private Button putOnLidButton;
    [Tooltip("The button to remove the lid from the cauldrom.")]
    [SerializeField] private Button removeLidButton;
    [Tooltip("The button to stir the cauldron.")]
    [SerializeField] private Button stirButton; // Button to stir the caudron
    [Tooltip("The button to begin brewing a potion.")]
    [SerializeField] private Button brewButton; // Button to begin brewing

    [Header("Text Settings:")]
    [Tooltip("The text that displays current brewing temperature.")]
    [SerializeField] private TMP_Text temperatureDisplayText; // TMP text game object for displaying the current temperature
    [Tooltip("The text that displays brew time remaining.")]
    [SerializeField] private TMP_Text timeRemainingText; // TMP text game object for displaying amount of cook time remaining

    [HideInInspector] public bool lidDesired = false;
    [HideInInspector] public Recipe potionRecipe;
    [HideInInspector] public int potionQuality = 4; // Quality of the potion crafted
    [HideInInspector] public bool ingredientSpaceLeft = true;
    [HideInInspector] public bool isBrewing = false; // Variable to track whether a potion is currently being brewed
    [HideInInspector] public bool isRetrievable = false; // Variable to track whether a potion is ready to be retrieved

    private int currentTemp = 3; // Current temperature of the cauldron
    private float timeCooked = 0f; // Time that the potion has been cooking
    private Color currentQualityColor = new Color(255, 255, 255, 1);
    private int desiredTemp = 3; // Desired temperature for the potion being made
    private float timeAtDesiredTemp = 0f; // Time that potion has been cooking at the desired temperature
    private float timeWithLidInDesiredState = 0f;

    public ItemData ingredient1; // First ingredient ItemData scriptable object
    public ItemData ingredient2; // Second ingredient ItemData scriptable object
    public ItemData ingredient3; // Third ingredient ItemData scriptable object
    public ItemData ingredient4; // Third ingredient ItemData scriptable object
    public PotionData potionBeingBrewed; // Third ingredient ItemData scriptable object

    public Image ingredient1Image; // Image for the first ingredient
    public Image ingredient2Image; // Image for the second ingredient
    public Image ingredient3Image; // Image for the third ingredient
    public Image ingredient4Image; // Image for the third ingredient
    public Image potionImage; // Image for the potion created or being created
    public Image potionBackgroundImage; // Image for the background of the potion, which represents its quality

    private bool isStirred = false;
    private bool isLidOn = false;
    private bool shouldBeStirred = false;

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
        ingredient4Button.onClick.AddListener(() => RetrieveItem(ingredient4, 4)); // Add button listener for third ingredient space

        increaseTempButton.onClick.AddListener(() => IncreaseTemperature()); // Add button listener for increase temperature button
        decreaseTempButton.onClick.AddListener(() => DecreaseTemperature());// Add button listener for decrease temperature button
        putOnLidButton.onClick.AddListener(() => ToggleLid());
        removeLidButton.onClick.AddListener(() => ToggleLid());
        stirButton.onClick.AddListener(() => StirCauldron());

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
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3, ingredient4);

        if (potionRecipe != null)
        {
            AudioManager.Instance.PlayPotionStartBrewingSound();
            AudioManager.Instance.PlayCauldronFireSound();
            isBrewing = true;
            BrewPotionWIthRecipe(potionRecipe);
            potionImage.sprite = potionRecipe.potion.itemIcon;
            potionBackgroundImage.color = currentQualityColor;
            UpdateBrewingTimerDisplay(potionRecipe.cookTime);
            //Debug.Log(potionRecipe); //MAKE INTO QUICK DISPLAY TEXT AFTER BEGINNING BREW
        }
        else
        {
            potionImage.sprite = null;
        }
    }

    private void BrewPotionWIthRecipe(Recipe potionRecipe)
    {
        if (potionRecipe == null) { return; }
        StartCoroutine(BrewingProcess(potionRecipe));
    }

    private IEnumerator BrewingProcess(Recipe potionRecipe)
    {
        timeCooked = 0f;
        timeAtDesiredTemp = 0f;
        timeWithLidInDesiredState = 0f;
        isStirred = false;
        potionBeingBrewed = potionRecipe.potion;
        shouldBeStirred = potionRecipe.needStirring;
        desiredTemp = potionRecipe.desiredTemp;
        lidDesired = potionRecipe.needsLidOn;
        bool _halfwayCompleteIndicated = false;
        //Debug.Log("Needs stirring:" + shouldBeStirred);
        ingredient1 = null;
        ingredient2 = null;
        ingredient3 = null;
        ingredient4 = null;
        //Debug.Log("Brewing process started");
        while (timeCooked < potionRecipe.cookTime &&
               GameManager.Instance.CheckIfTimerRunning())
        {
            if (currentTemp == desiredTemp && potionRecipe.needsLidOn == isLidOn)
            {
                float timeRemaining = Mathf.Max(0, cookTime - timeCooked);
                UpdateBrewingTimerDisplay(timeRemaining);
                timeCooked += Time.deltaTime;
                timeAtDesiredTemp += Time.deltaTime;
                timeWithLidInDesiredState += Time.deltaTime;
                //Debug.Log("At desired temp, lid in correct state.");
            }
            else if (currentTemp == desiredTemp && potionRecipe.needsLidOn != isLidOn)
            {
                float timeRemaining = Mathf.Max(0, cookTime - timeCooked);
                UpdateBrewingTimerDisplay(timeRemaining);
                timeCooked += Time.deltaTime;
                timeAtDesiredTemp += Time.deltaTime;
                //Debug.Log("At desired temp, but needs lid corrected.");
            }
            else if (currentTemp != desiredTemp && potionRecipe.needsLidOn == isLidOn)
            {
                float timeRemaining = Mathf.Max(0, cookTime - timeCooked);
                UpdateBrewingTimerDisplay(timeRemaining);
                timeCooked += Time.deltaTime;
                timeWithLidInDesiredState += Time.deltaTime;
                //Debug.Log("Not at desired temp, but lid correct.");
            }
            else
            {
                float timeRemaining = Mathf.Max(0, cookTime - timeCooked);
                UpdateBrewingTimerDisplay(timeRemaining);
                timeCooked += Time.deltaTime;
                //Debug.Log("Not at desired temp and needs lid corrected.");
            }
            
            CheckPotionQuality(potionRecipe.cookTime, timeAtDesiredTemp);
            UpdatePotionQualityIndicator();

            if ((timeCooked > 29.0f && timeCooked < 31.0f) && _halfwayCompleteIndicated)
            {
                AudioManager.Instance.PlayCauldronBubbleSound();
                _halfwayCompleteIndicated = true;
            }

            yield return null;
        }
        CheckPotionQuality(potionRecipe.cookTime, timeAtDesiredTemp);
        AudioManager.Instance.PlayPotionFinishedBrewingSound();
        DisplayBrewingComplete();
    }

    public void UpdateBrewButtonStatus()
    {
        Recipe potionRecipe = RecipeList.Instance.FindRecipe(ingredient1, ingredient2, ingredient3, ingredient4); // Get potion recipe from instance of the recipe list

        if (potionRecipe == null)
        {
            brewButton.interactable = false;
            return;
        }

        potionImage.sprite = potionRecipe.potion.itemIcon;
        timeRemainingText.text = isRetrievable || isBrewing ? "Done!" : cookTime.ToString();
        brewButton.interactable = !isBrewing && (isRetrievable || potionRecipe != null);
    }

    private void UpdateBrewingTimerDisplay(float timeRemaining)
    {
        timeRemainingText.text = (((int)timeRemaining).ToString());
    }

    private void ResetBrewingTimer()
    {
        timeRemainingText.text = ("--");
    }

    private void UpdatePotionQualityIndicator()
    {
        int currentQuality = GetPotionQuality();
        //Debug.Log(currentQuality);

        switch (currentQuality)
        {
            case 4:
                potionBackgroundImage.color = ultraQualityColor;
                break;

            case 3:
                potionBackgroundImage.color = highQualityColor;
                break;

            case 2:
                potionBackgroundImage.color = mediumQualityColor;
                break;

            case 1:
                potionBackgroundImage.color = lowQualityColor;
                break;

            default:
                potionBackgroundImage.color = failedColor;
                break;
        }
    }

    private int GetPotionQuality()
    {
        float _qualityPointsFromStirring = 0;
        if (shouldBeStirred && isStirred)
        {
            _qualityPointsFromStirring = .1f;
        } else if (!shouldBeStirred && !isStirred)
        {
            _qualityPointsFromStirring = .1f;
        } 
        else
        {
            _qualityPointsFromStirring = 0f;
        }

        float _qualityPointsFromTemperature = (timeAtDesiredTemp / cookTime) * .7f;

        float _qualityPointsFromLid = (timeWithLidInDesiredState / cookTime) * .2f;

        //Debug.Log("Lid points " + _qualityPointsFromLid);
        //Debug.Log("Stir points " + _qualityPointsFromStirring);
        //Debug.Log("Temperature points " + _qualityPointsFromTemperature);
        float qualityPercentage = (_qualityPointsFromStirring + _qualityPointsFromTemperature + _qualityPointsFromLid);

        // Need to make this deductive
        if (qualityPercentage >= ultraQualityTimePercentage)
        {
            potionQuality = 4;
            return 4;
        }
        if (qualityPercentage >= highQualityTimePercentage) 
        {
            potionQuality = 3;
            return 3; 
        }
        if (qualityPercentage >= mediumQualityTimePercentage) 
        {
            potionQuality = 2;
            return 2; 
        }
        if (qualityPercentage >= lowQualityTimePercentage) 
        {
            potionQuality = 1;
            return 1; 
        }
        potionQuality = 0;
        return 0;
    }

    private Color GetPotionQualityImageColor(int qualityLevel)
    {
        switch (qualityLevel)
        {
            case 4:
                currentQualityColor = ultraQualityColor;
                break;

            case 3:
                currentQualityColor = highQualityColor;
                break;

            case 2:
                currentQualityColor = mediumQualityColor;
                break;

            case 1:
                currentQualityColor = lowQualityColor;
                break;

            case 0:
                currentQualityColor = failedColor;
                break;
            default:
                currentQualityColor = Color.clear;
                break;
        }

        return currentQualityColor;
    }

    private void DisplayBrewingComplete()
    {
        //Debug.Log("Brewing complete.");
        timeRemainingText.text = ("Done!");
        isBrewing = false;
        isRetrievable = true;
        potionRetrievalButton.interactable = true;
        UpdatePotionQualityIndicator();
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

        if (ingredient4 != null)
        {
            ingredient4Image.sprite = ingredient4.itemIcon;
        }
        else
        {
            ingredient4Image.sprite = emptySlotImage;
        }
    }

    private void UpdatePotionIcon()
    {
        if (potionBeingBrewed != null || isRetrievable)
        {
            potionBackgroundImage.gameObject.SetActive(true);
        }
        else
        {
            potionImage.sprite = emptySlotImage;
            potionBackgroundImage.gameObject.SetActive(false);
        }
    }

    public bool CheckIfIngredientSlotAvailable() // Add an ingredient from the inventory into the crafting slot
    {
            if (ingredient1 == null)
            {
                return true;
            }
            else if (ingredient2 == null)
            {
                return true;
            }
            else if (ingredient3 == null)
            {
                return true;
            }
            else if (ingredient4 == null)
            {
                return true;
            }
            else
            {
                return false;
            }
    }

    public void AddIngredientToSlot(ItemData ingredient) // Add an ingredient from the inventory into the crafting slot
    {
        //Debug.Log("Adding " + ingredient);
        if (ingredient.isIngredient) // Check whether the item is an ingredient
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
            else if (ingredient4 == null)
            {
                ingredient4 = ingredient;
                ingredientSpaceLeft = false;
                return;
            }
            else
            {
                ingredientSpaceLeft = false;
                //Debug.Log("No available space.");
                return;
            }
        }
        else
        {
            //Debug.Log("No available space.");
            return; // Return if no space available amongst the four ingredient spaces
        }
    }

    public void AddItemToInventory(ItemData itemData)
    {
        AudioManager.Instance.PlayPickUpInventoryItemSound();
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
            else if (itemData == ingredient4)
            {
                itemData = null;
            }
            else
            {
                return;
            }
        } 

    }

    public void AddPotionToInventory(PotionData potionData, int qualityLevel)
    {
        //Debug.Log(potionData);
        if (potionData != null)
        {
            currentQualityColor = GetPotionQualityImageColor(qualityLevel);
            //Debug.Log(currentQualityColor);
            InventoryController.Instance.InsertPotion(potionData, qualityLevel, currentQualityColor);
            potionBeingBrewed = null;
            potionBackgroundImage.color = Color.clear;
        }
        OrderSystem.Instance.CheckForCompleteOrders();
    }

    public int CheckPotionQuality(float cookTime, float timeAtDesiredTemp) // Check what quality of potion has been made, based on time in desired temperature range
    {
       float qualityPercentage = GetPotionQuality(); // Calculate variable to represent the quality of potion made, based on time in desired temperature range

        if (qualityPercentage >= ultraQualityTimePercentage) // Check whether quality percentage is within ultra quality time range
        {
            currentQualityColor = ultraQualityColor;
            //isFailed = false; // Declare that the potion crafting process has succeeded
            return 4; // Return variable to represent an ultra quality potion (4)
        }
        else if (qualityPercentage >= highQualityTimePercentage) // Check whether quality percentage is within high quality time range
        {
            //isFailed = false; // Declare that the potion crafting process has succeeded
            currentQualityColor = highQualityColor;
            return 3; // Return variable to represent an ultra quality potion (3)
        }
        else if (qualityPercentage >= mediumQualityTimePercentage) // Check whether quality percentage is within medium quality time range
        {
            //isFailed = false; // Declare that the potion crafting process has succeeded
            currentQualityColor = mediumQualityColor;
            return 2; // Return variable to represent an ultra quality potion (2)
        }
        else if (qualityPercentage >= lowQualityTimePercentage) // Check whether quality percentage is within low quality time range
        {
            //isFailed = false; // Declare that the potion crafting process has succeeded
            currentQualityColor = lowQualityColor;
            return 1; // Return variable to represent an ultra quality potion (1)
        }
        else // Potion making has failed if lower that the low quality time limit
        {
            //isFailed = true; // Declare that the potion crafting process has failed
            currentQualityColor = failedColor;
            return 0; // // Return variable to represent that a potion was not crafted, or if it was it is inert (0)
        }
    }

    // *** CAULDRON CONTROLS ***

    private void IncreaseTemperature()
    {
        if (currentTemp < 5)
        {
            currentTemp++;
            if (currentTemp == 5)
            {
                AudioManager.Instance.PlaySwitchToBoilingTempSound();
                UpdateTemperatureDisplay();
            }
            else if (currentTemp == 4)
            {
                AudioManager.Instance.PlaySwitchToHotTempSound();
                UpdateTemperatureDisplay();
            }
            else if (currentTemp == 3)
            {
                AudioManager.Instance.PlaySwitchToMediumTempSound();
                UpdateTemperatureDisplay();
            }
            else if (currentTemp == 2)
            {
                AudioManager.Instance.PlaySwitchToColdTempSound();
                UpdateTemperatureDisplay();
            }
            else
            {
                currentTemp = 5;
                UpdateTemperatureDisplay();
            }
        }
    }

    private void DecreaseTemperature()
    {
        if (currentTemp > 1)
        {
            currentTemp--;
            if (currentTemp == 4)
            {
                AudioManager.Instance.PlaySwitchToHotTempSound();
                UpdateTemperatureDisplay();
            }
            else if (currentTemp == 3)
            {
                AudioManager.Instance.PlaySwitchToMediumTempSound();
                UpdateTemperatureDisplay();
            }
            else if (currentTemp == 2)
            {
                AudioManager.Instance.PlaySwitchToColdTempSound();
                UpdateTemperatureDisplay();
            } 
            else if (currentTemp == 1)
            {
                AudioManager.Instance.PlaySwitchToFreezingTempSound();
                UpdateTemperatureDisplay();
            }
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

    public void ToggleLid()
    {
        if (!isLidOn)
        {
            AudioManager.Instance.PlayPutLidOnSound();
            putOnLidButton.gameObject.SetActive(false);
            removeLidButton.gameObject.SetActive(true);
            isLidOn = true;
        }
        else
        {
            AudioManager.Instance.PlayTakeLidOffSound();
            removeLidButton.gameObject.SetActive(false);
            putOnLidButton.gameObject.SetActive(true);
            isLidOn = false;
        }
    }

    public void StirCauldron()
    {
        AudioManager.Instance.PlayStirSound();
        if (timeCooked > (cookTime / 2))
        {
            isStirred = true;
        }
    }

    // *** ITEM RETRIEVAL ***

    private void RetrievePotion()
    {
        if (!isBrewing)
        {
            if (isRetrievable && InventoryController.Instance.CheckForItemSpace(potionBeingBrewed))
            {
                AudioManager.Instance.PlayRetrievePotionSound();
                AddPotionToInventory(potionBeingBrewed, GetPotionQuality());
                OrderSystem.Instance.CheckForCompleteOrders();
                potionBeingBrewed = null;
                isRetrievable = false; // Reset is retrievable variable
                potionQuality = -1;
                UpdatePotionQualityIndicator();
                ResetBrewingTimer();
            }
        }
    }

    private void RetrieveItem(ItemData ingredient, int ingredientSlot)
    {
        AddItemToInventory(ingredient);
        AudioManager.Instance.PlayRemoveIngredientSound();
        if (ingredientSlot == 1)
        {
            ingredient1 = null;
        }
        else if (ingredientSlot == 2)
        {
            ingredient2 = null;
        }
        else if (ingredientSlot == 3)
        {
            ingredient3 = null;
        } 
        else
        {
            ingredient4 = null;
        }
    }
}