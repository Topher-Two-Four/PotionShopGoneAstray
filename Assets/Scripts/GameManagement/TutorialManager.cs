using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("List of tutorial canvases:")]
    [Tooltip("The list of tutorial and tooltip canvases in the game.")]
    [SerializeField] private GameObject brewingStepsIntroTutorial;
    [SerializeField] private GameObject brewingSteps1Tutorial;
    [SerializeField] private GameObject brewingSteps2Tutorial;
    [SerializeField] private GameObject checkRecipeBookTutorial;
    [SerializeField] private GameObject deliveryTutorial;
    [SerializeField] private GameObject exploreTutorial;
    [SerializeField] private GameObject goodAndBadPotionTutorial1;
    [SerializeField] private GameObject goodAndBadPotionTutorial2;
    [SerializeField] private GameObject inventoryTutorial;
    [SerializeField] private GameObject jumpTutorial;
    [SerializeField] private GameObject lookingAroundShopTutorial;
    [SerializeField] private GameObject mazeAccessTutorial;
    [SerializeField] private GameObject monstersTutorial;
    [SerializeField] private GameObject movementTutorial;
    [SerializeField] private GameObject pauseGameTutorial;
    [SerializeField] private GameObject sellPotionsTutorial;


    private bool brewingStepsIntroTutorialPlayed = false;
    private bool brewingSteps1TutorialPlayed = false;
    private bool brewingSteps2TutorialPlayed = false;
    private bool checkRecipeBookTutorialPlayed = false;
    private bool deliveryTutorialPlayed = false;
    private bool exploreTutorialPlayed = false;
    private bool goodAndBadPotionTutorial1Played = false;
    private bool goodAndBadPotionTutorial2Played = false;
    private bool inventoryTutorialPlayed = false;
    private bool jumpTutorialPlayed = false;
    private bool lookingAroundShopTutorialPlayed = false;
    private bool mazeAccessTutorialPlayed = false;
    private bool monstersTutorialPlayed = false;
    private bool movementTutorialPlayed = false;
    private bool pauseGameTutorialPlayed = false;
    private bool sellPotionsTutorialPlayed = false;
    private bool mazeTutorialPlayed = false;

    [Header("Tutorial Display Settings:")]
    [Tooltip("The amount of time that elapses before the tutorial canvas is set inactive.")]
    [SerializeField] private float timeoutTime = 5.0f;
    
    [SerializeField] private bool tutorialsOn = true;
    [SerializeField] private Button toggleTutorialsButton;
    [SerializeField] private Image tutorialsToggledIndicator;

    public static TutorialManager Instance { get; private set; } // Singleton logic

    private void Start()
    {
        try
        {
            toggleTutorialsButton.onClick.AddListener(() => ToggleTutorialVisibility());
        } 
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private void Update()
    {
        if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this; } // Singleton logic
    }

    private void ToggleTutorialVisibility()
    {
        if (tutorialsOn)
        {
            tutorialsOn = false;
            tutorialsToggledIndicator.gameObject.SetActive(false);
        } else
        {
            tutorialsOn = true;
            tutorialsToggledIndicator.gameObject.SetActive(true);
        }
    }

    public void ToggleOnBrewingStepsIntroTutorial()
    {
        if (!brewingStepsIntroTutorialPlayed && tutorialsOn)
        {
            brewingStepsIntroTutorial.SetActive(true);
            Invoke("ToggleOffBrewingStepsIntroTutorial", timeoutTime);
            brewingStepsIntroTutorialPlayed = true;
        }
    }

    public void ToggleOffBrewingStepsIntroTutorial()
    {
        brewingStepsIntroTutorial.SetActive(false);
    }

    public void ToggleOnBrewingSteps1Tutorial()
    {
        if (!brewingSteps1TutorialPlayed && tutorialsOn)
        {
            brewingSteps1Tutorial.SetActive(true);
            Invoke("ToggleOffBrewingSteps1Tutorial", timeoutTime);
            brewingSteps1TutorialPlayed = true;
        }
    }

    public void ToggleOffBrewingSteps1Tutorial()
    {
        brewingSteps1Tutorial.SetActive(false);
    }

    public void ToggleOnBrewingSteps2Tutorial()
    {
        if (!brewingSteps2TutorialPlayed && tutorialsOn)
        {
            brewingSteps2Tutorial.SetActive(true);
            Invoke("ToggleOffBrewingSteps2Tutorial", timeoutTime);
            brewingSteps2TutorialPlayed = true;
        }
    }

    public void ToggleOffBrewingSteps2Tutorial()
    {
        brewingSteps2Tutorial.SetActive(false);
    }

    public void ToggleOnCheckRecipeBookTutorial()
    {
        if (!checkRecipeBookTutorialPlayed && tutorialsOn)
        {
            checkRecipeBookTutorial.SetActive(true);
            Invoke("ToggleOffCheckRecipeBookTutorial", timeoutTime);
            checkRecipeBookTutorialPlayed = true;
        }
    }

    public void ToggleOffCheckRecipeBookTutorial()
    {
        checkRecipeBookTutorial.SetActive(false);
    }
    public void ToggleOnDeliveryTutorial()
    {
        if (!deliveryTutorialPlayed && tutorialsOn)
        {
            deliveryTutorial.SetActive(true);
            Invoke("ToggleOffDeliveryTutorial", timeoutTime);
            deliveryTutorialPlayed = true;
        }
    }

    public void ToggleOffDeliveryTutorial()
    {
        deliveryTutorial.SetActive(false);
    }

    public void ToggleOnExploreTutorial()
    {
        if (!exploreTutorialPlayed && tutorialsOn)
        {
            exploreTutorial.SetActive(true);
            Invoke("ToggleOffExploreTutorial", timeoutTime);
            exploreTutorialPlayed = true;
        }
    }

    public void ToggleOffExploreTutorial()
    {
        exploreTutorial.SetActive(false);
    }
    public void ToggleOnGoodAndBadPotions1Tutorial()
    {
        if (!goodAndBadPotionTutorial1Played && tutorialsOn)
        {
            goodAndBadPotionTutorial1.SetActive(true);
            Invoke("ToggleOffGoodAndBadPotions1Tutorial", timeoutTime);
            goodAndBadPotionTutorial1Played = true;
        }
    }

    public void ToggleOffGoodAndBadPotions1Tutorial()
    {
        goodAndBadPotionTutorial1.SetActive(false);
    }

    public void ToggleOnGoodAndBadPotions2Tutorial()
    {
        if (!goodAndBadPotionTutorial2Played && tutorialsOn)
        {
            goodAndBadPotionTutorial2.SetActive(true);
            Invoke("ToggleOffGoodAndBadPotions2Tutorial", timeoutTime);
            goodAndBadPotionTutorial2Played = true;
        }
    }

    public void ToggleOffGoodAndBadPotions2Tutorial()
    {
        goodAndBadPotionTutorial2.SetActive(false);
    }

    public void ToggleOnInventoryTutorial()
    {
        if (!inventoryTutorialPlayed && tutorialsOn)
        {
            inventoryTutorial.SetActive(true);
            Invoke("ToggleOffInventoryTutorial", timeoutTime);
            inventoryTutorialPlayed = true;
        }
    }

    public void ToggleOffInventoryTutorial()
    {
        inventoryTutorial.SetActive(false);
    }
    public void ToggleOnJumpTutorial()
    {
        if (!jumpTutorialPlayed && tutorialsOn)
        {
            jumpTutorial.SetActive(true);
            Invoke("ToggleOffJumpTutorial", timeoutTime);
            jumpTutorialPlayed = true;
        }
    }

    public void ToggleOffJumpTutorial()
    {
        jumpTutorial.SetActive(false);
    }
    public void ToggleOnLookingAroundShopTutorial()
    {
        if (!lookingAroundShopTutorialPlayed && tutorialsOn)
        {
            lookingAroundShopTutorial.SetActive(true);
            Invoke("ToggleOffLookingAroundShopTutorial", timeoutTime);
            lookingAroundShopTutorialPlayed = true;
        }
    }

    public void ToggleOffLookingAroundShopTutorial()
    {
        lookingAroundShopTutorial.SetActive(false);
    }

    public void ToggleOnMazeAccessTutorial()
    {
        if (!mazeAccessTutorialPlayed && tutorialsOn)
        {
            mazeAccessTutorial.SetActive(true);
            Invoke("ToggleOffMazeAccessTutorial", timeoutTime);
            mazeAccessTutorialPlayed = true;
        }
    }

    public void ToggleOffMazeAccessTutorial()
    {
        mazeAccessTutorial.SetActive(false);
    }

    public void ToggleOnMonstersTutorial()
    {
        if (!monstersTutorialPlayed && tutorialsOn)
        {
            monstersTutorial.SetActive(true);
            Invoke("ToggleOffMonstersTutorial", timeoutTime);
            monstersTutorialPlayed = true;
        }
    }

    public void ToggleOffMonstersTutorial()
    {
        monstersTutorial.SetActive(false);
    }

    public void ToggleOnMovementTutorial()
    {
        if (!movementTutorialPlayed && tutorialsOn)
        {
            movementTutorial.SetActive(true);
            Invoke("ToggleOffMovementTutorial", timeoutTime);
            movementTutorialPlayed = true;
        }
    }

    public void ToggleOffMovementTutorial()
    {
        movementTutorial.SetActive(false);
    }
    public void ToggleOnPauseGameTutorial()
    {
        if (!pauseGameTutorialPlayed && tutorialsOn)
        {
            pauseGameTutorial.SetActive(true);
            Invoke("ToggleOffPauseGameTutorial", timeoutTime);
            pauseGameTutorialPlayed = true;
        }
    }

    public void ToggleOffPauseGameTutorial()
    {
        pauseGameTutorial.SetActive(false);
    }

    public void ToggleOnSellPotionsTutorial()
    {
        if (!sellPotionsTutorialPlayed && tutorialsOn)
        {
            sellPotionsTutorial.SetActive(true);
            Invoke("ToggleOffSellPotionsTutorial", timeoutTime);
            sellPotionsTutorialPlayed = true;
        }
    }

    public void ToggleOffSellPotionsTutorial()
    {
        sellPotionsTutorial.SetActive(false);
    }

    public void BeginMazeTutorial()
    {
        if (!mazeTutorialPlayed && tutorialsOn)
        {
            Invoke("ToggleOnMovementTutorial", 0.5f);
            Invoke("ToggleOnJumpTutorial", 0.5f);
            Invoke("ToggleOnInventoryTutorial", 1.0f + timeoutTime);
            Invoke("ToggleOnMonstersTutorial", 1.0f + timeoutTime + timeoutTime);
            Invoke("ToggleOnPauseGameTutorial", 60.0f);
        }
    }
}
