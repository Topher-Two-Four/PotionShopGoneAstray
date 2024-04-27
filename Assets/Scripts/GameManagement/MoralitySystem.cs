using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoralitySystem : MonoBehaviour
{
    [Header("Button Settings:")]


    [Header("Maze Modifier Settings:")]

    [SerializeField] private float mazeLightModifer;
    [SerializeField] private float mazeFogModifer;

    [Header("Text Settings:")]
    [SerializeField] private TMP_Text[] mText;
    [SerializeField] private Color allGoodTextColor = Color.blue;
    [SerializeField] private Color veryGoodTextColor = Color.cyan;
    [SerializeField] private Color goodTextColor = Color.green;
    [SerializeField] private Color neutralTextColor = Color.white;
    [SerializeField] private Color badTextColor = Color.yellow;
    [SerializeField] private Color veryBadTextColor = Color.magenta;
    [SerializeField] private Color allBadTextColor = Color.red;

    [HideInInspector] public float monsterSpeedModifier;
    [HideInInspector] public float playerSpeedModifier;
    [HideInInspector] public float ingredientSpawnModifier;
    [HideInInspector] public float skullBearSpawnModifier;
    [HideInInspector] public int enemySpawnAmount;

    private int moralityCounter = 0;



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
            AudioManager.Instance.PlayIncreaseMoralitySound();
            moralityCounter += 3;
        }
        else if (potionData.isBenefit)
        {
            AudioManager.Instance.PlayIncreaseMoralitySound();
            moralityCounter += 2;
        }
        else if (potionData.isAntidote)
        {
            AudioManager.Instance.PlayIncreaseMoralitySound();
            moralityCounter++;
        }
        else if (potionData.isHatred)
        {
            AudioManager.Instance.PlayDecreaseMoralitySound();
            moralityCounter--;
        }
        else if (potionData.isHarmful)
        {
            AudioManager.Instance.PlayDecreaseMoralitySound();
            moralityCounter -= 2;
        }
        else if (potionData.isPoison)
        {
            AudioManager.Instance.PlayDecreaseMoralitySound();
            moralityCounter -= 3;
        } 
        else if (potionData.isDeath)
        {
            AudioManager.Instance.PlayDecreaseMoralitySound();
            moralityCounter -= 4;
        }
        UpdateMoralityUI();
    }

    public void UpdateMoralityUI()
    {
        if (moralityCounter >= 10)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: All Good (" + moralityCounter + ")";
                mText.color = allGoodTextColor;
            }
        }
        else if (moralityCounter >= 5 && moralityCounter < 10)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Very Good (" + moralityCounter + ")";
                mText.color = veryGoodTextColor;
            }
        }
        else if (moralityCounter >= 1 && moralityCounter < 5)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Good (" + moralityCounter + ")";
                mText.color = goodTextColor;
            }
        }
        else if (moralityCounter >= -1 && moralityCounter < 1)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Neutral (" + moralityCounter + ")";
                mText.color = neutralTextColor;
            }
        }
        else if (moralityCounter >= -5 && moralityCounter < -1)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Bad (" + moralityCounter + ")";
                mText.color = badTextColor;
            }
        }
        else if (moralityCounter > -10 && moralityCounter < -5)
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: Very Bad (" + moralityCounter + ")";
                mText.color = veryBadTextColor;
            }

        }
        else
        {
            foreach (TMP_Text mText in mText)
            {
                mText.text = "Alignment: All Bad (" + moralityCounter + ")";
                mText.color = allBadTextColor;
            }
        }
    }

    public int GetMoralityCount()
    {
        return moralityCounter;
    }
    public void SetMoralityCount(int moralityCount)
    {
        moralityCounter = moralityCount;
    }

    public void ResetMoralityCounter()
    {
        moralityCounter = 0;
    }

    public void ApplyMoralityEffect()
    {
        if (moralityCounter >= 10)
        {
            AudioManager.Instance.musicSource.pitch = 1.1f;
            // AudioManager.Instance.PlayMusic("AllGoodMazeMusic");
            monsterSpeedModifier = 0.5f; //Decrease monster speed
            mazeLightModifer = 1.3f; //Increase light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.01f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.5f; //Increase amount of items that spawn
            skullBearSpawnModifier = 0.5f;
            ItemPot.Instance.SetItemsCollectedDivider(1);
            enemySpawnAmount = Random.Range(0, 2);
            playerSpeedModifier = 1.2f; //Increase player speed
        }
        else if (moralityCounter >= 5 && moralityCounter < 10)
        {
            AudioManager.Instance.musicSource.pitch = 1.07f;
            // AudioManager.Instance.PlayMusic("VeryGoodMazeMusic");
            monsterSpeedModifier = 0.7f;//Decrease monster speed
            mazeLightModifer = 1.2f; //Increase light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.03f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.3f; //Increase amount of items that spawn
            skullBearSpawnModifier = 0.7f;
            ItemPot.Instance.SetItemsCollectedDivider(2);
            enemySpawnAmount = 1;
            playerSpeedModifier = 1.1f; //Increase player speed
        }
        else if (moralityCounter >= 1 && moralityCounter < 5)
        {
            AudioManager.Instance.musicSource.pitch = 1.03f;
            // AudioManager.Instance.PlayMusic("GoodMazeMusic");
            monsterSpeedModifier = 0.9f; //Decrease monster speed
            mazeLightModifer = 1.1f; //Increase light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.07f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.1f; //Increase amount of items that spawn
            skullBearSpawnModifier = 0.9f;
            ItemPot.Instance.SetItemsCollectedDivider(3);
            enemySpawnAmount = 1;
            playerSpeedModifier = 1.0f; //Normal player speed
        }
        else if (moralityCounter >= -1 && moralityCounter < 1)
        {
            AudioManager.Instance.musicSource.pitch = 1.0f;
            // AudioManager.Instance.PlayMusic("NeutralMazeMusic");
            monsterSpeedModifier = 1.0f; //Normal monster speed
            mazeLightModifer = 1.0f; //Normal light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.1f; // Normal fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 1.0f; //Normal amount of items spawn
            skullBearSpawnModifier = 1.0f;
            ItemPot.Instance.SetItemsCollectedDivider(4);
            enemySpawnAmount = Random.Range(1, 3);
            playerSpeedModifier = 1.0f; //Normal player speed
        }
        else if (moralityCounter >= -5 && moralityCounter < -1)
        {
            AudioManager.Instance.musicSource.pitch = 0.7f;
            // AudioManager.Instance.PlayMusic("BadMazeMusic");
            monsterSpeedModifier = 1.1f; //Increase monster speed
            mazeLightModifer = 0.9f; //Decrease light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.15f; // Increase fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 0.9f; //Decrease amount of items that spawn
            skullBearSpawnModifier = 1.3f;
            ItemPot.Instance.SetItemsCollectedDivider(5);
            enemySpawnAmount = Random.Range(1, 3);
            playerSpeedModifier = 0.95f; //Decreased player speed
        }
        else if (moralityCounter > -10 && moralityCounter < -5)
        {
            AudioManager.Instance.musicSource.pitch = -1.1f;
            // AudioManager.Instance.PlayMusic("VeryBadMazeMusic");
            monsterSpeedModifier = 1.3f; //Increase monster speed
            mazeLightModifer = 0.8f; //Decrease light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.23f; // Decrease fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 0.7f; //Decrease amount of items that spawn
            skullBearSpawnModifier = 1.5f;
            ItemPot.Instance.SetItemsCollectedDivider(6);
            enemySpawnAmount = Random.Range(2, 4);
            playerSpeedModifier = 0.9f; //Decreased player speed
        }
        else
        {
            AudioManager.Instance.musicSource.pitch = -1.3f;
            // AudioManager.Instance.PlayMusic("AllBadMazeMusic");
            monsterSpeedModifier = 1.5f; //Increase monster speed
            mazeLightModifer = 0.7f; //Decrease light in maze
            RenderSettings.sun.intensity = mazeLightModifer;
            mazeFogModifer = 0.3f; // Increase fog in maze
            RenderSettings.fogDensity = mazeFogModifer;
            ingredientSpawnModifier = 0.5f; //Decrease amount of items that spawn
            skullBearSpawnModifier = 2.0f;
            ItemPot.Instance.SetItemsCollectedDivider(7);
            enemySpawnAmount = 3;
            playerSpeedModifier = 0.85f; //Decreased player speed
        }
    }

    public float GetIngredientSpawnModifier()
    {
        return ingredientSpawnModifier;
    }

    public float GetSkullBearSpawnModifier()
    {
        return skullBearSpawnModifier;
    }

    public int GetEnemySpawnAmount()
    {
        return enemySpawnAmount;
    }

}
