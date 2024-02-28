using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("List of Rooms:")]
    [Tooltip("The list of tutorials and tooltips in the game.")]
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

    public static TutorialManager Instance { get; private set; } // Singleton logic

    private void Update()
    {
        if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this; } // Singleton logic
    }

    public void ToggleOnBrewingStepsIntroTutorial()
    {
        brewingStepsIntroTutorial.SetActive(true);
    }

    public void ToggleOffBrewingStepsIntroTutorial()
    {
        brewingStepsIntroTutorial.SetActive(false);
    }

    public void ToggleOnBrewingSteps1Tutorial()
    {
        brewingSteps1Tutorial.SetActive(true);
    }

    public void ToggleOffBrewingSteps1Tutorial()
    {
        brewingSteps1Tutorial.SetActive(false);
    }

    public void ToggleOnBrewingSteps2Tutorial()
    {
        brewingSteps2Tutorial.SetActive(true);
    }

    public void ToggleOffBrewingSteps2Tutorial()
    {
        brewingSteps2Tutorial.SetActive(false);
    }

    public void ToggleOnCheckRecipeBookTutorial()
    {
        checkRecipeBookTutorial.SetActive(true);
    }

    public void ToggleOffCheckRecipeBookTutorial()
    {
        checkRecipeBookTutorial.SetActive(false);
    }
    public void ToggleOnDeliveryTutorial()
    {
        deliveryTutorial.SetActive(true);
    }

    public void ToggleOffDeliveryTutorial()
    {
        deliveryTutorial.SetActive(false);
    }

    public void ToggleOnExploreTutorial()
    {
        exploreTutorial.SetActive(true);
    }

    public void ToggleOffExploreTutorial()
    {
        exploreTutorial.SetActive(false);
    }
    public void ToggleOnGoodAndBadPotions1Tutorial()
    {
        goodAndBadPotionTutorial1.SetActive(true);
    }

    public void ToggleOffGoodAndBadPotions1Tutorial()
    {
        goodAndBadPotionTutorial1.SetActive(false);
    }

    public void ToggleOnGoodAndBadPotions2Tutorial()
    {
        goodAndBadPotionTutorial2.SetActive(true);
    }

    public void ToggleOffGoodAndBadPotions2Tutorial()
    {
        goodAndBadPotionTutorial2.SetActive(false);
    }

    public void ToggleOnInventoryTutorial()
    {
        inventoryTutorial.SetActive(true);
    }

    public void ToggleOffInventoryTutorial()
    {
        inventoryTutorial.SetActive(false);
    }
    public void ToggleOnJumpTutorial()
    {
        jumpTutorial.SetActive(true);
    }

    public void ToggleOffJumpTutorial()
    {
        jumpTutorial.SetActive(false);
    }
    public void ToggleOnLookingAroundShopTutorial()
    {
        lookingAroundShopTutorial.SetActive(true);
    }

    public void ToggleOffLookingAroundShopTutorial()
    {
        lookingAroundShopTutorial.SetActive(false);
    }

    public void ToggleOnMazeAccessTutorial()
    {
        mazeAccessTutorial.SetActive(true);
    }

    public void ToggleOffMazeAccessTutorial()
    {
        mazeAccessTutorial.SetActive(false);
    }

    public void ToggleOnMonstersTutorial()
    {
        monstersTutorial.SetActive(true);
    }

    public void ToggleOffMonstersTutorial()
    {
        monstersTutorial.SetActive(false);
    }

    public void ToggleOnMovementTutorial()
    {
        movementTutorial.SetActive(true);
    }

    public void ToggleOffMovementTutorial()
    {
        movementTutorial.SetActive(false);
    }
    public void ToggleOnPauseGameTutorial()
    {
        pauseGameTutorial.SetActive(true);
    }

    public void ToggleOffPauseGameTutorial()
    {
        pauseGameTutorial.SetActive(false);
    }

    public void ToggleOnSellPotionsTutorial()
    {
        sellPotionsTutorial.SetActive(true);
    }

    public void ToggleOffSellPotionsTutorial()
    {
        sellPotionsTutorial.SetActive(false);
    }
}
