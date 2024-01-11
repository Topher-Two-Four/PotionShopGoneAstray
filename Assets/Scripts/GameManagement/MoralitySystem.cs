using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoralitySystem : MonoBehaviour
{
    public int moralityCounter = 0;

    public float monsterSpeedModifier;
    public float playerSpeedModifier;
    public float ingredientSpawnModifier;
    public float mazeLightModifer;
    public float mazeFogModifer;

    public TMP_Text[] mText;

    public static MoralitySystem Instance { get; private set; } // Singleton logic

    private void Start()
    {
        UpdateMoralityUI();
        monsterSpeedModifier = 1.0f;
        playerSpeedModifier = 1.0f;
        ingredientSpawnModifier = 1.0f;
        mazeLightModifer = 1.0f;
        mazeFogModifer = 1.0f;
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

        public void AdjustMoralityCounter(PotionData potionData)
    {
        if (potionData.isHealth)
        {
            moralityCounter += 3;
        }
        else if (potionData.isBenefit)
        {
            moralityCounter += 2;
        }
        else if (potionData.isAntidote)
        {
            moralityCounter++;
        }
        else if (potionData.isHatred)
        {
            moralityCounter--;
        }
        else if (potionData.isCrippling)
        {
            moralityCounter -= 2;
        }
        else if (potionData.isPoison)
        {
            moralityCounter -= 3;
        } 
        else if (potionData.isDeath)
        {
            moralityCounter -= 4;
        }
        UpdateMoralityUI();
        // ApplyMoralityEffect();
    }

    public void UpdateMoralityUI()
    {
        if (moralityCounter >= 10)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: All Good (" + moralityCounter + ")";
                mText.color = new Color(0, 255, 0);
            }
        }
        else if (moralityCounter >= 5 && moralityCounter < 10)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Very Good (" + moralityCounter + ")";
                mText.color = new Color(255, 130, 0);
            }
        }
        else if (moralityCounter >= 1 && moralityCounter < 5)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Good (" + moralityCounter + ")";
                mText.color = new Color(255, 255, 0);
            }
        }
        else if (moralityCounter >= -1 && moralityCounter < 1)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Neutral (" + moralityCounter + ")";
                mText.color = Color.gray;
            }
        }
        else if (moralityCounter >= -5 && moralityCounter < -1)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Bad (" + moralityCounter + ")";
                mText.color = new Color(255, 203, 0);
            }
        }
        else if (moralityCounter > -10 && moralityCounter < -5)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Very Bad (" + moralityCounter + ")";
                mText.color = new Color(255, 110, 0);
            }

        }
        else
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: All Bad (" + moralityCounter + ")";
                mText.color = new Color(255, 0, 0);
            }
        }
    }

    public void ApplyMoralityEffect()
    {
        if (moralityCounter >= 10)
        {
            monsterSpeedModifier = 0.5f; //Decrease monster speed
            mazeLightModifer = 1.3f; //Increase light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.01f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.5f; //Increase amount of items that spawn
            playerSpeedModifier = 1.2f; //Increase player speed
        }
        else if (moralityCounter >= 5 && moralityCounter < 10)
        {
            monsterSpeedModifier = 0.7f;//Decrease monster speed
            mazeLightModifer = 1.2f; //Increase light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.03f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.3f; //Increase amount of items that spawn
            playerSpeedModifier = 1.1f; //Increase player speed
        }
        else if (moralityCounter >= 1 && moralityCounter < 5)
        {
            monsterSpeedModifier = 0.9f; //Decrease monster speed
            mazeLightModifer = 1.1f; //Increase light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.07f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.1f; //Increase amount of items that spawn
            playerSpeedModifier = 1.0f; //Normal player speed
        }
        else if (moralityCounter >= -1 && moralityCounter < 1)
        {
            monsterSpeedModifier = 1.0f; //Normal monster speed
            mazeLightModifer = 1.0f; //Normal light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.1f; // Normal fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.0f; //Normal amount of items spawn
            playerSpeedModifier = 1.0f; //Normal player speed
        }
        else if (moralityCounter >= -5 && moralityCounter < -1)
        {
            monsterSpeedModifier = 1.1f; //Increase monster speed
            mazeLightModifer = 0.9f; //Decrease light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.15f; // Increase fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 0.9f; //Decrease amount of items that spawn
            playerSpeedModifier = 1.0f; //Normal player speed
        }
        else if (moralityCounter > -10 && moralityCounter < -5)
        {
            monsterSpeedModifier = 1.3f; //Increase monster speed
            mazeLightModifer = 0.8f; //Decrease light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.23f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 0.7f; //Decrease amount of items that spawn
            playerSpeedModifier = 1.0f; //Normal player speed
        }
        else
        {
            monsterSpeedModifier = 1.5f; //Increase monster speed
            mazeLightModifer = 0.7f; //Decrease light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.3f; // Increase fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 0.5f; //Decrease amount of items that spawn
            playerSpeedModifier = 0.9f; //Decreased player speed
        }

    }

}
