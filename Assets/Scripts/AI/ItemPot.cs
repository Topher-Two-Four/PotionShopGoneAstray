using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPot : MonoBehaviour
{

    [SerializeField] private int hitsToBreak;
    [SerializeField] private Vector3 itemDropOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private int itemsCollectedDivider = 2;

    private int _itemsCollected = 0;
    
    public static ItemPot Instance { get; private set; }

    private void Start()
    {
        _itemsCollected = 0;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); } else { Instance = this; }
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            ItemObject[] ingredientList = MazeSpawnManager.Instance.GetIngredientsArray();

            int _itemsToSpawn = (_itemsCollected / itemsCollectedDivider);
            Debug.Log("Items to spawn: " + _itemsToSpawn);

            for (int x = 0; x < _itemsToSpawn; x++)
            {
                int randomIngredientIndex = Random.Range(0, ingredientList.Length - 1);
                Instantiate(ingredientList[randomIngredientIndex], gameObject.transform.position - itemDropOffset, gameObject.transform.rotation);
            }
            AudioManager.Instance.PlayUrnBreakSound();
            Destroy(gameObject);
        }
    }

    public void AddItemToCount()
    {
        _itemsCollected++;
        Debug.Log(_itemsCollected);
    }

    public void SetItemsCollectedDivider(int divisor)
    {
        itemsCollectedDivider = divisor;
    }

   /*  public void MoveEnemiesToUrnBreakSound()
    {
        Debug.Log("MoveEnemiesToUrnBreakSoundCalled");

        GameObject[] mazeEnemies = GameObject.FindGameObjectsWithTag("MazeEnemy");
        foreach (GameObject enemy in mazeEnemies)
        {
            //Search(PenguinBell.Instance.gameObject.transform.position);
            Debug.Log(enemy + " moving to " + PenguinBell.Instance.gameObject.transform.position);
        }
    }
   */
}
