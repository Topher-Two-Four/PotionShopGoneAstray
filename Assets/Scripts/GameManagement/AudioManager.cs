using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

     public AudioSource musicSource;
     public AudioSource sfxSource;

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
        if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this; } // Singleton logic
    }

    public void PlayMusic(AudioClip audioClip)
    {
        Debug.Log(audioClip);
        { 
            musicSource.PlayOneShot(audioClip);
            Debug.Log(audioClip);
        }
    }

    public void PlaySFX(AudioClip audioClip)
    {
        Debug.Log(audioClip);
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(audioClip);
            Debug.Log(audioClip);
        }
    }

    //Change music tempo and pitch based on good or bad

}
