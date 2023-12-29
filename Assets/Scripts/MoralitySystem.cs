using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoralitySystem : MonoBehaviour
{
    public int moralityCounter = 0;

    public TMP_Text[] mText;

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

}
