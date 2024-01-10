using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip triggeredMusic;
    public AudioClip caughtSound;
    public float currentPhaseTime = 0f;
    public float phaseLength = 10.0f;
    public bool phasedIn = false;
    public float phaseCatchDistance = 20.0f;
    public float timeRemovedWhenCaught = 120.0f;

    public bool playerIsLooking = false;
    public bool hasPhasedOut = true;
    public MazeAIController mazeAIController;

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
            if (currentPhaseTime < phaseLength && !playerIsLooking)
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

        if (playerDistance <= phaseCatchDistance && !playerIsLooking)
        {
            PlayerCaught();
        }
        mazeAIController.MoveToPosition(new Vector3(60, -64, 22));

        hasPhasedOut = true;
        Debug.Log("Phased out.");
    }

    private void PlayerCaught()
    {
        Debug.Log("Player caught.");
        GameManager.Instance.timeRemaining -= timeRemovedWhenCaught;
        GameManager.Instance.SwitchSceneToPotionLevel();
    }
}
