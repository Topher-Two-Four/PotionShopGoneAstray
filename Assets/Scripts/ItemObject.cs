using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemObject : MonoBehaviour
{
    public Rigidbody rigidBody;
    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(itemData);
            InventoryController.Instance.AddItemObjectToInventory(itemData);
            Destroy(gameObject);
        }
    }

}
