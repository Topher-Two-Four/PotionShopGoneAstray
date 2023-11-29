using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; } // Used to reference quests by display name

    [Header("General")]
    public string displayName; // Display name of the quest

    [Header("Requirements")]
    public int levelRequirement; // Level requirement to start quest
    public QuestInfoSO[] questPrerequisites; // Prerequisite quests

    [Header("Steps")]
    public GameObject[] questStepPrefabs; // Quest steps for multi-part quets

    [Header("Rewards")]
    public int goldReward; // Gold reward for quest
    public int experienceReward; // Experience reward for quest

    // ensure the id is always the name of the Scriptable Object asset
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
