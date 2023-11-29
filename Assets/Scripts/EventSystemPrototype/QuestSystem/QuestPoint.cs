using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))] // Require 2D circle collider for this game object
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint; // Quest info scriptable object for this quest point

    [Header("Config")]
    [SerializeField] private bool startPoint = true; // Whether the quest point is a start point
    [SerializeField] private bool finishPoint = true; // Whether the quest point is a finish point

    private bool playerIsNear = false; // Whether player is near quest point
    private string questId; // Quest ID
    private QuestState currentQuestState; // Current state of quest

    private QuestIcon questIcon; // Icon for this quest point

    private void Awake() 
    {
        questId = questInfoForPoint.id; // Set quest ID
        questIcon = GetComponentInChildren<QuestIcon>(); // Get quest icon
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange; // Subscribe to on quest state change event
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed; // Subscribe to on submit pressed event
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange; // Unsubscribe to on quest state change event
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed; // Unsubscribe to on submit pressed event
    }

    private void SubmitPressed()
    {
        if (!playerIsNear) // If player is not near
        {
            return; // Return from method
        }

        // start or finish a quest
        if (currentQuestState.Equals(QuestState.CAN_START) && startPoint) // If player can start quest and is at start point
        {
            GameEventsManager.instance.questEvents.StartQuest(questId); // Call start quest event using quest ID to start quest
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint) // If player can finish quest and is at finish point
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId); // Call finish quest event using quest ID to finish quest
        }
    }

    private void QuestStateChange(Quest quest)
    {
        // only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId)) // If this point has a corresponding quest
        {
            currentQuestState = quest.state; // Set current quest state from corresponding quest
            questIcon.SetState(currentQuestState, startPoint, finishPoint); // Display appropriate quest state icon
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player")) // Check if collision with player
        {
            playerIsNear = true; // Set that player is near quest point
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player")) // Check if collision with player
        {
            playerIsNear = false; // Set that player is not near quest point
        }
    }
}
