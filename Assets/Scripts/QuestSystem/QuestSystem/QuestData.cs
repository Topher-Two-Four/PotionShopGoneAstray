using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData // This class is used to store and persist quest data
{
    public QuestState state; // Current quest state
    public int questStepIndex; // Current quest step index
    public QuestStepState[] questStepStates; // Quest step states array

    public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepStates) // Constructor that takes in quest data variable values
    {
        this.state = state; // Set current quest state to state passed through parameter
        this.questStepIndex = questStepIndex; // Set current quest index to state passed through parameter
        this.questStepStates = questStepStates; // Set quest step states array to array passed through parameter
    }
}
