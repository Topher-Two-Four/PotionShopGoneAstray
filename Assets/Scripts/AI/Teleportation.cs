using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [Header("Teleportation/Phase Settings:")]
    [Tooltip("The length of time from teleport in to teleport out for the maze enemy.")]
    [SerializeField] private float teleportLength = 10.0f;
    [Tooltip("The amount of time to remove from the player's day when caught by the maze enemy.")]
    [SerializeField] private float timeRemovedWhenCaught = 120.0f;
    [Tooltip("The maze enemy AI controller.")]
    [SerializeField] private GameObject jellyModel;

    [Header("Audio Settings:")]
    [Tooltip("The audio source attached to the maze enemy.")]
    [SerializeField] private AudioSource audioSource;
    [Tooltip("The audio clip that plays when the maze enemy is triggered.")]
    [SerializeField] private AudioClip triggeredMusic;
    [Tooltip("The audio clip that plays when the maze enemy catches the player.")]
    [SerializeField] private AudioClip caughtSound;

    private float teleportedInTime = 0f;
    private bool hasTeleportedIn = false;
    private bool playerHasLooked = false;

    public static Teleportation Instance { get; private set; } // Singleton logic

    private void Start()
    {
        TeleportOut();
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
                TeleportOut();
                hasTeleportedIn = false;
                Debug.Log("Jelly teleported out.");
                return;
            }
            else if (teleportedInTime < teleportLength)
            {
                teleportedInTime += Time.deltaTime;
                //Debug.Log(teleportedInTime);
            } 
            else
            {
                PlayerCaught();
                Debug.Log("Player caught.");
            }

        }
    }

    public void TeleportIn()
    {
        if (!hasTeleportedIn)
        {
            hasTeleportedIn = true;
            teleportedInTime = 0;
            MakeVisible();
            Debug.Log("Teleported in.");
        }
    }

    public void TeleportOut()
    {
        hasTeleportedIn = false;
        teleportedInTime = 0f;

        MakeInvisible();

        GetComponentInParent<MazeAIController>().MoveToRandomPosition();
        PlayerHasNotLooked();

        Debug.Log("Teleported out.");
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
        Debug.Log("Player caught.");
        GameManager.Instance.timeRemaining -= timeRemovedWhenCaught;
        GameManager.Instance.SwitchSceneToPotionLevel();
    }
}
