using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfx1VolumeSlider;
    [SerializeField] private Slider sfx2VolumeSlider;
    [SerializeField] private Slider sfx3VolumeSlider;

    [SerializeField] private AudioListener playerAudioListener;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource sfx2Source;
    public AudioSource sfx3Source;

    [Header("Menu Sounds:")]
    public AudioClip menuButtonHoverSound;
    [Range(0.0f, 1.0f)] public float menuButtonHoverSoundVolume = 0.5f;
    public AudioClip menuButtonClickSound;
    [Range(0.0f, 1.0f)] public float menuButtonClickSoundVolume = 0.5f;

    [Header("General Sounds:")]
    public AudioClip newDaySound;
    [Range(0.0f, 1.0f)] public float newDaySoundVolume = 0.5f;
    public AudioClip endDaySound;
    [Range(0.0f, 1.0f)] public float endDaySoundVolume = 0.5f;
    public AudioClip pauseGameSound;
    [Range(0.0f, 1.0f)] public float pauseGameSoundVolume = 0.5f;
    public AudioClip unpauseGameSound;
    [Range(0.0f, 1.0f)] public float unpauseGameSoundVolume = 0.5f;
    public AudioClip winGameSound;
    [Range(0.0f, 1.0f)] public float winGameSoundVolume = 0.5f;
    public AudioClip loseGameSound;
    [Range(0.0f, 1.0f)] public float loseGameSoundVolume = 0.5f;
    public AudioClip navigatePotionShopSound;
    [Range(0.0f, 1.0f)] public float navigatePotionShopSoundVolume = 0.5f;

    [Header("Inventory Sounds:")]
    public AudioClip openInventorySound;
    [Range(0.0f, 1.0f)] public float openInventorySoundVolume = 0.5f;
    public AudioClip closeInventorySound;
    [Range(0.0f, 1.0f)] public float closeInventorySoundVolume = 0.5f;
    public AudioClip pickUpInventoryItemSound;
    [Range(0.0f, 1.0f)] public float pickUpInventoryItemSoundVolume = 0.5f;
    public AudioClip rotateInventoryItemSound;
    [Range(0.0f, 1.0f)] public float rotateInventoryItemSoundVolume = 0.5f;
    public AudioClip placeInventoryItemSound;
    [Range(0.0f, 1.0f)] public float placeInventoryItemSoundVolume = 0.5f;
    public AudioClip trashInventorySound;
    [Range(0.0f, 1.0f)] public float trashInventorySoundVolume = 0.5f;
    public AudioClip dropItemSound;
    [Range(0.0f, 1.0f)] public float dropItemSoundVolume = 0.5f;

    [Header("Book Sounds:")]
    public AudioClip openBookSound;
    [Range(0.0f, 1.0f)] public float openBookSoundVolume = 0.5f;
    public AudioClip closeBookSound;
    [Range(0.0f, 1.0f)] public float closeBookSoundVolume = 0.5f;
    public AudioClip switchPageSound;
    [Range(0.0f, 1.0f)] public float switchPageSoundVolume = 0.5f;

    [Header("Character Sounds:")]
    public AudioClip characterSpeechSound;
    [Range(0.0f, 1.0f)] public float characterSpeechSoundVolume = 0.5f;
    public AudioClip completeOrderSound;
    [Range(0.0f, 1.0f)] public float completeOrderSoundVolume = 0.5f;
    public AudioClip increaseMoralitySound;
    [Range(0.0f, 1.0f)] public float increaseMoralitySoundVolume = 0.5f;
    public AudioClip decreaseMoralitySound;
    [Range(0.0f, 1.0f)] public float decreaseMoralitySoundVolume = 0.5f;

    [Header("Cauldron Sounds:")] 
    public AudioClip cauldronBrewingSound;
    [Range(0.0f, 1.0f)] public float cauldronBrewingSoundVolume = 0.5f;
    public AudioClip cauldronBubbleSound;
    [Range(0.0f, 1.0f)] public float cauldronBubbleSoundVolume = 0.5f;
    public AudioClip cauldronFireSound;
    [Range(0.0f, 1.0f)] public float cauldronFireSoundVolume = 0.5f;
    public AudioClip stirSound;
    [Range(0.0f, 1.0f)] public float stirSoundVolume = 0.5f;
    public AudioClip putLidOnSound;
    [Range(0.0f, 1.0f)] public float putLidOnSoundVolume = 0.5f;
    public AudioClip takeLidOffSound;
    [Range(0.0f, 1.0f)] public float takeLidOffSoundVolume = 0.5f;
    public AudioClip potionStartBrewingSound;
    [Range(0.0f, 1.0f)] public float potionStartBrewingSoundVolume = 0.5f;
    public AudioClip potionFinishedBrewingSound;
    [Range(0.0f, 1.0f)] public float potionFinishedBrewingSoundVolume = 0.5f;
    public AudioClip retrievePotionSound;
    [Range(0.0f, 1.0f)] public float retrievePotionSoundVolume = 0.5f;
    public AudioClip addIngredientSound;
    [Range(0.0f, 1.0f)] public float addIngredientSoundVolume = 0.5f;
    public AudioClip removeIngredientSound;
    [Range(0.0f, 1.0f)] public float removeIngredientSoundVolume = 0.5f;
    public AudioClip switchToFreezingTempSound;
    [Range(0.0f, 1.0f)] public float switchToFreezingTempSoundVolume = 0.5f;
    public AudioClip switchToColdTempSound;
    [Range(0.0f, 1.0f)] public float switchToColdTempSoundVolume = 0.5f;
    public AudioClip switchToMediumTempSound;
    [Range(0.0f, 1.0f)] public float switchToMediumTempSoundVolume = 0.5f;
    public AudioClip switchToHotTempSound;
    [Range(0.0f, 1.0f)] public float switchToHotTempSoundVolume = 0.5f;
    public AudioClip switchToBoilingTempSound;
    [Range(0.0f, 1.0f)] public float switchToBoilingTempSoundVolume = 0.5f;

    [Header("Music:")]
    public AudioClip menuMusic;
    [Range(0.0f, 1.0f)] public float menuMusicVolume = 0.5f;
    public AudioClip potionShopMusic;
    [Range(0.0f, 1.0f)] public float potionShopMusicVolume = 0.5f;
    public AudioClip mazeMusic;
    [Range(0.0f, 1.0f)] public float mazeMusicVolume = 0.5f;
    public AudioClip mazeMusicAllGood;
    [Range(0.0f, 1.0f)] public float mazeMusicAllGoodVolume = 0.5f;
    public AudioClip mazeMusicVeryGood;
    [Range(0.0f, 1.0f)] public float mazeMusicVeryGoodVolume = 0.5f;
    public AudioClip mazeMusicGood;
    [Range(0.0f, 1.0f)] public float mazeMusicGoodVolume = 0.5f;
    public AudioClip mazeMusicNeutral;
    [Range(0.0f, 1.0f)] public float mazeMusicNeutralVolume = 0.5f;
    public AudioClip mazeMusicBad;
    [Range(0.0f, 1.0f)] public float mazeMusicBadVolume = 0.5f;
    public AudioClip mazeMusicVeryBad;
    [Range(0.0f, 1.0f)] public float mazeMusicVeryBadVolume = 0.5f;
    public AudioClip mazeMusicAllBad;
    [Range(0.0f, 1.0f)] public float mazeMusicAllBadVolume = 0.5f;


    [Header("Ambient Sounds:")]
    public AudioClip potionShopAmbience1;
    [Range(0.0f, 1.0f)] public float potionShopAmbience1Volume = 0.5f;
    public AudioClip potionShopAmbience2;
    [Range(0.0f, 1.0f)] public float potionShopAmbience2Volume = 0.5f;

    [Header("Player Sounds:")]
    public AudioClip playerWalkSound;
    [Range(0.0f, 1.0f)] public float playerWalkSoundVolume = 0.5f;
    public AudioClip playerRunSound;
    [Range(0.0f, 1.0f)] public float playerRunSoundVolume = 0.5f;
    public AudioClip playerJumpSound;
    [Range(0.0f, 1.0f)] public float playerJumpSoundVolume = 0.5f;
    public AudioClip playerOutOfStaminaSound;
    [Range(0.0f, 1.0f)] public float playerOutOfStaminaSoundVolume = 0.5f;
    public AudioClip pickUpMazeItemSound;
    [Range(0.0f, 1.0f)] public float pickUpMazeItemSoundVolume = 0.5f;
    public AudioClip teleportToMazeSound;
    [Range(0.0f, 1.0f)] public float teleportToMazeSoundVolume = 0.5f;
    public AudioClip teleportToShopSound;
    [Range(0.0f, 1.0f)] public float teleportToShopSoundVolume = 0.5f;

    public static AudioManager Instance { get; private set; } // Singleton logic

    private void Start()
    {
        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
            LoadVolumePref();
        }
        else
        {
            LoadVolumePref();
        }
    }

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
        audioClips.Add("MenuMusic", menuMusic);
        audioClips.Add("ShopMusic", potionShopMusic);
        audioClips.Add("MazeMusic", mazeMusic);
        audioClips.Add("AllGoodMazeMusic", mazeMusicAllGood);
        audioClips.Add("VeryGoodMazeMusic", mazeMusicVeryGood);
        audioClips.Add("GoodMazeMusic", mazeMusicGood);
        audioClips.Add("NeutralMazeMusic", mazeMusicNeutral);
        audioClips.Add("BadMazeMusic", mazeMusicBad);
        audioClips.Add("VeryBadMazeMusic", mazeMusicVeryBad); // These are not insults, it just represents music corresponding to alignment :)
        audioClips.Add("AllBadMazeMusic", mazeMusicAllBad);
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
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void PlaySFX2(string name)
    {
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlaySFX3(string name)
    {
        if (audioClips.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = masterVolumeSlider.value;
        SaveVolumePref();
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicVolumeSlider.value;
    }

    public void ChangeSFX1Volume()
    {
        sfxSource.volume = sfx1VolumeSlider.value;
    }

    public void ChangeSFX2Volume()
    {
        sfx2Source.volume = sfx2VolumeSlider.value;
    }

    public void ChangeSFX3Volume()
    {
        sfx3Source.volume = sfx3VolumeSlider.value;
    }

    private void LoadVolumePref()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
    }

    private void SaveVolumePref()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
    }

}
