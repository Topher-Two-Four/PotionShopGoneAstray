using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class StaminaSystem : MonoBehaviour
{
    [Header("Stamina General Settings")] // Create project menu for general stamina settings
    public float currentStamina = 100.0f;
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float jumpStaminaCost = 20.0f;
    [HideInInspector] public bool hasRegeneratedStamina = true;
    [HideInInspector] public bool isCurrentlySprinting = false;

    [Header("Stamina Regeneration Settings")] // Create project menu for stamina regeneration settings
    [Range(0, 50)] [SerializeField] private float staminaDepletionRate = 0.5f;
    [Range(0, 50)] [SerializeField] private float staminaRegenerationRate = 0.5f;

    [Header("Stamina Speed Settings")] // Create project menu for stamina speed settings
    [SerializeField] private float slowedRunSpeed = 4.0f;
    [SerializeField] private float normalRunSpeed = 4.0f;

    [Header("Stamina UI Settings")] // Create project menu for stamina UI settings
    [SerializeField] private Image staminaProgressUI = null;
    [SerializeField] private CanvasGroup sliderCanvasGroup = null;

    private FirstPersonController playerController;

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
            isCurrentlySprinting = true; // Set player has regenerated stamina to true
            currentStamina -= staminaDepletionRate * Time.deltaTime; // Deplete stamina
            UpdateStamina(1); // Update stamina UI
        }

        if (currentStamina <= 0) // Check if stamina is less than or equal to 0
        {
            hasRegeneratedStamina = false; // Set player has regenerated stamina to false
            playerController.SetSprintSpeed(slowedRunSpeed * MoralitySystem.Instance.playerSpeedModifier);
            sliderCanvasGroup.alpha = 0.2f; // Set alpha of canvas slider group to 0 so that UI will not be disabled
        }

    }

   public void StaminaJump()
    {
        if (currentStamina >= (maxStamina * jumpStaminaCost / maxStamina)) // Check if player has stamina to jump
        {
            currentStamina -= jumpStaminaCost; // Use stamina to jump
            playerController.PlayerJump(); // Allow player to jump if they have enough stamina
            UpdateStamina(1); // Update stamina UI
        }
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
