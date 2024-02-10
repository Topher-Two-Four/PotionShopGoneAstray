using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

     public AudioSource musicSource;
     public AudioSource sfxSource;
     public AudioSource sfx2Source;

     public AudioClip menuButtonHoverSound;
     public AudioClip menuButtonClickSound;
     public AudioClip newDaySound;
     public AudioClip endDaySound;
     public AudioClip pauseGameSound;
     public AudioClip unpauseGameSound;
     public AudioClip winGameSound;
     public AudioClip loseGameSound;
     public AudioClip navigatePotionShopSound;

     public AudioClip openInventorySound;
     public AudioClip closeInventorySound;
     public AudioClip pickUpInventoryItemSound; 
     public AudioClip rotateInventoryItemSound;
     public AudioClip placeInventoryItemSound;
     public AudioClip trashInventorySound;
     public AudioClip dropItemSound;

     public AudioClip openBookSound;
     public AudioClip closeBookSound;
     public AudioClip switchPageSound;

     public AudioClip characterSpeechSound;
     public AudioClip completeOrderSound;
     public AudioClip increaseMoralitySound;
     public AudioClip decreaseMoralitySound;

     public AudioClip cauldronBrewingSound;
     public AudioClip cauldronBubbleSound;
     public AudioClip cauldronFireSound;
     public AudioClip stirSound;
     public AudioClip putLidOnSound;
     public AudioClip takeLidOffSound;
     public AudioClip potionStartBrewingSound;
     public AudioClip potionFinishedBrewingSound;
     public AudioClip retrievePotionSound;
     public AudioClip addIngredientSound;
     public AudioClip removeIngredientSound;
     public AudioClip switchToFreezingTempSound;
     public AudioClip switchToColdTempSound;
     public AudioClip switchToMediumTempSound;
     public AudioClip switchToHotTempSound;
     public AudioClip switchToBoilingTempSound;

     public AudioClip musicScore1;
     public AudioClip musicScore2;
     public AudioClip menuMusic1;
     public AudioClip potionShopAmbience1;
     public AudioClip potionShopAmbience2;

     public AudioClip playerWalkSound;
     public AudioClip playerRunSound;
     public AudioClip playerJumpSound;
     public AudioClip playerOutOfStaminaSound;
     public AudioClip pickUpMazeItemSound;
     public AudioClip teleportToMazeSound;
     public AudioClip teleportToShopSound;

    public static AudioManager Instance { get; private set; } // Singleton logic

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); } else { Instance = this; } // Singleton logic

        audioClips.Add("MenuButtonHover", menuButtonHoverSound);
        audioClips.Add("MenuButtonClick", menuButtonClickSound);
        audioClips.Add("NewDay", newDaySound);
        audioClips.Add("EndDay", endDaySound);
        audioClips.Add("PauseGame", pauseGameSound);
        audioClips.Add("UnpauseGame", unpauseGameSound);
        audioClips.Add("WinGame", winGameSound);
        audioClips.Add("LoseGame", loseGameSound);
        audioClips.Add("NavigatePotionShop", navigatePotionShopSound);
        audioClips.Add("OpenInventory", openInventorySound);
        audioClips.Add("CloseInventory", closeInventorySound);
        audioClips.Add("PickUpInventoryItem", pickUpInventoryItemSound);
        audioClips.Add("RotateInventoryItem", rotateInventoryItemSound);
        audioClips.Add("PlaceInventoryItem", placeInventoryItemSound);
        audioClips.Add("TrashInventory", trashInventorySound);
        audioClips.Add("DropItem", dropItemSound);
        audioClips.Add("OpenBook", openBookSound);
        audioClips.Add("CloseBook", closeBookSound);
        audioClips.Add("SwitchPage", switchPageSound);
        audioClips.Add("CharacterSpeech", characterSpeechSound);
        audioClips.Add("CompleteOrder", completeOrderSound);
        audioClips.Add("IncreaseMorality", increaseMoralitySound);
        audioClips.Add("DecreaseMorality", decreaseMoralitySound);
        audioClips.Add("CauldronBrewing", cauldronBrewingSound);
        audioClips.Add("CauldronBubble", cauldronBubbleSound);
        audioClips.Add("CauldronFire", cauldronFireSound);
        audioClips.Add("Stir", stirSound);
        audioClips.Add("PutLidOn", putLidOnSound);
        audioClips.Add("TakeLidOff", takeLidOffSound);
        audioClips.Add("PotionStartBrewing", potionStartBrewingSound);
        audioClips.Add("PotionFinishedBrewing", potionFinishedBrewingSound);
        audioClips.Add("RetrievePotion", retrievePotionSound);
        audioClips.Add("AddIngredient", addIngredientSound);
        audioClips.Add("RemoveIngredient", removeIngredientSound);
        audioClips.Add("SwitchToFreezingTemp", switchToFreezingTempSound);
        audioClips.Add("SwitchToColdTemp", switchToColdTempSound);
        audioClips.Add("SwitchToMediumTemp", switchToMediumTempSound);
        audioClips.Add("SwitchToHotTemp", switchToHotTempSound);
        audioClips.Add("SwitchToBoilingTemp", switchToBoilingTempSound);
        audioClips.Add("MusicScore1", musicScore1);
        audioClips.Add("MusicScore2", musicScore2);
        audioClips.Add("MenuMusic1", menuMusic1);
        audioClips.Add("PotionShopAmbience1", potionShopAmbience1);
        audioClips.Add("PotionShopAmbience2", potionShopAmbience2);
        audioClips.Add("PlayerWalk", playerWalkSound);
        audioClips.Add("PlayerRun", playerRunSound);
        audioClips.Add("PlayerJump", playerJumpSound);
        audioClips.Add("PlayerOutOfStamina", playerOutOfStaminaSound);
        audioClips.Add("PickUpMazeItem", pickUpMazeItemSound);
        audioClips.Add("TeleportToMaze", teleportToMazeSound);
        audioClips.Add("TeleportToShop", teleportToShopSound);
    }

    public void PlayMusic(string name)
    {
        Debug.Log(name);
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
            Debug.Log(clip);
        }
    }

    public void PlaySFX(string name)
    {
        Debug.Log(name);
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log(clip);
        }
    }
    public void PlaySFX2(string name)
    {
        Debug.Log(name);
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
            Debug.Log(clip);
        }
    }

    //Change music tempo and pitch based on good or bad

}
