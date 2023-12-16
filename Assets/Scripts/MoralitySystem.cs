using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralitySystem : MonoBehaviour
{

    public int moralityCounter = 0;

    public static MoralitySystem Instance { get; private set; } // Singleton logic

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
    }

}
