using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawnManager : MonoBehaviour
{
    [Header("Ingredient Spawn Settings:")]
    [Tooltip("The base amount of ingredients to spawn without morality modifier applied.")]
    [SerializeField] private float baseSpawnAmount = 50;
    [Tooltip("The amount of ingredients to spawn after morality modifier is appplied.")]
    [SerializeField] private int ingredientsToSpawn;
    [Tooltip("The list of spawn point game objects that ingredients have a possibility to spawn at.")]
    [SerializeField] private GameObject[] ingredientSpawnPoints; // Array for holding ingredient spawn points

    [Header("List of Ingredients:")]
    [Tooltip("The list of ingredients that can possibly spawn in the maze.")]
    [SerializeField] private ItemObject[] ingredientsArray; // List of all ingredients in the game, which the manager will use to spawn random ingredients

    [Header("Maze Enemy Settings:")]
    [Tooltip("The list of spawn points for the maze enemy (not yet implemented).")]
    [SerializeField] private GameObject[] enemySpawnPoint; // List for holding enemy spawn points

    [Header("Player Spawn Settings:")]
    [Tooltip("Transformational zero for the maze level.")]
    [SerializeField] private GameObject zero; // List for holding enemy spawn points

    void Start()
    {
        AudioManager.Instance.sfxSource.Stop();
        AudioManager.Instance.sfx2Source.Stop();
        AudioManager.Instance.PlayMusic("MazeMusic");

        MoralitySystem.Instance.ApplyMoralityEffect();

        ingredientsToSpawn = Mathf.FloorToInt(baseSpawnAmount * MoralitySystem.Instance.GetIngredientSpawnModifier());

        for (int x = 0; x < ingredientsToSpawn; x++)
        {
            int randomSpawnPointIndex = Random.Range(0, ingredientSpawnPoints.Length - 1);
            GameObject spawnPoint = ingredientSpawnPoints[randomSpawnPointIndex];
            Vector3 spawnLocation = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);

            int randomIngredientIndex = Random.Range(0, ingredientsArray.Length - 1);

            Instantiate(ingredientsArray[randomIngredientIndex], spawnLocation, Quaternion.identity);

        }
        GameManager.Instance.GetPlayerCapsule().transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
        GameManager.Instance.ToggleCursorOff();
        // Spawn AI in random area
    }
}
