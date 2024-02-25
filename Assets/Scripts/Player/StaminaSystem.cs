using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class StaminaSystem : MonoBehaviour
{

    [Header("Stamina General Settings")] // Create project menu for general stamina settings
    [Tooltip("The first person controller for the player character.")]
    [SerializeField] private FirstPersonController playerController;
    [Tooltip("The player's maximum stamina.")]
    [SerializeField] private float maxStamina = 100.0f;
    [Tooltip("The amount of stamina that it costs for the player to jump.")]
    [SerializeField] private float jumpStaminaCost = 20.0f;

    [Header("Stamina Regeneration Settings")] // Create project menu for stamina regeneration settings
    [Tooltip("The rate at which stamina is depleted.")]
    [Range(0, 50)] [SerializeField] private float staminaDepletionRate = 0.5f;
    [Tooltip("The rate at which stamina is regenerated.")]
    [Range(0, 50)] [SerializeField] private float staminaRegenerationRate = 0.5f;

    [Header("Stamina Speed Settings")] // Create project menu for stamina speed settings
    [Tooltip("The speed that the player runs at when out of stamina.")]
    [SerializeField] private float slowedRunSpeed = 4.0f;
    [Tooltip("The speed that the player runs at normally.")]
    [SerializeField] private float normalRunSpeed = 4.0f;

    [Header("Stamina UI Settings")] // Create project menu for stamina UI settings
    public Image staminaProgressUI = null;
    public CanvasGroup sliderCanvasGroup = null;

    [HideInInspector] public float currentStamina = 100.0f;
    [HideInInspector] public bool hasRegeneratedStamina = true;
    [HideInInspector] public bool isCurrentlySprinting = false;

    public static StaminaSystem Instance { get; private set; } // Singleton logic

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this); } else { Instance = this; } // Singleton logic

    }

    private void Start()
    {
        playerController = GetComponent<FirstPersonController>(); // Get first person controller component of player controller
    }

    private void Update()
    {
        if (!isCurrentlySprinting) // Check if player is currently sprinting
        {
            if (currentStamina <= maxStamina - 0.01) // Check if stamina is not at maximum
            {
                currentStamina += staminaRegenerationRate * Time.deltaTime; // Regenerate stamina
                UpdateStamina(1); // Update stamina UI

                if (currentStamina >= 50) // Check if stamina is at or above maximum
                {
                    playerController.SetSprintSpeed(normalRunSpeed * MoralitySystem.Instance.playerSpeedModifier);
                    hasRegeneratedStamina = true; // Set player has regenerated stamina to true
                    staminaProgressUI.color = Color.white;
                    if (currentStamina >= maxStamina)
                    {
                        sliderCanvasGroup.alpha = 0;// Set alpha of canvas slider group to 0 so that UI will not be disabled
                    }
                }
            }
        }
    }

    public void Sprinting()
    {
        if (hasRegeneratedStamina) // Check if player has regenerated their stamina
        {
            //AudioManager.Instance.PlaySFX2("PlayerRun");
            isCurrentlySprinting = true; // Set player has regenerated stamina to true
            currentStamina -= staminaDepletionRate * Time.deltaTime; // Deplete stamina
            UpdateStamina(1); // Update stamina UI
        }

        if (currentStamina <= 0) // Check if stamina is less than or equal to 0
        {
            AudioManager.Instance.PlaySFX("PlayerOutOfStamina");
            hasRegeneratedStamina = false; // Set player has regenerated stamina to false
            playerController.SetSprintSpeed(slowedRunSpeed * MoralitySystem.Instance.playerSpeedModifier);
            staminaProgressUI.color = Color.gray;
        }

    }

   public void StaminaJump()
    {
        if (currentStamina >= (maxStamina * jumpStaminaCost / maxStamina)) // Check if player has stamina to jump
        {
            AudioManager.Instance.PlaySFX2("PlayerJump");
            currentStamina -= jumpStaminaCost; // Use stamina to jump
            playerController.PlayerJump(); // Allow player to jump if they have enough stamina
            UpdateStamina(1); // Update stamina UI

            if (currentStamina <= 0) // Check if stamina is less than or equal to 0
            {
                AudioManager.Instance.PlaySFX("PlayerOutOfStamina");
                hasRegeneratedStamina = false; // Set player has regenerated stamina to false
                playerController.SetSprintSpeed(slowedRunSpeed * MoralitySystem.Instance.playerSpeedModifier);
                staminaProgressUI.color = Color.gray;
            }
        }
    }
    
    public void HaltMovement()
    {
        AudioManager.Instance.sfx2Source.Stop();
        slowedRunSpeed = 0;
        normalRunSpeed = 0;
    }

    void UpdateStamina(int alphaValue)
    {
        staminaProgressUI.fillAmount = currentStamina / maxStamina; // Adjust and display stamina as percentage of slider bar

        if (alphaValue == 0)
        {
            sliderCanvasGroup.alpha = 0;
        }
        else
        {
            sliderCanvasGroup.alpha = 1;
        }
    }


}
