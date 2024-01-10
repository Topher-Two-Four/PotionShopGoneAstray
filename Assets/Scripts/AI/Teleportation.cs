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


    public static Teleportation Instance { get; private set; } // Singleton logic

    // Start is called before the first frame update
    void Start()
    {
        currentPhaseTime = 0f;
        phasedIn = false;
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
        if (phasedIn == true)
        {
            float playerDistance = Vector3.Distance(GameManager.Instance.playerCapsule.transform.position, transform.position);

            if (currentPhaseTime < phaseLength)
            {
                currentPhaseTime += Time.deltaTime;
            }
            else
            {
                PhaseOut();
                if (playerDistance <= phaseCatchDistance && !playerIsLooking)
                {
                    PlayerCaught();
                }
            }

        }
    }

    public void PhaseIn()
    {
        if (!phasedIn)
        {
            phasedIn = true;
            Debug.Log("Phased in.");
        }
    }

    public void PhaseOut()
    {
        phasedIn = false;
        currentPhaseTime = 0f;
        Debug.Log("Phased out.");
    }

    private void PlayerCaught()
    {
        Debug.Log("Player caught.");
        GameManager.Instance.timeRemaining -= timeRemovedWhenCaught;
        GameManager.Instance.SwitchSceneToPotionLevel();
    }
}
