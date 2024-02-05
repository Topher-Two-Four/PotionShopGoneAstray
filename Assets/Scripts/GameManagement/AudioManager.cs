using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip MenuButtonHoverSound;
    [SerializeField] private AudioClip MenuButtonClickSound;
    [SerializeField] private AudioClip NewDaySound;
    [SerializeField] private AudioClip EndDaySound;
    [SerializeField] private AudioClip PauseGameSound;
    [SerializeField] private AudioClip UnpauseGameSound;
    [SerializeField] private AudioClip WinGameSound;
    [SerializeField] private AudioClip LoseGameSound;
    [SerializeField] private AudioClip NavigatePotionShopSound;

    [SerializeField] private AudioClip OpenInventorySound;
    [SerializeField] private AudioClip CloseInventorySound;
    [SerializeField] private AudioClip PickUpInventoryItemSound; 
    [SerializeField] private AudioClip RotateInventoryItemSound;
    [SerializeField] private AudioClip PlaceInventoryItemSound;
    [SerializeField] private AudioClip TrashInventorySound;
    [SerializeField] private AudioClip DropItemSound;

    [SerializeField] private AudioClip OpenBookSound;
    [SerializeField] private AudioClip CloseBookSound;
    [SerializeField] private AudioClip SwitchPageSound;

    [SerializeField] private AudioClip CharacterSpeechSound;
    [SerializeField] private AudioClip CompleteOrderSound;
    [SerializeField] private AudioClip IncreaseMoralitySound;
    [SerializeField] private AudioClip DecreaseMoralitySound;

    [SerializeField] private AudioClip CauldronBrewingSound;
    [SerializeField] private AudioClip CauldronBubbleSound;
    [SerializeField] private AudioClip CauldronFireSound;
    [SerializeField] private AudioClip StirSound;
    [SerializeField] private AudioClip PutLidOnSound;
    [SerializeField] private AudioClip TakeLidOffSound;
    [SerializeField] private AudioClip PotionStartBrewingSound;
    [SerializeField] private AudioClip PotionFinishedBrewingSound;
    [SerializeField] private AudioClip RetrievePotionSound;
    [SerializeField] private AudioClip AddIngredientSound;
    [SerializeField] private AudioClip RemoveIngredientSound;
    [SerializeField] private AudioClip SwitchToFreezingTempSound;
    [SerializeField] private AudioClip SwitchToColdTempSound;
    [SerializeField] private AudioClip SwitchToMediumTempSound;
    [SerializeField] private AudioClip SwitchToHotTempSound;
    [SerializeField] private AudioClip SwitchToBoilingTempSound;

    [SerializeField] private AudioClip MusicScore1;
    [SerializeField] private AudioClip MusicScore2;
    [SerializeField] private AudioClip MenuMusic1;
    [SerializeField] private AudioClip PotionShopAmbience1;
    [SerializeField] private AudioClip PotionShopAmbience2;

    [SerializeField] private AudioClip PlayerWalkSound;
    [SerializeField] private AudioClip PlayerRunSound;
    [SerializeField] private AudioClip PlayerJumpSound;
    [SerializeField] private AudioClip PlayerOutOfStaminaSound;
    [SerializeField] private AudioClip PickUpMazeItemSound;
    [SerializeField] private AudioClip TeleportToMazeSound;
    [SerializeField] private AudioClip TeleportToShopSound;

    void Start()
    {
        
    }

    public void ButtonPressSound()
    { 
    
    }

    //Change music tempo and pitch based on good or bad

}
