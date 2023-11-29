using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool loadQuestState = true; // Persist data if true

    private Dictionary<string, Quest> questMap; // Create dictionary to map a string to a Quest

    private int currentPlayerLevel; // Quest start requirement, right now set as player level, can use for morality system later

    private void Awake()
    {
        questMap = CreateQuestMap(); // Create new quest map
    }

    private void OnEnable() // Subscribe to events on enable
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;

        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }

    private void OnDisable() // Unsubscribe to events on disable
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;

        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;

        GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
    }

    private void Start()
    {
        
        foreach (Quest quest in questMap.Values) // Iterate through all quests in quest map
        {
            if (quest.state == QuestState.IN_PROGRESS)  // If quest state is in progress
            {
                quest.InstantiateCurrentQuestStep(this.transform); // Initialize any quest steps already loaded
            }
            GameEventsManager.instance.questEvents.QuestStateChange(quest); // Broadcast initial state of all quests on start
        }
    }

    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id); // Get quest using quest ID
        quest.state = state; // Set quest state to neew state
        GameEventsManager.instance.questEvents.QuestStateChange(quest); // Send out new quest state change event
    }

    private void PlayerLevelChange(int level)
    {
        currentPlayerLevel = level; // Change player to certain level based on passed parameter
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true; // Begin as true and will become false according to conditions

        if (currentPlayerLevel < quest.info.levelRequirement) // Check if player meets level requirements for quest
        {
            meetsRequirements = false; // If player does not meet requirements then set as false
        }

        // check quest prerequisites for completion
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites) // Iterate through quest prerequisites to check for completion
        {
            if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED) // If quest from quest info scriptable object is not yet in finished state
            {
                meetsRequirements = false; // Set to false if requirements not met and not yet finished
            }
        }

        return meetsRequirements; // Return whether the player meets quest requirements
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values) // Iterate through all quests in quest map
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest)) // Switch to can start state if requirements are now met for quest start
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START); // Change quest info scriptable object state to can start
            }
        }
    }

    private void StartQuest(string id) 
    {
        Quest quest = GetQuestById(id); // Get quest using quest ID
        quest.InstantiateCurrentQuestStep(this.transform); // Instantiate the current quest step under quest in scene
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS); // Change quest state for quest scriptable object to in-progress
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id); // Get quest using quest ID

        quest.MoveToNextStep(); // Progress quest to the next step

        if (quest.CurrentStepExists()) // If a new current step exists
        {
            quest.InstantiateCurrentQuestStep(this.transform); // Instantiate new current quest step
        }
        else // If no new current step exists (if quest is complete)
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH); // Change quest state to can finish
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id); // Get quest using quest ID
        ClaimRewards(quest); // Claim the rewards for this quest by calling claim rewards method
        ChangeQuestState(quest.info.id, QuestState.FINISHED); // Change quest state to finished
    }

    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.instance.goldEvents.GoldGained(quest.info.goldReward); // Call gold gained event to give player gold reward
        GameEventsManager.instance.playerEvents.ExperienceGained(quest.info.experienceReward); // Call experience gained event to give player experience reward
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id); // Get quest using quest ID
        quest.StoreQuestStepState(questStepState, stepIndex); // Store current quest step state to one passed through parameter
        ChangeQuestState(id, quest.state); // Send out quest state change event
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests"); // Load all of the QuestInfoSO scriptable objects in the Assets/Resources/Quests foler
        
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>(); // Create quest map
        
        foreach (QuestInfoSO questInfo in allQuests) // Iterate through quest info for all quests
        {
            if (idToQuestMap.ContainsKey(questInfo.id)) // Check if quest ID already exists
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id); // Show warning if duplicate quest ID has been found
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo)); // If quest doesn't already exist then map quest to quest ID by passing QuestInfoSO
        }
        return idToQuestMap; // Return the quest to quest ID map
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id]; // Get the quest from the quest map using ID
        if (quest == null) // If quest is null
        {
            Debug.LogError("ID not found in the Quest Map: " + id); // Return error that quest is not found in the quest map
        }
        return quest; // If quest exists in quest map then return it
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values) // Iterate through all quests in quest map
        {
            SaveQuest(quest); // Save quest
        }
    }

    // *** DEVELOP SAVE & LOAD SYSTEM THAT WRITES TO FILE RATHER THAN PLAYER PREFS ***
    private void SaveQuest(Quest quest)
    {
        try 
        {
            QuestData questData = quest.GetQuestData(); // Get quest data from quest parameter passed in

            string serializedData = JsonUtility.ToJson(questData); // Serialize quest save data into json format using JsonUtility (can also use JSON.NET if needed)

            PlayerPrefs.SetString(quest.info.id, serializedData); // Should eventually use save and load system here or save to cloud, instead of to player prefs

            Debug.Log(serializedData);
        }
        catch (System.Exception e) // Catch exception if quest not able to be saved
        {
            Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e); // Display error log stating that quest with this ID has failed to save
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null; // Define quest object as null
        try 
        {
            if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState) // If key exists 
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id); // Get serialized data from string in player prefs using quest ID
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData); // Load quest data from json file using JsonUtility
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates); // Create new quest using constructor
            }
            else // If key or load quest state doesn't exist
            {
                quest = new Quest(questInfo); // Create new quest using constructor
            }
        }
        catch (System.Exception e) // Catch exception if data is not found
        {
            Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e); // Display error log that quest data was not found
        }
        return quest;
    }
}
