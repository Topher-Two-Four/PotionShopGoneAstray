using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawnManager : MonoBehaviour
{
    [Header("Ingredient Spawn Settings:")]
    [Tooltip("The base amount of ingredients to spawn without morality modifier applied.")]
    [SerializeField] private float baseIngredientSpawnAmount = 50;
    [Tooltip("The amount of ingredients to spawn after morality modifier is appplied.")]
    [SerializeField] private int ingredientsToSpawn;
    [Tooltip("The list of spawn point game objects that ingredients have a possibility to spawn at.")]
    [SerializeField] private GameObject[] ingredientSpawnPoints; // Array for holding ingredient spawn points

    [Header("Skull Bear Spawn Settings:")]
    [Tooltip("The Skull Bear Item Object.")]
    [SerializeField] private ItemObject skullBearItemObject; // List of all ingredients in the game, which the manager will use to spawn random ingredients
    [Tooltip("The base amount of ingredients to spawn without morality modifier applied.")]
    [SerializeField] private float baseSkullBearSpawnAmount = 10;
    [Tooltip("The amount of ingredients to spawn after morality modifier is appplied.")]
    [SerializeField] private int skullBearsToSpawn;
    [Tooltip("The list of spawn point game objects that ingredients have a possibility to spawn at.")]
    [SerializeField] private GameObject[] skullBearSpawnPoints; // Array for holding ingredient spawn points

    [Header("List of Ingredients:")]
    [Tooltip("The list of ingredients that can possibly spawn in the maze.")]
    [SerializeField] private ItemObject[] ingredientsArray; // List of all ingredients in the game, which the manager will use to spawn random ingredients

    [Header("Maze Enemy Settings:")]
    [Tooltip("The Bird enemy game object.")]
    [SerializeField] private GameObject birdGameObject; // List for holding enemy spawn points
    [Tooltip("The Music Man enemy game object.")]
    [SerializeField] private GameObject musicManGameObject; // List for holding enemy spawn points
    [Tooltip("The Jelly enemy game object.")]
    [SerializeField] private GameObject jellyGameObject; // List for holding enemy spawn points
    [Tooltip("The number of enemies to spawn after morality modifier is appplied.")]
    [SerializeField] private int enemiesToSpawn;
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
        GameManager.Instance.GetPlayerCapsule().transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
        GameManager.Instance.ToggleCursorOff();
        ingredientsToSpawn = Mathf.FloorToInt(baseIngredientSpawnAmount * MoralitySystem.Instance.GetIngredientSpawnModifier());
        skullBearsToSpawn = Mathf.FloorToInt(baseSkullBearSpawnAmount * MoralitySystem.Instance.GetSkullBearSpawnModifier());
        enemiesToSpawn = MoralitySystem.Instance.GetEnemySpawnAmount();

        for (int x = 0; x < ingredientsToSpawn; x++)
        {
            int randomSpawnPointIndex = Random.Range(0, ingredientSpawnPoints.Length - 1);
            GameObject spawnPoint = ingredientSpawnPoints[randomSpawnPointIndex];
            Vector3 spawnLocation = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);

            int randomIngredientIndex = Random.Range(0, ingredientsArray.Length - 1);

            Instantiate(ingredientsArray[randomIngredientIndex], spawnLocation, Quaternion.identity);

        }

        for (int x = 0; x < skullBearsToSpawn; x++)
        {
            int randomSpawnPointIndex = Random.Range(0, skullBearSpawnPoints.Length - 1);
            GameObject spawnPoint = skullBearSpawnPoints[randomSpawnPointIndex];
            Vector3 spawnLocation = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);

            int randomSkullBearIndex = Random.Range(0, ingredientsArray.Length - 1);

            Instantiate(skullBearItemObject, spawnLocation, Quaternion.identity);

        }


        bool birdSpawned = false;
        bool musicManSpawned = false;
        bool jellySpawned = false;

        for (int x = 0; x < enemiesToSpawn; x++)
        {
            int randomSpawnPointIndex = Random.Range(0, 3);

            if (randomSpawnPointIndex == 0 && birdSpawned == false)
            {
                birdGameObject.gameObject.SetActive(true);
                birdSpawned = true;
            } 
            else if (randomSpawnPointIndex == 1 && musicManSpawned == false)
            {
                musicManGameObject.gameObject.SetActive(true);
                musicManSpawned = true;
            } 
            else if (randomSpawnPointIndex == 2 && jellySpawned == false)
            {
                jellyGameObject.gameObject.SetActive(true);
                jellySpawned = true;
            } else
            {
                enemiesToSpawn--;
            }
        }
        GameManager.Instance.ToggleOffLoadingScreenCanvas();
    }
}
