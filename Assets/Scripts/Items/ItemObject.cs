using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ItemObject : MonoBehaviour
{
    [Header("General Settings:")]
    [Tooltip("The rigid body for the item object.")]
    [SerializeField] private Rigidbody rigidBody;
    [Tooltip("The item data for the item object.")]
    [SerializeField] private ItemData itemData;

    private void Update()
    {
        RotateTowardsTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (InventoryController.Instance.CheckForItemSelected()) { return; }

        if (other.gameObject.tag == "Player" && InventoryController.Instance.CheckForItemSpace(itemData))
        {
            InventoryController.Instance.AddItemObjectToInventory(itemData);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "UrnNPC")
        {
            ItemPot.Instance.AddItemToCount();
            Destroy(gameObject);
        }

    }

    private void RotateTowardsTarget()
    {
        if ((!SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(0)) ||
            !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(1)) ||
            !SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByBuildIndex(2))) &&
            GameObject.FindGameObjectWithTag("ItemTarget") != null)
        {
            GameObject itemTarget = GameObject.FindGameObjectWithTag("ItemTarget");
            transform.LookAt(itemTarget.transform);
        }

    }

}
