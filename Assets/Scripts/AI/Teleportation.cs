using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [Header("Teleportation/Phase Settings:")]
    [Tooltip("The length of time from teleport in to teleport out for the maze enemy.")]
    [SerializeField] private float teleportLength = 10.0f;
    [Tooltip("The distance from which the player will be caught if they are within this radius when enemy teleports out.")]
    [SerializeField] private float teleportCatchDistance = 20.0f;
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
    private bool hasTeleportedOut = true;


    public static Teleportation Instance { get; private set; } // Singleton logic


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

        teleportedInTime = 0f;
        hasTeleportedIn = false;
        TeleportOut();
    }

    void Update()
    {
        if (hasTeleportedIn == true)
        {
            if (teleportedInTime < teleportLength && !playerHasLooked)
            {
                teleportedInTime += Time.deltaTime;
                Debug.Log("Jelly teleported in.");
            }
            else
            {
                TeleportOut();
                Debug.Log("Jelly teleported out.");
            }

        }
    }

    public void TeleportIn()
    {
        if (!hasTeleportedIn)
        {
            hasTeleportedIn = true;
            MakeVisible();
            hasTeleportedOut = false;
            Debug.Log("Teleported in.");
        }
    }

    public void TeleportOut()
    {
        hasTeleportedIn = false;
        teleportedInTime = 0f;

        float playerDistance = Vector3.Distance(GameManager.Instance.GetPlayerCapsule().transform.position, transform.position);
        MakeInvisible();

        if (playerDistance <= teleportCatchDistance && !playerHasLooked)
        {
            PlayerCaught();
        }
        
         GetComponentInParent<MazeAIController>().MoveToRandomPosition();

        hasTeleportedOut = true;
        playerHasLooked = false;
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
