using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    [Header("Teleportation/Phase Settings:")]
    [Tooltip("The length of time from teleport in to teleport out for the maze enemy.")]
    [SerializeField] private float teleportLength = 10.0f;
    [Tooltip("The amount of time to remove from the player's day when caught by the maze enemy.")]
    [SerializeField] private float timeRemovedWhenCaught = 120.0f;
    [Tooltip("The maze enemy AI controller.")]
    [SerializeField] private GameObject jellyModel;
    [Tooltip("The amount of items to remove from the player's inventory when caught by the teleporting enemy.")]
    [SerializeField] private int caughtItemRemoveAmount = 3;
    [Tooltip("The tentacle warning  canvas for Jelly when it has teleported in.")]
    [SerializeField] private GameObject jellyWarningCanvas;

    [Header("Audio Settings:")]
    [Tooltip("The audio source attached to the maze enemy.")]
    [SerializeField] private AudioSource audioSource;
    [Tooltip("The audio clip that plays when the maze enemy is triggered.")]
    [SerializeField] private AudioClip triggeredSound;
    [Tooltip("The audio clip that plays when the maze enemy catches the player.")]
    [SerializeField] private AudioClip caughtSound;

    private float teleportedInTime = 0f;
    private bool hasTeleportedIn = false;
    private bool playerHasLooked = false;
    private bool playerCaught = false;

    public static Teleportation Instance { get; private set; } // Singleton logic

    private void Start()
    {
        TeleportOut();
        playerCaught = false;
    }

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
    }

    void Update()
    {
        if (hasTeleportedIn == true)
        {
            if (playerHasLooked)
            {
                Invoke("TeleportOut", 1.0f);
                hasTeleportedIn = false;
                Debug.Log("Jelly teleported out.");
                return;
            }
            else if (teleportedInTime < teleportLength)
            {
                teleportedInTime += Time.deltaTime;
            }
            else
            {
                if (!playerCaught)
                {
                    PlayerCaught();
                }
            }

        }
    }

    public void TeleportIn()
    {
        if (!hasTeleportedIn)
        {
            hasTeleportedIn = true;
            teleportedInTime = 0;
            audioSource.PlayOneShot(triggeredSound);
            MakeVisible();
            ToggleOnJellyWarningCanvas();
        }
    }

    public void TeleportOut()
    {
        hasTeleportedIn = false;
        teleportedInTime = 0f;

        audioSource.PlayOneShot(triggeredSound);

        MakeInvisible();
        ToggleOffJellyWarningCanvas();
        
        GetComponentInParent<MazeAIController>().MoveToRandomPosition();
        
        PlayerHasNotLooked();
    }

    public void MakeInvisible()
    {
        jellyModel.SetActive(false);
    }

    public void MakeVisible()
    {
        jellyModel.SetActive(true);
    }

    public void PlayerHasLooked()
    {
        playerHasLooked = true;
    }

    public void PlayerHasNotLooked()
    {
        playerHasLooked = false;
    }


    private void PlayerCaught()
    {
        for (int i = 0; i < caughtItemRemoveAmount; i++)
        {
            InventoryController.Instance.RemoveRandomItemFromGrid();
            CutsceneManager.Instance.PlayJellyCatchCutscene();
            AudioManager.Instance.PlayJellyCatchSound();
        }
        GameManager.Instance.timeRemaining -= timeRemovedWhenCaught;
        GameManager.Instance.SwitchSceneToPotionLevel();
    }

    private void ToggleOnJellyWarningCanvas()
    {
        jellyWarningCanvas.SetActive(true);
    }
    private void ToggleOffJellyWarningCanvas()
    {
        jellyWarningCanvas.SetActive(false);
    }

}
