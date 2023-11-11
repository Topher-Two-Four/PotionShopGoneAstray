using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{

    public float maxStamina = 100f;
    public float currentStamina = 100f;
    public float staminaDrainAmount = 0.5f;
    public float staminaRegenAmount = 0.2f;


    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentStamina -= staminaDrainAmount;
        } else if (!Input.GetKeyDown(KeyCode.LeftShift) && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenAmount;
        }
        else
        {
            currentStamina = maxStamina;
        }
    }



}
