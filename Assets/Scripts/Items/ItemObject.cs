using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ItemObject : MonoBehaviour
{
    public Rigidbody rigidBody;
    public ItemData itemData;

    private void Update()
    {
        RotateTowardsTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && InventoryController.Instance.CheckForItemSpace(itemData))
        {
            InventoryController.Instance.AddItemObjectToInventory(itemData);
            Destroy(gameObject);
        }
    }

    private void RotateTowardsTarget()
    {
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(3)) ||
            SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(4)) ||
            SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(5)))
        {
            GameObject itemTarget = GameObject.FindGameObjectWithTag("ItemTarget");
            transform.LookAt(itemTarget.transform);
        }

    }

}
