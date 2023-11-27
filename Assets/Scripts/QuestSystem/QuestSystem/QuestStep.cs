using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false; // Whether quest step is finished
    private string questId; // Quest ID
    private int stepIndex; // Step index for this quest step

    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId; // Set quest ID for this step to the one passed through parameter
        this.stepIndex = stepIndex; // Set step index to be the one passed through parameter
        if (questStepState != null && questStepState != "") // If quest step state exists
        {
            SetQuestStepState(questStepState); // Set quest step state based on parameter
        }
    }

    protected void FinishQuestStep()
    {
        if (!isFinished) // Check if quest step has been finished
        {
            isFinished = true; // If quest step has been completed then mark as finished
            GameEventsManager.instance.questEvents.AdvanceQuest(questId); // Advance quest forward
            Destroy(this.gameObject); // Destroy this quest step as it is no longer needed
        }
    }

    protected void ChangeState(string newState)
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState)); // Call quest step change event with new step
    }

    protected abstract void SetQuestStepState(string state); // Abstract class taking in quest step state as parameter
}
