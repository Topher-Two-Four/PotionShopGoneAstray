using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource MenuButtonHoverSound;
    [SerializeField] private AudioSource MenuButtonClickSound;
    [SerializeField] private AudioSource NewDaySound;
    [SerializeField] private AudioSource EndDaySound;
    [SerializeField] private AudioSource PauseGameSound;
    [SerializeField] private AudioSource UnpauseGameSound;
    [SerializeField] private AudioSource WinGameSound;
    [SerializeField] private AudioSource LoseGameSound;
    [SerializeField] private AudioSource NavigatePotionShopSound;
    
    [SerializeField] private AudioSource OpenInventorySound;
    [SerializeField] private AudioSource CloseInventorySound;
    [SerializeField] private AudioSource PickUpInventoryItemSound;
    [SerializeField] private AudioSource RotateInventoryItemSound;
    [SerializeField] private AudioSource PlaceInventoryItemSound;
    [SerializeField] private AudioSource OpenBookSound;
    [SerializeField] private AudioSource CloseBookSound;
    [SerializeField] private AudioSource SwitchPageSound;

    [SerializeField] private AudioSource CharacterSpeakSound1;
    [SerializeField] private AudioSource CharacterSpeakSound2;
    [SerializeField] private AudioSource CharacterSpeakSound3;
    [SerializeField] private AudioSource CompleteOrderSound;
    [SerializeField] private AudioSource IncreaseMoralitySound;
    [SerializeField] private AudioSource DecreaseMoralitySound;

    [SerializeField] private AudioSource CauldronBrewingSound;
    [SerializeField] private AudioSource CauldronBubbleSound;
    [SerializeField] private AudioSource CauldronFireSound;
    [SerializeField] private AudioSource StirSound;
    [SerializeField] private AudioSource PutLidOnSound;
    [SerializeField] private AudioSource TakeLidOffSound;
    [SerializeField] private AudioSource PotionStartBrewingSound;
    [SerializeField] private AudioSource PotionFinishedBrewingSound;
    [SerializeField] private AudioSource RetrievePotionSound;
    [SerializeField] private AudioSource AddIngredientSound;
    [SerializeField] private AudioSource RemoveIngredientSound;
    [SerializeField] private AudioSource SwitchToFreezingTempSound;
    [SerializeField] private AudioSource SwitchToColdTempSound;
    [SerializeField] private AudioSource SwitchToMediumTempSound;
    [SerializeField] private AudioSource SwitchToHotTempSound;
    [SerializeField] private AudioSource SwitchToBoilingTempSound;

    [SerializeField] private AudioSource MusicScore1;
    [SerializeField] private AudioSource MusicScore2;
    [SerializeField] private AudioSource MenuMusic1;
    [SerializeField] private AudioSource PotionShopAmbience1;
    [SerializeField] private AudioSource PotionShopAmbience2;

    [SerializeField] private AudioSource PlayerWalkSound;
    [SerializeField] private AudioSource PlayerRunSound;
    [SerializeField] private AudioSource PlayerJumpSound;
    [SerializeField] private AudioSource PlayerOutOfStaminaSound;
    [SerializeField] private AudioSource PickUpMazeItemSound;
    [SerializeField] private AudioSource TeleportToMazeSound;
    [SerializeField] private AudioSource TeleportToShopSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ButtonPressSound() 
    { 
    
    }

    //Change music tempo and pitch based on good or bad

}
