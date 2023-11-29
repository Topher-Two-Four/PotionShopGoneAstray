using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Serialized for saving
public class QuestStepState
{
    public string state; // State of quest step

    public QuestStepState(string state)
    {
        this.state = state; // Set quest step state based on passed parameter
    }

    public QuestStepState()
    {
        this.state = ""; // If no quest step state is passed then set to empty
    }
}
