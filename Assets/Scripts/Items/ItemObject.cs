using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        GameObject itemTarget = GameObject.FindGameObjectWithTag("ItemTarget");
        transform.LookAt(itemTarget.transform);
    }

}
