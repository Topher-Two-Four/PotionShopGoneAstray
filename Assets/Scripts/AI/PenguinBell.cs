using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBell : MonoBehaviour
{
    [Header("Audio Settings:")]
    [Tooltip("The audio source attached to the penguin NPC.")]
    [SerializeField] private AudioSource audioSource;
    [Tooltip("The penguin bell audio clip.")]
    [SerializeField] private AudioClip penguinBellSound;
    [Tooltip("Cooldown timer for penguin bell activation.")]
    [SerializeField] private float penguinCooldownTimer = 5.0f;

    public static PenguinBell Instance { get; private set; } // Singleton logic

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

    public void SoundAlarmBell()
    {
        audioSource.PlayOneShot(penguinBellSound);
        Debug.Log("Alarm sounded.");
    }

    public float GetPenguinCoolDownTime()
    {
        return penguinCooldownTimer;
    }

}
