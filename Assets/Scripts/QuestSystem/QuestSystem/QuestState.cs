using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    REQUIREMENTS_NOT_MET, // When player has not met requirements for quest yet
    CAN_START, // When player is able to start the quest
    IN_PROGRESS, // When the quest is in progress
    CAN_FINISH, // When the quest can be finished
    FINISHED // When the quest is complete and finished
}
