using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoralitySystem : MonoBehaviour
{
    public int moralityCounter = 0;

    public TMP_Text moralityDisplayText;

    public static MoralitySystem Instance { get; private set; } // Singleton logic

    private void Start()
    {
        UpdateMoralityUI();
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
    }

    private void UpdateMoralityUI()
    {
        if (moralityCounter >= 10)
        {
            moralityDisplayText.text = "Alignment: All Good (" + moralityCounter + ")";
            moralityDisplayText.color = new Color(0, 255, 0);
        }
        else if (moralityCounter >= 5 && moralityCounter < 10)
        {
            moralityDisplayText.text = "Alignment: Very Good (" + moralityCounter + ")";
            moralityDisplayText.color = new Color(255, 130, 0);
        }
        else if (moralityCounter >= 1 && moralityCounter < 5)
        {
            moralityDisplayText.text = "Alignment: Good (" + moralityCounter + ")";
            moralityDisplayText.color = new Color(255, 255, 0);
        }
        else if (moralityCounter >= -1 && moralityCounter < 1)
        {
            moralityDisplayText.text = "Alignment: Neutral (" + moralityCounter + ")";
            moralityDisplayText.color = Color.gray;
        }
        else if (moralityCounter >= -5 && moralityCounter < -1)
        {
            moralityDisplayText.text = "Alignment: Bad (" + moralityCounter + ")";
            moralityDisplayText.color = new Color(255, 203, 0);
        }
        else if (moralityCounter > -10 && moralityCounter < -5)
        {
            moralityDisplayText.text = "Alignment: Very Bad (" + moralityCounter + ")";
            moralityDisplayText.color = new Color(255, 110, 0);
        }
        else
        {
            moralityDisplayText.text = "Alignment: All Bad (" + moralityCounter + ")";
            moralityDisplayText.color = new Color(255, 0, 0);
        }
    }

}
