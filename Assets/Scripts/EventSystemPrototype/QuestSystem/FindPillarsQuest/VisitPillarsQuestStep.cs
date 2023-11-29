using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))] // Require circle collider 2D
public class VisitPillarsQuestStep : QuestStep // Extend from quest step class
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player")) // If collision with game object tagged "Player"
        {
            FinishQuestStep(); // Complete quest step
        }
    }

    protected override void SetQuestStepState(string state)
    {
        // No need to keep track of state for this quest
    }
}
