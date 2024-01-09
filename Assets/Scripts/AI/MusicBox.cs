using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip triggeredMusic;
    public AudioClip caughtSound;
    public float currentMusicPlayTime = 0f;
    public float musicLength = 10.0f;
    public bool musicPlaying = false;
    public float musicCatchDistance = 20.0f;
    public float timeRemovedWhenCaught = 120.0f;


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
            float playerDistance = Vector3.Distance(GameManager.Instance.playerCapsule.transform.position, transform.position);

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
            Debug.Log("Music started.");
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
        musicPlaying = false;
        currentMusicPlayTime = 0f;
        Debug.Log("Music stopped.");
    }

    private void PlayerCaught()
    {
        Debug.Log("Player caught.");
        GameManager.Instance.timeRemaining -= timeRemovedWhenCaught;
        GameManager.Instance.SwitchSceneToPotionLevel();
    }
}
