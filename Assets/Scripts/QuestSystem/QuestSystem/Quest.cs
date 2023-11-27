using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info; // Static quest data

    public QuestState state; // Current state of the quest
    private int currentQuestStepIndex; // Current quest step index
    private QuestStepState[] questStepStates; // Array of quest step states

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo; // Set quest info
        this.state = QuestState.REQUIREMENTS_NOT_MET; // Set initial quest state to be requirements not met
        this.currentQuestStepIndex = 0; // Set initial quest step index to 0
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length]; // Initialize quest step states array of the same length as quest step prefabs
        for (int i = 0; i < questStepStates.Length; i++) // Loop through each quest step state
        {
            questStepStates[i] = new QuestStepState(); // Initialize each quest step to a new quest state game object so values are not null
        }
    }

    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates) // Constructor using saved quest data
    {
        this.info = questInfo; // Set quest info
        this.state = questState; // Set quest state
        this.currentQuestStepIndex = currentQuestStepIndex; // Set current quest step index
        this.questStepStates = questStepStates; // Set quest step array

        // if the quest step states and prefabs are different lengths,
        // something has changed during development and the saved data is out of sync.
        if (this.questStepStates.Length != this.info.questStepPrefabs.Length) // If quest step states and quest step prefabs are different lengths
        {
            Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                + "of different lengths. This indicates something changed "
                + "with the QuestInfo and the saved data is now out of sync. "
                + "Reset your data - as this might cause issues. QuestId: " + this.info.id); // Display warning log that data is out of sync
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++; // Increment quest step index to move to next quest step
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length); // Check to see whether there is another quest step
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab(); // Get current quest step from prefab
        if (questStepPrefab != null) // If quest step prefab exists
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>(); // Instantiate quest step prefab under the parents transform
            // CAN USE OBJECT POOLING INSTEAD OF INSTANTIATION IF TOO MEMORY INTENSIVE
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state); // Initialize current quest step
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null; // Set quest step prefab to null
        if (CurrentStepExists()) // If current quest step prefab exists
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex]; // Get the quest step prefab using current quest step index 
        }
        else // If there are no current quest steps
        {
            Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                + "there's no current step: QuestId=" + info.id + ", stepIndex=" + currentQuestStepIndex); // Warning for if quest step not found
        }
        return questStepPrefab; // Return the quest step prefab game object
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if (stepIndex < questStepStates.Length) // Check to make sure step index is within range of quest step state array
        {
            questStepStates[stepIndex].state = questStepState.state; // Update state in quest step states array
        }
        else  // If index is not within range of quest step state array
        {
            Debug.LogWarning("Tried to access quest step data, but stepIndex was out of range: "+ "Quest Id = " + info.id + ", Step Index = " + stepIndex); // Log warning
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates); // Return quest data
    }
}
