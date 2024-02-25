using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [Header("Music Box Settings:")]
    [Tooltip("The length of time that the maze enemy plays music before player is caught upon it stopping.")]
    [SerializeField] private float musicLength = 10.0f;
    [Tooltip("The distance from which the player will be caught if they are within this radius when music stops.")]
    [SerializeField] private float musicCatchDistance = 20.0f;
    [Tooltip("The amount of time to remove from the player's day when caught by the maze enemy.")]
    [SerializeField] private float timeRemovedWhenCaught = 120.0f;
    [Tooltip("The amount of items to remove from the player's inventory when caught by the music enemy.")]
    [SerializeField] private int caughtItemRemoveAmount = 3;

    [Header("Audio Settings:")]
    [Tooltip("The audio source attached to the maze enemy.")]
    [SerializeField] private AudioSource audioSource;
    [Tooltip("The audio clip that plays when the maze enemy is triggered.")]
    [SerializeField] private AudioClip triggeredMusic;
    [Tooltip("The audio clip that plays when the maze enemy catches the player.")]
    [SerializeField] private AudioClip caughtSound;

    private bool musicPlaying = false;
    private float currentMusicPlayTime = 0f;

    public static MusicBox Instance { get; private set; } // Singleton logic

    // Start is called before the first frame update
    void Start()
    {
        currentMusicPlayTime = 0f;
        musicPlaying = false;
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
        if (musicPlaying == true)
        {
            float playerDistance = Vector3.Distance(GameManager.Instance.GetPlayerCapsule().transform.position, transform.position);

            if (currentMusicPlayTime < musicLength)
            {
                currentMusicPlayTime += Time.deltaTime;
            }
            else
            {
                StopMusic();
                if (playerDistance <= musicCatchDistance)
                {
                    PlayerCaught();
                }
            }

        }
    }

    public void PlayMusic()
    {
        if (!musicPlaying)
        {
            audioSource.Play(0);
            musicPlaying = true;
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
        musicPlaying = false;
        currentMusicPlayTime = 0f;
    }

    private void PlayerCaught()
    {
        for (int i = 0; i < caughtItemRemoveAmount; i++)
        {
            InventoryController.Instance.RemoveRandomItemFromGrid();
            //Debug.Log("Trying to remove random item from music box script...");
        }
        GameManager.Instance.timeRemaining -= timeRemovedWhenCaught;
        GameManager.Instance.SwitchSceneToPotionLevel();
    }
}
