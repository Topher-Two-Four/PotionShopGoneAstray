using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawnManager : MonoBehaviour
{
    public float baseSpawnAmount = 50;
    public int ingredientsToSpawn;


    public GameObject[] ingredientSpawnPoints; // Array for holding ingredient spawn points
    public GameObject[] enemySpawnPoint; // List for holding enemy spawn points

    public ItemObject[] ingredientsArray; // List of all ingredients in the game, which the manager will use to spawn random ingredients

    void Start()
    {
        MoralitySystem.Instance.ApplyMoralityEffect();

        ingredientsToSpawn = Mathf.FloorToInt(baseSpawnAmount * MoralitySystem.Instance.ingredientSpawnModifier);

        for (int x = 0; x < ingredientsToSpawn; x++) 
        {
            int randomSpawnPointIndex = Random.Range(0, ingredientSpawnPoints.Length - 1);
            GameObject spawnPoint = ingredientSpawnPoints[randomSpawnPointIndex];
            Vector3 spawnLocation = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);

            int randomIngredientIndex = Random.Range(0, ingredientsArray.Length - 1);

            Instantiate(ingredientsArray[randomIngredientIndex], spawnLocation, Quaternion.identity);

        }

        // Spawn AI in random area
    }

}
