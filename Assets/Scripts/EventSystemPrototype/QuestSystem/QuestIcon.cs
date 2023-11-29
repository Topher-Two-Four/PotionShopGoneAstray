using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject requirementsNotMetToStartIcon; // Icon to display when quest requirements not met to start
    [SerializeField] private GameObject canStartIcon; // Icon to display when player can start quest
    [SerializeField] private GameObject requirementsNotMetToFinishIcon; // Icon to display when completion requirements are not yet met
    [SerializeField] private GameObject canFinishIcon; // Icon to display when player can finish quest

    public void SetState(QuestState newState, bool startPoint, bool finishPoint)
    {
        requirementsNotMetToStartIcon.SetActive(false); // Initially set to inactive
        canStartIcon.SetActive(false); // Initially set to inactive
        requirementsNotMetToFinishIcon.SetActive(false); // Initially set to inactive
        canFinishIcon.SetActive(false); // Initially set to inactive

        // set the appropriate one to active based on the new state
        switch (newState) // Switch statement to determine active icon when state is changed
        {
            case QuestState.REQUIREMENTS_NOT_MET: // If quest requirements are not met to start
                if (startPoint) { requirementsNotMetToStartIcon.SetActive(true); } // Display quest requirements not met icon
                break;
            case QuestState.CAN_START: // If player can start quest
                if (startPoint) { canStartIcon.SetActive(true); } // Display can start quest icon
                break;
            case QuestState.IN_PROGRESS: // If quest is in progress
                if (finishPoint) { requirementsNotMetToFinishIcon.SetActive(true); } //  Display requirements not met to finish icon
                break;
            case QuestState.CAN_FINISH: // If player can finish quest
                if (finishPoint) { canFinishIcon.SetActive(true); } // Display can finish quest icon
                break;
            case QuestState.FINISHED: // If player has finished quest
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState); // Warning if quest state not recognized
                break;
        }
    }
}
