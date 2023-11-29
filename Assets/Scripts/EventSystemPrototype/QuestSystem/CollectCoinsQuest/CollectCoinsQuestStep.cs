using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected = 0; // Number of coins collected
    private int coinsToComplete = 5; // Number of coins needed to complete quest

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected += CoinCollected; // Subscribe to coin collected event
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected -= CoinCollected; // Unsubscribe to coin collected event
    }

    private void CoinCollected()
    {
        if (coinsCollected < coinsToComplete) // If enough coins for quest have not yet been collected
        {
            coinsCollected++; // Add a coin to number of coins collected
            UpdateState(); // Broadcase state change
        }

        if (coinsCollected >= coinsToComplete) //Check if enough coins have been collected to complete objective
        {
            FinishQuestStep(); // Complete quest step
        }
    }

    private void UpdateState()
    {
        string state = coinsCollected.ToString(); // Update coins collected state by passing coins collected status as string
        ChangeState(state); // Change quest state based on coins collected state
    }

    protected override void SetQuestStepState(string state)
    {
        this.coinsCollected = System.Int32.Parse(state); // Take state and parse back into integer
        UpdateState(); // Broadcast state updated
    }
}
