using System;

public class QuestEvents
{
    public event Action<string> onStartQuest; // Event to call on start of quest

    public void StartQuest(string id)
    {
        if (onStartQuest != null) // If on start of quest event exists
        {
            onStartQuest(id); // Call start quest event using quest ID
        }
    }

    public event Action<string> onAdvanceQuest; // Event to call upon advancing quest
    public void AdvanceQuest(string id) 
    {
        if (onAdvanceQuest != null) // If on quest advance event exists
        {
            onAdvanceQuest(id); // Call advance quest event using quest ID
        }
    }

    public event Action<string> onFinishQuest; // Event to call upon finishing quest
    public void FinishQuest(string id)
    {
        if (onFinishQuest != null) // If on finish quest event exists
        {
            onFinishQuest(id); // Call finish quest event using quest ID
        }
    }

    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChange != null) // If state change for quest exists
        {
            onQuestStateChange(quest); // Call quest state change event using Quest game object
        }
    }

    public event Action<string, int, QuestStepState> onQuestStepStateChange; //

    public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        if (onQuestStepStateChange != null) // If state change for quest exists
        {
            onQuestStepStateChange(id, stepIndex, questStepState); // Call quest state change event
        }
    }
}
