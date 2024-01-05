using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip triggeredMusic;
    public AudioClip caughtSound;
    public float lookTimeElapsed = 0f;
    public float timeToLook = 4.0f;
    public bool playerMustLook = false;
    public bool playerIsLooking = false;
    public float timeRemovedWhenCaught = 120.0f;

    public static Teleportation Instance { get; private set; } // Singleton logic

    // Start is called before the first frame update
    void Start()
    {
        lookTimeElapsed = 0f;
        playerMustLook = false;
        playerIsLooking = false;
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
        if (playerMustLook == true)
        {
            float playerDistance = Vector3.Distance(GameManager.Instance.playerCapsule.transform.position, transform.position);

            if (lookTimeElapsed < timeToLook)
            {
                lookTimeElapsed += Time.deltaTime;
                Debug.Log(playerDistance);
            }
            else
            {
                if (playerIsLooking)
                {
                    TeleportOut();
                }
                else
                {
                    PlayerCaught();
                }
            }

        }
    }

    public void TeleportIn()
    {
        if (!playerMustLook)
        {
            audioSource.Play(0);
            GetComponent<MeshRenderer>().gameObject.SetActive(true);
            playerMustLook = true;
            Debug.Log("Music started.");
        }
    }

    public void TeleportOut()
    {
        audioSource.Stop();
        GetComponent<MeshRenderer>().gameObject.SetActive(false);
        playerMustLook = false;
        lookTimeElapsed = 0f;
        Debug.Log("Music stopped.");
    }

    private void PlayerCaught()
    {
        Debug.Log("Player caught.");
        GameManager.Instance.timeRemaining -= timeRemovedWhenCaught;
        GameManager.Instance.SwitchSceneToPotionLevel();
    }



}
