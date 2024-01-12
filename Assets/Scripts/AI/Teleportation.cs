using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [Header("Teleportation/Phase Settings:")]
    [SerializeField] private float phaseLength = 10.0f;
    [SerializeField] private float phaseCatchDistance = 20.0f;
    [SerializeField] private float timeRemovedWhenCaught = 120.0f;
    [SerializeField] private MazeAIController mazeAIController;

    [Header("Audio Settings:")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip triggeredMusic;
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

        float playerDistance = Vector3.Distance(GameManager.Instance.playerCapsule.transform.position, transform.position);
        mazeAIController.gameObject.transform.position = Vector3.zero;
        mazeAIController.isPhasedIn = false;
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
