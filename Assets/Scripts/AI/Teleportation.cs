using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [Header("Teleportation/Phase Settings:")]
    [Tooltip("The length of time from teleport in to teleport out for the maze enemy.")]
    [SerializeField] private float phaseLength = 10.0f;
    [Tooltip("The distance from which the player will be caught if they are within this radius when enemy teleports out.")]
    [SerializeField] private float phaseCatchDistance = 20.0f;
    [Tooltip("The amount of time to remove from the player's day when caught by the maze enemy.")]
    [SerializeField] private float timeRemovedWhenCaught = 120.0f;
    [Tooltip("The maze enemy AI controller.")]
    [SerializeField] private MazeAIController mazeAIController;

    [Header("Audio Settings:")]
    [Tooltip("The audio source attached to the maze enemy.")]
    [SerializeField] private AudioSource audioSource;
    [Tooltip("The audio clip that plays when the maze enemy is triggered.")]
    [SerializeField] private AudioClip triggeredMusic;
    [Tooltip("The audio clip that plays when the maze enemy catches the player.")]
    [SerializeField] private AudioClip caughtSound;

    private float currentPhaseTime = 0f;
    private bool phasedIn = false;
    private bool playerHasLooked = false;
    private bool hasPhasedOut = true;


    public static Teleportation Instance { get; private set; } // Singleton logic

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseTime = 0f;
        phasedIn = false;
        mazeAIController.MakeInvisible();
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

    // Update is called once per frame
    void Update()
    {
        if (phasedIn == true && !hasPhasedOut)
        {
            if (currentPhaseTime < phaseLength && !playerHasLooked)
            {
                currentPhaseTime += Time.deltaTime;
            }
            else
            {
                PhaseOut();
            }

        }
    }

    public void PhaseIn()
    {
        if (!phasedIn)
        {
            phasedIn = true;
            mazeAIController.MakeVisible();
            hasPhasedOut = false;
            Debug.Log("Phased in.");
        }
    }

    public void PhaseOut()
    {
        phasedIn = false;
        currentPhaseTime = 0f;

        float playerDistance = Vector3.Distance(GameManager.Instance.GetPlayerCapsule().transform.position, transform.position);
        mazeAIController.gameObject.transform.position = Vector3.zero;
        mazeAIController.SetPhasedIn(false);
        //mazeAIController.MakeInvisible();

        if (playerDistance <= phaseCatchDistance && !playerHasLooked)
        {
            PlayerCaught();
        }
        mazeAIController.MoveToPosition(new Vector3(60, -64, 22));

        hasPhasedOut = true;
        Debug.Log("Phased out.");
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
